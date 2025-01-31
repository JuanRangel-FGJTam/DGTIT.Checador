using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Topshelf;
using Topshelf.Autofac;
using log4net.Config;
using Autofac;
using Autofac.Extras.Quartz;
using ChecadorService.Services;
using ChecadorService.Jobs;
using ChecadorService.Data.Repositories;
using log4net;

namespace ChecadorService {
    internal static class Program {
        
        static void Main() {
            // * read configuration for log4net
            XmlConfigurator.Configure();

            // * configure services for dependancy injection
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ChecadorWService>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EmployeeService>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<NotifyService>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<SQLRecordRepository>().As<IRecordRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RecordServerRepo>().As<IRecordServerRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RecordService>().As<IRecordService>().InstancePerLifetimeScope();

            containerBuilder.RegisterModule(new QuartzAutofacFactoryModule {
                ConfigurationProvider = context => 
                    (NameValueCollection)ConfigurationManager.GetSection("quartz")
            });
            containerBuilder.RegisterModule(new QuartzAutofacJobsModule(Assembly.GetExecutingAssembly()));
            IContainer container = containerBuilder.Build();

            // * configure the windows service
            HostFactory.Run(hostConfigurator => {
                hostConfigurator.SetServiceName("DGTIT - Checador service");
                hostConfigurator.SetDisplayName("Checador service");
                hostConfigurator.SetDescription("Servicio utilizado por el checador para sincronizar datos con el servidor.");
                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.UseLog4Net();
                hostConfigurator.UseAutofacContainer(container);
                hostConfigurator.StartAutomatically();
                
                hostConfigurator.Service<ChecadorWService>(serviceConfigurator => {
                    serviceConfigurator.ConstructUsingAutofacContainer();
                    serviceConfigurator.WhenStarted(s => s.OnStart());
                    serviceConfigurator.WhenStopped(s => s.OnStop());
                    serviceConfigurator.WhenPaused(s => s.OnPaused());
                    serviceConfigurator.WhenContinued(s => s.OnContinue());
                });
            });
        }
    }
}

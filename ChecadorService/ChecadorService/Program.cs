using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using log4net.Config;
using Autofac;
using ChecadorService.Services;
using Autofac.Extras.Quartz;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Configuration;
using ChecadorService.Jobs;
using System.Reflection;
using Topshelf.Autofac;

namespace ChecadorService {
    internal static class Program {
        
        static void Main() {
            // * read configuration for log4net
            XmlConfigurator.Configure();

            // * configure services for dependancy injection
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ChecadorWService>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EmployeeService>().AsSelf().InstancePerDependency();
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
                //hostConfigurator.RunAsLocalSystem();
                hostConfigurator.RunAs("Usuario", "j6r3uwb9");
                hostConfigurator.UseLog4Net();
                hostConfigurator.UseAutofacContainer(container);
                
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

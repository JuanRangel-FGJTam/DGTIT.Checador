using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using DGTIT.Checador.Core.Interfaces;
using DGTIT.Checador.ViewModels;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Utilities;

namespace DGTIT.Checador.Services {
    internal class FiscaliaService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IProcuEmployeeRepo procuEmployeeRepo;
        private readonly ILog logger;
        private readonly Uri storagePath;

        internal FiscaliaService(IEmployeeRepository empRepo, IProcuEmployeeRepo procuEmployeeRepo)
        {
            this.employeeRepository = empRepo;
            this.procuEmployeeRepo = procuEmployeeRepo;
            this.logger = LogManager.GetLogger(typeof(FiscaliaService));
            this.storagePath = new Uri(CustomApplicationSettings.GetStoragePath());
        }

        public async Task<IEnumerable<ProcuEmployee>> SearchEmployees(string search)
        {
            try
            {
                var employeesList = await procuEmployeeRepo.SearchByEmployeeNumber(int.Parse(search));
                return employeesList.ToList();
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                return Array.Empty<ProcuEmployee>();
            }
        }

        public async Task<Bitmap> GetEmployeePhoto(int employeeNumber)
        {
            // * get the employee 
            var employee = await employeeRepository.FindByEmployeeNumber(employeeNumber);

            // * Attemp to get the foto from the disk
            Bitmap bmp;
            try
            {
                bmp = this.GetPhotoFromDisk(employee.Photo) ?? throw new KeyNotFoundException("Not foto was found on the disk");
                return bmp;
            }
            catch (Exception err)
            {
                logger.Error($"Can't load the foto of the employee '{employeeNumber}' from the disk: {err.Message}", err);
            }
            
            // * attempt to get the foto from the server.
            byte[] foto = null;
            try
            {
                // * get the employee infor from the RH Database
                var procuEmployee = await this.procuEmployeeRepo.FindByEmployeeNumber(employeeNumber)
                        ?? throw new KeyNotFoundException($"The employee number '{employeeNumber}' was not found on the server. ");
                foto = procuEmployee.Foto;
            }
            catch (Exception err)
            {
                logger.Error($"Fail at attempt to get the employee data from the RH Database: {err.Message}", err);
            }

            // * if employee has no poto, show a provisional one
            if (foto == null)
            {
                bmp = new Bitmap(Properties.Resources.employee_unknow);
                return bmp;
            }

            if (!foto.Any())
            {
                bmp = new Bitmap(Properties.Resources.employee_unknow);
                return bmp;
            }

            // * store the foto for used the next time
            try
            {
                StorePhotoToDisk(employee.Photo, foto);
            }
            catch (UnauthorizedAccessException ex)
            {
                this.logger.Error($"Can't save the photo of the employee :'{employeeNumber}': {ex.Message}");
            }

            using (var ms = new MemoryStream(foto))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }

        // * Method for access the employee photos on the disk
        private Bitmap GetPhotoFromDisk(string path)
        {
            var filePath = Path.Combine(this.storagePath.AbsolutePath, path);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified photo does not exist.", filePath);
            }
            using (var ms = new MemoryStream(File.ReadAllBytes(filePath)))
            {
                return new Bitmap(ms);
            }
        }

        private void StorePhotoToDisk(string path, byte[] foto)
        {
            var filePath = new Uri( Path.Combine(this.storagePath.AbsolutePath, path));
            var directoryPath = Path.GetDirectoryName(filePath.AbsolutePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(filePath.AbsolutePath, foto);
        }

    }
}

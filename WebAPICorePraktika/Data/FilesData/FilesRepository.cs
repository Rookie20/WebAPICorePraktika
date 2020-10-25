using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPICorePraktika.Data.ApplicationUserData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.FilesData {
    public class FilesRepository : IFilesRepository {
        private readonly ApplicationUserContext _context;
        private readonly IHostEnvironment _hostEnvironment;

        public FilesRepository(ApplicationUserContext context, IHostEnvironment hostEnvironment) {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        //public string Base64File(Files files) {
        //    var base64 = Convert.ToBase64String(files.FileData);
        //    var url = string.Format("data:image/png;base64,{0}", base64);
        //    return url;
        //}


        public IEnumerable<Files> GetAllFiles() {
            return _context.Files.ToList();
        }

        public Files GetFileById(int id) {
            return _context.Files.FirstOrDefault(p => p.FileId == id);
        }

        private string GetUniqueFileName(string fileName)
        {
            //fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }


        public void UploadFile(IFormFile formFile)
        {

            var fileName = Path.GetFileName(formFile.FileName);
            var newFileName = GetUniqueFileName(fileName);

            var fileExtension = Path.GetExtension(fileName);

            var saveImage = Path.Combine("Resources", "Images");
            var filePath = Path.Combine(saveImage, newFileName);
            formFile.CopyTo(new FileStream(filePath, FileMode.Create));

            var objFile = new Files()
            {
                FileId = 0,
                FileName = newFileName,
                FileType = fileExtension,
                FileCreatedOn = DateTime.Now
            };

            using (var target = new MemoryStream())
            {
                formFile.CopyTo(target);
                objFile.FileData = target.ToArray();
            }

            _context.Files.Add(objFile);
            _context.SaveChanges();
        }
    }
}

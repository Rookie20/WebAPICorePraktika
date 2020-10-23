using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public FilesRepository(ApplicationUserContext context) {
            _context = context;
        }

        public string Base64File(Files files) {
            var base64 = Convert.ToBase64String(files.FileData);
            var url = string.Format("data:image/png;base64,{0}", base64);
            return url;
        }

        public IEnumerable<Files> GetAllFiles() {
            return _context.Files.ToList();
        }

        public Files GetFileById(int id) {
            return _context.Files.FirstOrDefault(p => p.FileId == id);
        }

        public void UploadFile(IFormFile formFile) {
            if(formFile != null) {
                if(formFile.Length > 0) {

                    var fileName = Path.GetFileName(formFile.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(Guid.NewGuid().ToString(), fileExtension);

                    var objFile = new Files() {
                        FileId = 0,
                        FileName = newFileName,
                        FileType = fileExtension,
                        FileCreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream()) {
                        formFile.CopyTo(target);
                        objFile.FileData = target.ToArray();
                    }

                    _context.Files.Add(objFile);
                    _context.SaveChanges();
                }
            }
        }
    }
}

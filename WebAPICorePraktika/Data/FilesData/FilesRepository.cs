using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAPICorePraktika.Data.ApplicationUserData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.FilesData {
    public class FilesRepository : IFilesRepository {
        private readonly ApplicationUserContext _context;

        public FilesRepository(ApplicationUserContext context) {
            _context = context;
        }

        public IEnumerable<Files> GetAllFiles(string id) {
            return _context.Files.Include(a => a.ApplicationUser).Where(a => a.Id == id).ToList();
        }

        public Files GetFileById(int id) {
            return _context.Files.Include(a => a.ApplicationUser).FirstOrDefault(p => p.FileId == id);
        }


        private string GetUniqueFileName(string fileName) {

            return Path.GetFileNameWithoutExtension(fileName)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 4)
                + Path.GetExtension(fileName);
        }


        public void UploadFile(string id, IFormFile formFile) {
            if(formFile != null) {
                if(formFile.Length > 0) {

                    var fileName = Path.GetFileName(formFile.FileName);
                    var newFileName = GetUniqueFileName(fileName);

                    var fileExtension = Path.GetExtension(fileName);

                    var saveImage = Path.Combine("Resources", "Images");
                    var filePath = Path.Combine(saveImage, newFileName);

                    var fs = new FileStream(filePath, FileMode.Create);
                    formFile.CopyTo(fs);

                    
                    var objFile = new Files() {
                        FileId = 0,
                        FileName = newFileName,
                        FileType = fileExtension,
                        FileCreatedOn = DateTime.Now,
                        Id = id
                    };

                    using (var target = new MemoryStream()) {
                        formFile.CopyTo(target);
                        objFile.FileData = target.ToArray();
                    }

                    _context.Files.Add(objFile);
                    fs.Close();
                }
            }
        }

        public void Delete(Files formFile) {
            _context.Files.Remove(formFile);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.FilesData {
    public interface IFilesRepository {
        void UploadFile(IFormFile formFile);
        IEnumerable<Files> GetAllFiles();
        Files GetFileById(int id);

        string Base64File(Files files);
    }
}

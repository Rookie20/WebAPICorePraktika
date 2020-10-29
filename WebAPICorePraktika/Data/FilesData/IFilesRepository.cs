using System.Collections.Generic;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.FilesData
{
    public interface IFilesRepository {
        void UploadFile(string id, Files formFile);
        IEnumerable<Files> GetAllFiles(string id);
        Files GetFileById(int id);

        void Delete(Files formFile);

        void SaveChanges();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebAPICorePraktika.Data.FilesData;
using WebAPICorePraktika.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Hosting;
using System.Net.Mime;

namespace WebAPICorePraktika.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase {
        private readonly IFilesRepository _repository;
        private readonly IHostEnvironment _hostEnvironment;

        public FilesController(IFilesRepository repository, IHostEnvironment hostEnvironment) {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Files>> GetAllFiles() {
            return Ok(_repository.GetAllFiles());
        }

        [HttpGet("{id}")]
        public ActionResult<Files> GetFileById(int id) {

            return Ok(_repository.GetFileById(id));
        }

        //[Route("base64/{id}")]
        //public ActionResult GetBase64File(int id) {

        //    var files = _repository.GetFileById(id);
        //    var base64 = _repository.Base64File(files);

        //    return Ok(base64);
        //}

        [HttpPost]
        public IActionResult UploadFiles(IFormFile formFile) {
            if (formFile != null) 
            {
                if(formFile.Length > 0)
                {
                    _repository.UploadFile(formFile);
                    return Ok();
                }
                return BadRequest();
            }
            return NotFound();
            
        }


        [HttpGet("download/{id}")]
        public IActionResult Download(int id)
        {

            var fileDetails = _repository.GetFileById(id);
            if (fileDetails != null)
            {
                //ContentDisposition cd = new ContentDisposition
                //{
                //    FileName = fileDetails.FileName,
                //    Inline = false
                //};

                //Response.Headers.Add("Content-Disposition", cd.ToString());

                //var path = _hostEnvironment.ContentRootPath;
                //var fileReadPath = Path.Combine(path, "Resources", "Images", fileDetails.FileName);
                //var file = System.IO.File.OpenRead(fileReadPath);

                //var mimeFileType = GetMimeTypeByWindowsRegistry(fileDetails.FileType);

                var path = _hostEnvironment.ContentRootPath;
                var fileReadPath = Path.Combine(path, "Resources", "Images", fileDetails.FileName);

                byte[] fileBytes = System.IO.File.ReadAllBytes(fileReadPath);

                return File(fileBytes, "application/force-download", fileDetails.FileName);
            }

            return NotFound();
        }


        public static string GetMimeTypeByWindowsRegistry(string fileNameOrExtension)
        {
            string mimeType = "application/unknown";
            string ext = (fileNameOrExtension.Contains(".")) ? System.IO.Path.GetExtension(fileNameOrExtension).ToLower() : "." + fileNameOrExtension;
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }



        //public FileResult Download(int id)
        //{
        //    var file = _repository.GetFileById(id);
        //    var FileVirtualPath = "~/Resources/Images/" + file.FileName;
        //    return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        //}


    }
}

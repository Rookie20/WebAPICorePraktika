using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebAPICorePraktika.Data.FilesData;
using WebAPICorePraktika.Models;
using Microsoft.Extensions.Hosting;

namespace WebAPICorePraktika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase {
        private readonly IFilesRepository _repository;
        private readonly IHostEnvironment _hostEnvironment;

        public FilesController(IFilesRepository repository, IHostEnvironment hostEnvironment) {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Files>> GetAllFiles(string id) {
            return Ok(_repository.GetAllFiles(id));
        }

        [HttpGet("details/{id}")]
        public ActionResult<Files> GetFileById(int id) {

            return Ok(_repository.GetFileById(id));
        }

        [HttpPost("{id}")]
        public IActionResult UploadFiles(string id, [FromBody] Files formFile) {

            if (formFile != null)
            {
                if (formFile.FileData != null)
                {
                    _repository.UploadFile(id, formFile);
                    _repository.SaveChanges();
                    return Ok();

                }
                return BadRequest();
            }
            return NotFound();

            //var content=Convert.FromBase64String(fileModel.FileContent);

            //return Ok();
        }


        //[HttpGet("download/{id}")]
        //public IActionResult Download(int id) {

        //    var file = _repository.GetFileById(id);
        //    if(file != null) {

        //        return File(file.FileData, "application/force-download", file.FileName);

        //    }
        //    return NotFound();
            
        //}

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteFile(int id) {

            var file = _repository.GetFileById(id);
            if(file != null) {
                _repository.Delete(file);

                //var path = _hostEnvironment.ContentRootPath;
                //var fileReadPath = Path.Combine(path, "Resources", "Images", file.FileName);

                //if (System.IO.File.Exists(fileReadPath)) {
                //    System.IO.File.Delete(fileReadPath);
                //}

                _repository.SaveChanges();
                return Ok();
            }
            return NotFound();   
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICorePraktika.Data.FilesData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase {
        private readonly IFilesRepository _repository;

        public FilesController(IFilesRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Files>> GetAllFiles() {
            return Ok(_repository.GetAllFiles());
        }

        [HttpGet("{id}")]
        public ActionResult<Files> GetFileById(int id) {

            return Ok(_repository.GetFileById(id));
        }

        [Route("base64/{id}")]
        public ActionResult GetBase64File(int id) {

            var files = _repository.GetFileById(id);
            var base64 = _repository.Base64File(files);
            
            return Ok(base64);
        }

        [HttpPost]
        public IActionResult UploadFiles(IFormFile formFile) {
            _repository.UploadFile(formFile);
            return Ok();
        }
    }
}

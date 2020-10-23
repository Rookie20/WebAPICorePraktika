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


        public IConfigurationRoot GetConnection() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();
            return builder;

        }

        [HttpGet("download/{id}")]
        public IActionResult Download(int id) {

            byte[] bytes = null;

            string name = string.Empty;

            string connectionstring = GetConnection().GetSection("ConnectionStrings").GetSection("PraktikaDB").Value;

            using (SqlConnection con = new SqlConnection(connectionstring)) {

                using (SqlCommand cmd = new SqlCommand("Select * from Files where FileId=@FileID", con)) {

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@FileID", id);

                    cmd.Connection = con;

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    sdr.Read();

                    if (sdr.HasRows) {

                        bytes = (byte[])sdr["FileData"];

                        name = Convert.ToString(sdr["FileName"]);

                    }
                    con.Close();

                }

            }

            return Ok(File(Convert.ToBase64String(bytes), "application/png", name, lastModified: DateTime.UtcNow.AddSeconds(-5),
                entityTag: new EntityTagHeaderValue("\"AyeWeDidSomething\"")));
        }

    }
}

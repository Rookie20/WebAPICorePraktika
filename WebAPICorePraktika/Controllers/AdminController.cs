using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPICorePraktika.Data.AdminData;
using WebAPICorePraktika.Data.FilesData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase {
        private readonly IAdminRepository _repository;
        private readonly IFilesRepository _filesRepository;

        public AdminController(IAdminRepository repository, IFilesRepository filesRepository) {
            _repository = repository;
            _filesRepository = filesRepository;
        }


        [HttpGet("personal")]
        public ActionResult GetPersonalData() {
            string userName = User.Identity.Name;

            var getUserData = _repository.GetUserById(_repository.UserId(userName));
            var getUserHistory = _repository.GetHistorikuPoziPunes(_repository.UserId(userName));
            var getUserFiles = _filesRepository.GetAllFiles(_repository.UserId(userName));

            return Ok(new { getUserData, getUserHistory, getUserFiles });
        }


        [HttpGet("aktiv/{id}")]
        public ActionResult<IEnumerable<ApplicationUser>> GetAllUsersAktiv(int id) {

            if (_repository.PozicionPuneExist(id)) {
                return Ok(_repository.GetAllUsersAktiv(id));
            }

            return NotFound();
        }

        [HttpGet("joaktiv/{id}")]
        public ActionResult<IEnumerable<ApplicationUser>> GetAllUsersJoAktiv(int id) {

            if (_repository.PozicionPuneExist(id)) {
                return Ok(_repository.GetAllUsersJoAktiv(id));
            }

            return NotFound();
        }

        [HttpGet("details/{id}")]
        public ActionResult<ApplicationUser> GetUserById(string id) {

            if (id == null) {
                return NotFound();
            }

            var userData = _repository.GetUserById(id);
            if(userData == null)
            {
                return NotFound();
            }

            return Ok(userData);
        }

        [HttpPut("edit/{id}")]
        public ActionResult UpdateUser(string id, ApplicationUser applicationUser) {
 
            if(id != applicationUser.Id) {
                return NotFound();
            }

            HistorikuPoziPune historikuPoziPune = new HistorikuPoziPune();

            _repository.HistoriaPoziPerpara(id, historikuPoziPune);
            _repository.UpdateUser(id, applicationUser);
            _repository.SaveChanges();
            _repository.HistoriaPoziPas(id, historikuPoziPune);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUser(string id) {

            var user = _repository.GetUserById(id);
            if(user != null) {

                _repository.DeleteUser(user);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
    }
}

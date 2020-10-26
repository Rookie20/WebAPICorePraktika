using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPICorePraktika.Data.AdminData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Controllers {

    [ApiController]
    [Route("api/departament/pozicionpune/[controller]")]
    public class AdminController : ControllerBase {
        private readonly IAdminRepository _repository;

        public AdminController(IAdminRepository repository) {
            _repository = repository;
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

            return Ok(_repository.GetUserById(id));
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

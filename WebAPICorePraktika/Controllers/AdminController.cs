using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICorePraktika.Data.AdminData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase {
        private readonly IAdminRepository _repository;

        public AdminController(IAdminRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApplicationUser>> GetAllUsers() {
            return Ok(_repository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<ApplicationUser> GetUserById(string id) {
            if (id == null) {
                return NotFound();
            }
            return Ok(_repository.GetUserById(id));
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

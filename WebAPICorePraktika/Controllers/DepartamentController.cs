﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPICorePraktika.Data.DepartamentData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Controllers {

    //[Authorize/*(Roles = UserRoles.Admin)*/]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentController : ControllerBase {
        private readonly IDepartamentRepository _repository;

        public DepartamentController(IDepartamentRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Departamenti>> GetAllDepartament() {
            var allDepartament = _repository.GetAllDepartamente();
            return Ok(allDepartament);
        }

        [HttpGet("details/{id}")]
        public ActionResult<Departamenti> GetDepartamentById(int id) {
            var departamenti = _repository.GetDepartamentiById(id);
            if (departamenti != null) {
                return Ok(departamenti);
            }
            return NotFound();
        }

        [HttpPost("create")]
        public ActionResult<Departamenti> CreateDepartament(Departamenti departamenti) {
            if (!_repository.GetDepartamentEmer(departamenti.DepartamentEmer)) {
                _repository.CreateDepartament(departamenti);
                _repository.SaveChanges();
                return Accepted();
            }
            return BadRequest();
        }

        [HttpPut("edit/{id}")]
        public ActionResult<Departamenti> UpdateDepartamenti(int id, Departamenti departamenti) {
            if (id != departamenti.DepartamentId) {
                return BadRequest();
            }
            _repository.UpdateDepartament(departamenti);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<Departamenti> DeleteDepartamenti(int id) {
            var departamenti = _repository.GetDepartamentiById(id);
            if(departamenti == null) {
                return NotFound();
            }
            _repository.DeleteDepartament(departamenti);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}

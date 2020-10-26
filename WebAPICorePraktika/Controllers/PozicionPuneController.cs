using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPICorePraktika.Data.PozicionPuneData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class PozicionPuneController : ControllerBase {
        private readonly IPozicionPuneRepository _repository;

        public PozicionPuneController(IPozicionPuneRepository repository) {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PozicioniPunes>> GetAllPozicionPune(int id) {

            if (_repository.DepartamentExist(id)) {
                var allPozi = _repository.GetAllPozicioniPune(id);
                return Ok(allPozi);
            }
            return NotFound();
        }

        [HttpGet("details/{id}")]
        public ActionResult<PozicioniPunes> GetPozicionPuneById(int id) {
            var pozi = _repository.GetPozicioniPunesById(id);
            if (pozi != null) {
                return Ok(pozi);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PozicioniPunes> CreatePozicionPune(PozicioniPunes pozicioniPunes) {

            var exist = _repository.DepartamentExist(pozicioniPunes.DepartamentId);
            if (!exist) {
                return NotFound();
            }
            _repository.CreatePozicionPune(pozicioniPunes);
            _repository.SaveChanges();
            return Accepted();
        }

        [HttpPut("{id}")]
        public ActionResult<PozicioniPunes> UpdatePozicionPune(int id, PozicioniPunes pozicioniPunes) {
            if(id != pozicioniPunes.PozicionPuneId || !_repository.DepartamentExist(pozicioniPunes.DepartamentId)) {
                return BadRequest();
            }
            _repository.UpdatePozicioniPune(pozicioniPunes);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<PozicioniPunes> DeletePozicionPune(int id) {
            var pozi = _repository.GetPozicioniPunesById(id);
            if(pozi == null) {
                return NotFound();
            }
            _repository.DeletePozicionPune(pozi);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}

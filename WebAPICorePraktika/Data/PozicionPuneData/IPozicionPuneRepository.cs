using System.Collections.Generic;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.PozicionPuneData {
    public interface IPozicionPuneRepository {
        IEnumerable<PozicioniPunes> GetAllPozicioniPune(int id);
        PozicioniPunes GetPozicioniPunesById(int id);
        bool PozicionPuneEmriExist(string poziEmer);
        void CreatePozicionPune(PozicioniPunes pozicioniPunes);
        void UpdatePozicioniPune(PozicioniPunes pozicioniPunes);
        void DeletePozicionPune(PozicioniPunes pozicioniPunes);          
        bool DepartamentExist(int id);
        bool PozicioniExist(int id);
        bool SaveChanges();
    }
}

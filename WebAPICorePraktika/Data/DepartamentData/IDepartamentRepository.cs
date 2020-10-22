using System.Collections.Generic;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.DepartamentData {
    public interface IDepartamentRepository {
        IEnumerable<Departamenti> GetAllDepartamente();
        Departamenti GetDepartamentiById(int id);

        void CreateDepartament(Departamenti departamenti);
        void UpdateDepartament(Departamenti departamenti);

        void DeleteDepartament(Departamenti departamenti);

        bool SaveChanges();

    }
}

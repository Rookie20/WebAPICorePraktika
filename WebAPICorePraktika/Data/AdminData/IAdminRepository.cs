using System.Collections.Generic;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.AdminData {
    public interface IAdminRepository {

        IEnumerable<ApplicationUser> GetAllUsersAktiv(int id);
        IEnumerable<ApplicationUser> GetAllUsersJoAktiv(int id);

        IEnumerable<HistorikuPoziPune> GetHistorikuPoziPunes(string id);
        ApplicationUser GetUserById(string id);
        string UserId(string userName);
        void UpdateUser(string id, ApplicationUser user);
        void DeleteUser(ApplicationUser user);

        bool PozicionPuneExist(int id);

        void HistoriaPoziPerpara(string id, HistorikuPoziPune historikuPoziPune);
        void HistoriaPoziPas(string id, HistorikuPoziPune historikuPoziPune);
        bool SaveChanges();
    }
}

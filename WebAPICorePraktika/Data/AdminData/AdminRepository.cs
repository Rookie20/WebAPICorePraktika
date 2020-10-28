using System.Collections.Generic;
using System.Linq;
using WebAPICorePraktika.Data.ApplicationUserData;
using WebAPICorePraktika.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPICorePraktika.Data.AdminData {
    public class AdminRepository : IAdminRepository {
        private readonly ApplicationUserContext _context;
        //private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AdminRepository(ApplicationUserContext context) {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllUsersAktiv(int id) {
            return _context.Users.Include(p => p.PozicioniPune).ThenInclude(d => d.Departament)
                .Include(h => h.HistorikuPoziPunes).Include(f => f.Files)
                .Where(p => p.PozicionPuneId == id && p.Aktiv == true).ToList();
        }

        public IEnumerable<ApplicationUser> GetAllUsersJoAktiv(int id) {
            return _context.Users.Include(p => p.PozicioniPune).ThenInclude(d => d.Departament)
                .Include(h => h.HistorikuPoziPunes).Include(f => f.Files)
                .Where(p => p.PozicionPuneId == id && p.Aktiv == false).ToList();
        }

        public ApplicationUser GetUserById(string id) {
            return _context.Users.Include(p => p.PozicioniPune).ThenInclude(d => d.Departament)
                .Include(h => h.HistorikuPoziPunes)
                .Include(f => f.Files)
                .FirstOrDefault(u => u.Id == id);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(string id, ApplicationUser user) {
            var appUser = GetUserById(id);
            

            if(user != null) {
                appUser.Id = user.Id;
                appUser.UserName = user.UserName;
                appUser.NormalizedUserName = appUser.UserName.ToUpper();
                appUser.Email = user.Email;
                appUser.NormalizedEmail = appUser.Email.ToUpper();
                appUser.PasswordHash = user.PasswordHash;
                appUser.SecurityStamp = user.SecurityStamp;
                appUser.ConcurrencyStamp = user.ConcurrencyStamp;
                appUser.PhoneNumber = user.PhoneNumber;
                appUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                appUser.TwoFactorEnabled = user.TwoFactorEnabled;
                appUser.LockoutEnd = user.LockoutEnd;
                appUser.LockoutEnabled = user.LockoutEnabled;
                appUser.AccessFailedCount = appUser.AccessFailedCount;
                appUser.PozicionPuneId = appUser.PozicionPuneId;
                appUser.Aktiv = user.Aktiv;
                appUser.Emer = user.Emer;
                appUser.Mbiemer = user.Mbiemer;
                appUser.KartaId = user.KartaId;

                if (PozicionPuneExist(user.PozicionPuneId)) {
                    appUser.PozicionPuneId = user.PozicionPuneId;
                }
            }
        }

        public bool PozicionPuneExist(int id) {
            if(_context.PozicioniPune.Where(p => p.PozicionPuneId == id).Any()) {
                return true;
            }
            return false;
        }

        public void DeleteUser(ApplicationUser user) {
            _context.Users.Remove(user);
        }

        public void AddHistoria(ApplicationUser user, HistorikuPoziPune historikuPoziPune) {
            historikuPoziPune.PozicioniPas = user.PozicioniPune.PozicionPuneEmri;
            _context.HistorikuPoziPunes.Add(historikuPoziPune);
            _context.SaveChanges();
        }

        public void HistoriaPoziPerpara(string id, HistorikuPoziPune historikuPoziPune) {
            var user = GetUserById(id);
            historikuPoziPune.Id = id;
            historikuPoziPune.PozicioniPerpara = user.PozicioniPune.PozicionPuneEmri;
        }

        public void HistoriaPoziPas(string id, HistorikuPoziPune historikuPoziPune) {
            var user = GetUserById(id);
            historikuPoziPune.PozicioniPas = user.PozicioniPune.PozicionPuneEmri;

            if(historikuPoziPune.PozicioniPerpara != historikuPoziPune.PozicioniPas) {
                _context.HistorikuPoziPunes.Add(historikuPoziPune);
                SaveChanges();
            }
            
        }

        public IEnumerable<HistorikuPoziPune> GetHistorikuPoziPunes(string id) {
            return _context.HistorikuPoziPunes.Where(a => a.Id == id).ToList();
        }

        public string UserId(string userName) {
            return _context.Users.Where(a => a.Email == userName).Select(a => a.Id).FirstOrDefault();
        }
    }
}

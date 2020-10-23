﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICorePraktika.Data.ApplicationUserData;
using WebAPICorePraktika.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebAPICorePraktika.Data.AdminData {
    public class AdminRepository : IAdminRepository {
        private readonly ApplicationUserContext _context;
        //private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AdminRepository(ApplicationUserContext context) {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllUsers() {
            return _context.Users.Include(p => p.PozicioniPune).ThenInclude(d => d.Departament).ToList();
        }

        public ApplicationUser GetUserById(string id) {
            return _context.Users.Include(p => p.PozicioniPune).ThenInclude(d => d.Departament).FirstOrDefault(u => u.Id == id);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(string id, ApplicationUser user) {
            var appUser = GetUserById(id);
            if(user != null) {
                appUser.Email = user.Email;
                appUser.UserName = user.UserName;
                appUser.NormalizedEmail = appUser.Email.ToUpper();
                appUser.NormalizedUserName = appUser.UserName.ToUpper();
                appUser.PhoneNumber = user.PhoneNumber;

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
    }
}

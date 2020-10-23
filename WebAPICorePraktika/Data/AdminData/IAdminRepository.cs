﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.AdminData {
    public interface IAdminRepository {

        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(string id);

        void UpdateUser(string id, ApplicationUser user);

        void DeleteUser(ApplicationUser user);
        bool SaveChanges();
    }
}
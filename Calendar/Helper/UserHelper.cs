using Calendar.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;


namespace Calendar.Helper
{
    public static class UserHelper
    {
        /// <summary>
        /// This function checks if the given user is admin or not.
        /// </summary>
        /// <param name="pUserId"></param>
        /// <returns>
        /// ture: if Admin
        /// false: otherwise
        /// </returns>
        public static Boolean isAdminUser(string pUserId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var s = UserManager.GetRoles(pUserId);
            if (s.Count > 0)
            {
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
            }

            return false;

        }
    }
}
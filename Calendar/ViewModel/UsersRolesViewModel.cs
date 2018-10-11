using System.Collections.Generic;
using System.Web.Mvc;


namespace Calendar.ViewModel
{
    /// <summary>
    /// Users with rules, Roles is used for binding to the DropDownlist
    /// </summary>
    public class UsersRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<string> RolesId { get; set; }
        public MultiSelectList Roles { get; set; }
    }
}
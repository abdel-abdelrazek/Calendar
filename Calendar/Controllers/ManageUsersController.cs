using Calendar.Models;
using Calendar.ViewModel;
using MyMVCWebsite.Filters;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Calendar.Controllers
{
    [AuthenticationAttribute]
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();

        // GET: ManageUsers
        public ActionResult Index()
        {
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UsersRolesViewModel()

                                  {
                                      UserId = p.UserId,
                                      UserName = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames),
                                  });


            return View(usersWithRoles);
        }
        
        // GET: ManageUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usersWithRoles = (from user in context.Users
                                  where user.Id == id
                                  select new
                                  {
                                      UserName = user.UserName,
                                      UserId = user.Id,
                                      RoleIds = (from userRole in user.Roles
                                                 join role in context.Roles on userRole.RoleId
                                                 equals role.Id
                                                 select role.Id).ToList(),
                                      Roles = (from role in context.Roles
                                               select new
                                               {
                                                   roleId = role.Id,
                                                   roleName = role.Name
                                               }).ToList()


                                  }).ToList().Select(t => new UsersRolesViewModel() { Roles = new MultiSelectList(t.Roles, "roleId", "roleName"), RolesId = t.RoleIds, UserName = t.UserName, UserId = t.UserId }).FirstOrDefault();

            if (usersWithRoles == null)
            {
                return HttpNotFound();
            }

            return View(usersWithRoles);
        }

        // POST: ManageUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RolesId,UserId")] UsersRolesViewModel pUrserRole)
        {
            if (ModelState.IsValid)
            {
                var userEnity = (from user in context.Users where user.Id == pUrserRole.UserId select user).FirstOrDefault();

                userEnity.Roles.Clear();

                var rolesEnity = context.Roles.Where(x => pUrserRole.RolesId.Contains(x.Id)).ToList();

                foreach (var item in rolesEnity)
                {
                    Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole userRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole();
                    userRole.RoleId = item.Id;
                    userRole.UserId = userEnity.Id;

                    userEnity.Roles.Add(userRole);

                }

                context.Entry(userEnity).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(pUrserRole);
        }
    }
}
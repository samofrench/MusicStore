using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MusicStore.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicStore.Startup))]
namespace MusicStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("canEditUsers"))
            {
                var role = new IdentityRole();
                role.Name = "canEditUsers";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "ADMIN_USER";
                user.Email = "ADMIN_USER";

                string userPwd = "ADMIN_PASSWORD";

                var chkUser = userManager.Create(user, userPwd);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "canEditUsers");
                }

            }
        }
    }
}

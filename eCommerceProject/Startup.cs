using eCommerceProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCommerceProject.Startup))]
namespace eCommerceProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private async void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<MyRole, long>(new RoleStore<MyRole, long, MyUserRole>(context));
            var UserManager = new UserManager<MyUser, long>(new UserStore<MyUser,MyRole,long,MyLogin,MyUserRole,MyClaim>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new MyRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new MyUser();
                user.UserName = "MyAdmin";
                user.Email = "admin@eCommerce.com";
                user.IsActive = true;
                user.IsDelete = false;
                string userPWD = "My@3Comm_2021";

                var chkUser = await UserManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");

                }
            }


            // creating Creating Client role     
            if (!roleManager.RoleExists("Client"))
            {
                var role = new MyRole();
                role.Name = "Client";
                roleManager.Create(role);

            }
        }
    }
}

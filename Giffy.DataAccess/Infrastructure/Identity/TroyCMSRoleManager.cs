using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Giffy.DataAccess.Infrastructure.Identity
{
    public class GiffyRoleManager : RoleManager<IdentityRole>
    {
        public GiffyRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static GiffyRoleManager Create(IdentityFactoryOptions<GiffyRoleManager> options, IOwinContext context)
        {
            var GiffyRoleManager = new GiffyRoleManager(new RoleStore<IdentityRole>(context.Get<GiffyIdentityContext>()));

            return GiffyRoleManager;
        }
    }
}

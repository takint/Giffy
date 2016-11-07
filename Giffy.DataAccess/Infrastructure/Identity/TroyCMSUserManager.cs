using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Infrastructure.Identity
{
    public class GiffyUserManager : UserManager<User>
    {
        public GiffyUserManager(IUserStore<User> store)
            : base(store)
        {
    }

    public static GiffyUserManager Create(IdentityFactoryOptions<GiffyUserManager> options, IOwinContext context)
    {
        var GiffyIdentityContext = context.Get<GiffyIdentityContext>();
        var GiffyUserManager = new GiffyUserManager(new UserStore<User>(GiffyIdentityContext));

        return GiffyUserManager;
    }
}
}

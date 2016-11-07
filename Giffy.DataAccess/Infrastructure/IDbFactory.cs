using System;

namespace Giffy.DataAccess.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        GiffyContext Init();
    }
}

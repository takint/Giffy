namespace Giffy.DataAccess.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        GiffyContext dbContext;

        public GiffyContext Init()
        {
            return dbContext ?? (dbContext = new GiffyContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

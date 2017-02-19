namespace DG.Haer.Data
{
    public class DbFactory : Disposable, IDbFactory
    {
        private readonly IDbProvider _dbProvider;

        private AppDbContext _dbContext;

        public DbFactory(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public AppDbContext Init()
        {
            return _dbContext ?? (_dbContext = new AppDbContext(_dbProvider));
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}

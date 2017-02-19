using DG.Haer.Domain;
using System.Data.Entity;

namespace DG.Haer.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IDbProvider _dbProvider;

        public IDbSet<Contact> Contacts { get; set; }

        public AppDbContext(IDbProvider dbProvider)
            : base(dbProvider.ConnectionString)
        {
            _dbProvider = dbProvider;
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactConfiguration());
        }
    }
}

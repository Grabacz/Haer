using DG.Haer.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DG.Haer.Data
{
    public abstract class EntityConfiguration<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public EntityConfiguration()
        {
            HasKey(x => x.Id);
        }
    }
}

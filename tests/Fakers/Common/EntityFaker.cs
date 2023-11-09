using Blanche.Domain.Common;
using Bogus;

namespace tests.Fakers.Common
{
    public class EntityFaker<TEntity> : Faker<TEntity> where TEntity : Entity
    {
        public EntityFaker(string locale = "nl") : base(locale)
        {
            RuleFor(r => r.Id, f => Guid.NewGuid());
        }

        public Faker<TEntity> AsTransient()
        {
            RuleFor(x => x.Id, f => default);
            return this;
        }
    }
}


using Blanche.Shared.Common;
using FluentValidation;

namespace Blanche.Shared.Products
{
    public class BeerDto : EntityDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }

        public BeerDto() { }

        public override bool Equals(object other)
        {
            return (other as BeerDto)?.Id == Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString() => Name;

        public class Validator : AbstractValidator<BeerDto>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
                RuleFor(x => x.Price).InclusiveBetween(0, 1000);
            }
        }
    }
}

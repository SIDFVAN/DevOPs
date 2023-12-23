using Blanche.Shared.Common;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Products
{
    public class BeerDto : EntityDto
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0, Double.MaxValue)]
        public double Price { get; set; }

        public BeerDto() { }

        public override bool Equals(object other)
        {
            return (other as BeerDto)?.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
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

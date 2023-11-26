using Blanche.Shared.Common;
using FluentValidation;

namespace Blanche.Shared.Formulas
{
    public class FormulaDto : EntityDto
    {
        public class Index
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = default!;
            public string Description { get; set; } = default!;
            public double Price { get; set; } 
            public string ImageUrl { get; set; } = string.Empty;
            public bool HasDrinks { get; set; }
            public bool HasFood { get; set; }
            public Dictionary<int, double> PricePerDays { get; set; } = default!;
            public double PricePerExtraDay { get; set; }

            public override bool Equals(object other)
            {
                return (other as Index)?.Id == Id;
            }

            public override int GetHashCode()
            {
                return this.Id.GetHashCode();
            }

            public override string ToString() => Name + " " + "(" + Description + ")";
        }

        public class Detail
        {
            public Guid Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int? Days { get; set; }
            public double? Price { get; set; }
            public string? ImageUrl { get; set; }
        }

        public class Mutate
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public double? Price { get; set; }
            public string? ImageUrl { get; set; }
            public int? Days { get; set; }
            public string? ImageContentType { get; set; }

            public class Validator : AbstractValidator<Mutate>
            {
                public Validator()
                {
                    RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
                    RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
                    RuleFor(x => x.Price).InclusiveBetween(0, 5000);
                }
            }
        }
    }
}
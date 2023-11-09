namespace Blanche.Shared.Formulas
{
    public class FormulaDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }

        public FormulaDto() { }

        public FormulaDto(string name, string desc, double price)
        {
            Name = name;
            Description = desc;
            Price = price;
        }

    }
}
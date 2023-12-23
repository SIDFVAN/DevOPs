using Blanche.Shared.Products;

namespace Blanche.Shared.Reservations
{
    [Serializable]
    public class ReservationLineDTO
    {
        public int Quantity { get; set; }
        public ProductDto Product { get; set; } = new ProductDto();

        public ReservationLineDTO(int quantity, ProductDto product)
        {
            Quantity = quantity;
            this.Product = product;
        }

        public double GetPrice()
        {
            return Quantity * Product.MinimumUnits;
        }
 
    }
}
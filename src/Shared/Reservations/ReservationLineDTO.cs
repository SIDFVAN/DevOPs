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

        public static ReservationLineDtoBuilder Builder()
        {
            return new ReservationLineDtoBuilder();
        }

        public class ReservationLineDtoBuilder
        {
            private int quantity;
            private ProductDto product = new ProductDto();

            public ReservationLineDtoBuilder WithQuantity(int quantity)
            {
                this.quantity = quantity;
                return this;
            }

            public ReservationLineDtoBuilder WithProduct(ProductDto product)
            {
                this.product = product;
                return this;
            }

            public ReservationLineDTO Build()
            {
                return new ReservationLineDTO(quantity, product);
            }
        }
    }
}
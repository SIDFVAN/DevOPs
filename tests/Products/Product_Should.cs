using Blanche.Domain.Formulas;
using Blanche.Domain.Products;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Products
{
    public class Product_Should
    {
        [Fact]
        public void Be_created_valid()
        {
            //Arrange we gaan de data klaarzetten om een nieuwe formule aan te maken
            string name = "Vuurschaal";
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = 1;
            double price = 40;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = 1;
            //Assert
            Product productTest = new (name, desc, quantity, price, imageUrl, minimumUnits);
            //Act
            productTest.Name.ShouldBe(name);
            productTest.Description.ShouldBe(desc);
            productTest.QuantityInStock.ShouldBe(quantity);
            productTest.Price.ShouldBe(price);
            productTest.ImageUrl.ShouldBe(imageUrl);
            productTest.MinimumUnits.ShouldBe(minimumUnits);
        }

        
        [Fact]
        public void Throw_when_invalid_Name()
        {
            string name = string.Empty;
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = 1;
            double price = 40;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = 1;
            Action act = () =>
            {
                Product productTest = new (name, desc, quantity, price, imageUrl, minimumUnits);
            };

            act.ShouldThrow<Exception>();
        }
        
        [Fact]
		public void Throw_when_invalid_Description()
		{
			string name = "Vuurschaal";
            string desc = string.Empty;
            int quantity = 1;
            double price = 40;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = 1;
            Action act = () =>
            {
                Product productTest = new (name, desc, quantity, price, imageUrl, minimumUnits);
            };

            act.ShouldThrow<Exception>();
        }

        
        [Fact]
        public void Throw_when_invalid_ImageUrl()
        {
            string name = "Vuurschaal";
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = 1;
            double price = 40;
            string imageUrl = string.Empty;
            int minimumUnits = 1;
            Action act = () =>
            {
                Product productTest = new(name, desc, quantity, price, imageUrl, minimumUnits);
            };

            act.ShouldThrow<Exception>();
        }
        
        [Fact]
        public void Throw_when_invalid_Quantity()
        {
            string name = "Vuurschaal";
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = -1;
            double price = 40;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = 1;
            Action act = () =>
            {
                Product productTest = new(name, desc, quantity, price, imageUrl, minimumUnits);
            };

            act.ShouldThrow<Exception>();
        }
        
        [Fact]
        public void Throw_when_invalid_Price()
        {
           string name = "Vuurschaal";
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = 1;
            double price = 0;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = 1;
            Action act = () =>
            {
                Product productTest = new(name, desc, quantity, price, imageUrl, minimumUnits);
            };

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Throw_when_invalid_MinimumUnits()
        {
            string name = "Vuurschaal";
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = 1;
            double price = 40;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = -1;
            Action act = () =>
            {
                Product productTest = new(name, desc, quantity, price, imageUrl, minimumUnits);
            };

            act.ShouldThrow<Exception>();
        }
        
        [Fact]
        public void Be_equal_to_another_formula_with_the_same_properties()
        {
            //Assert
            string name = "Vuurschaal";
            string desc = "Voor de sfeer en gezelligheid";
            int quantity = 1;
            double price = 40;
            string imageUrl = "/images/vuurschaal.jpg";
            int minimumUnits = 1;

            Product product1 = new(name, desc, quantity, price, imageUrl, minimumUnits);
            // Arrange create a formula with specific properties
            Product product2 = new(product1.Name, product1.Description, product1.QuantityInStock, product1.Price, product1.ImageUrl, product1.MinimumUnits);

            // Act & Assert
            product1.ShouldBeEquivalentTo(product2);
            //product1.GetHashCode().ShouldBe(product2.GetHashCode());
        }
        
    }
}

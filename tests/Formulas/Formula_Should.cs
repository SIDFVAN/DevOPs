using System;
using Shouldly;
using Blanche.Domain.Formulas;
using tests.Fakers.Formulas;
using Xunit.Abstractions;
using Blanche.Domain.Products;
using Bogus.DataSets;

namespace tests.Formulas
{
	public class Formula_Should
	{
        //wat willen we testen?

        //1. Het aanmaken van een formule, we willen de constructor testen om te zien of deze werkt met juiste parameter
        //alsook met verkeerde parameters
        // Een formula bevat:
        // - Name : korte titel van de formula
        // - Description: korte omschrijving van de formule
        // - Days : aantal dagen dat de formule gebruikt wordt???
        // - Price: de prijs van de formule
        // - ImageUrl = string die verwijst naar een foto

        [Fact]
		public void Be_created_valid()
		{
			//Arrange we gaan de data klaarzetten om een nieuwe formule aan te maken
			String name = "Gewoon Blanche";
			String desc = "Gewoon Blanche zonder boe of ba. Zonder bier, zonder eten, gewoom de caravan";
			int days = 1;
			double price = 250;
			String imageUrl = "/images/blablabla.jpg";
			//Assert
			Formula formulaTest = new Formula(name, desc, days, price, imageUrl);
			//Act
			formulaTest.Name.ShouldBe(name);
			formulaTest.Description.ShouldBe(desc);
			formulaTest.Days.ShouldBe(days);
			formulaTest.Price.ShouldBe(price);
			formulaTest.ImageUrl.ShouldBe(imageUrl);
		}

        [Fact]
        public void Throw_when_invalid_Name()
        {
            String name = string.Empty;
            String desc = "Gewoon Blanche zonder boe of ba. Zonder bier, zonder eten, gewoom de caravan";
            int days = 1;
            double price = 250;
            String imageUrl = "/images/blablabla.jpg";
            Action act = () =>
            {
                Formula forumula = new(name, desc, days, price, imageUrl);
            };

            act.ShouldThrow<Exception>();
        }

        [Fact]
		public void Throw_when_invalid_Description()
		{
			String name = "Gewoon Blanche";
            String desc = string.Empty;
            int days = 1;
            double price = 250;
            String imageUrl = "/images/blablabla.jpg";
            Action act = () =>
            {
                Formula forumula = new(name, desc, days,price, imageUrl);
            };

            act.ShouldThrow<Exception>();
        }


        [Fact]
        public void Throw_when_invalid_ImageUrl()
        {
            String name = "Gewoon Blanche";
            String desc = "Gewoon Blanche zonder boe of ba. Zonder bier, zonder eten, gewoom de caravan";
            int days = 1;
            double price = 250;
            String imageUrl = null;
            Action act = () =>
            {
                Formula forumula = new(name, desc, days, price, imageUrl);
            };

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Throw_when_invalid_Days()
        {
            String name = "Gewoon Blanche";
            String desc = "Gewoon Blanche zonder boe of ba. Zonder bier, zonder eten, gewoom de caravan";
            int days = 0;
            double price = 250;
            String imageUrl = string.Empty;
            Action act = () =>
            {
                Formula forumula = new(name, desc, days, price, imageUrl);
            };

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Throw_when_invalid_Price()
        {
            String name = "Gewoon Blanche";
            String desc = "Gewoon Blanche zonder boe of ba. Zonder bier, zonder eten, gewoom de caravan";
            int days = 1;
            double price = 0;
            String imageUrl = string.Empty;
            Action act = () =>
            {
                Formula forumula = new(name, desc, days, price, imageUrl);
            };

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Be_equal_to_another_formula_with_the_same_properties()
        {
            //Assert
            String name = "Gewoon Blanche";
            String desc = "Gewoon Blanche zonder boe of ba. Zonder bier, zonder eten, gewoom de caravan";
            int days = 1;
            double price = 250;
            String imageUrl = "/images/blablabla.jpg";
   
            Formula formula1 = new Formula(name, desc, days, price, imageUrl);
            // Arrange create a formula with specific properties
            Formula formula2 = new Formula(formula1.Name, formula1.Description, formula1.Days, formula1.Price, formula1.ImageUrl);

            // Act & Assert
            formula1.ShouldBeEquivalentTo(formula2);
            //formula1.GetHashCode().ShouldBe(formula2.GetHashCode());
        }


    }
}


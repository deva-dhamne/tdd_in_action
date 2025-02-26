
using FluentAssertions;
using ShoppingCart.Core;

namespace ShoppingCart.UnitTests
{
    public class Buy2Get1FreeDiscountShould
    {
        [Theory]
        [InlineData(3, 10, 20)]
        [InlineData(4, 10, 30)]
        [InlineData(5, 10, 40)]
        [InlineData(6, 10, 40)]
        public void Return_DiscountedPriceForItemsAboveQuantity3(int quantity, decimal price, decimal expectedDiscountedPrice)
        {
            var discount = new Buy2Get1FreeDiscount("Apple");

            var discountedPrice = discount.Apply(quantity, price);

            discountedPrice.Should().Be(expectedDiscountedPrice);
        }
        
        [Theory]
        [InlineData(1, 10, 10)]
        [InlineData(2, 10, 20)]
        public void Return_ActualTotalPriceForItemBelowQuantity3(int quantity, decimal price, decimal expectedDiscountedPrice)
        {
            var discount = new Buy2Get1FreeDiscount("Apple");

            var discountedPrice = discount.Apply(quantity, price);

            discountedPrice.Should().Be(expectedDiscountedPrice);
        }

        [Fact]
        public void ReturnTrue_ForApplicableItem()
        {
            var discount = new Buy2Get1FreeDiscount("Apple");

            Assert.True(discount.AppliesTo("Apple"));
        }

        [Fact]
        public void ReturnFalse_ForNonApplicableItem()
        {
            var discount = new Buy2Get1FreeDiscount("Apple");

            Assert.False(discount.AppliesTo("Orange"));
        }
    }
}
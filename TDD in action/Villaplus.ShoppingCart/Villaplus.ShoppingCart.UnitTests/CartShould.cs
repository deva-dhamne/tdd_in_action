using Moq;

namespace Villaplus.ShoppingCart.UnitTests
{
    public class CartShould
    {
        private readonly Mock<IDiscountService> _discountServiceMock;
        private readonly Cart _cart;

        public CartShould()
        {
            _discountServiceMock = new Mock<IDiscountService>();
            _cart = new Cart(_discountServiceMock.Object);
        }

        [Fact]
        public void AddItem_WhenValidQuantity()
        {
            var apple = new Product("Apple", 1.00m);

            _cart.AddItem(apple);
            _cart.AddItem(apple);

            Assert.Equal(2, _cart.Items.Single(x => x.Product.Name == "Apple").Quantity);
        }

        [Fact]
        public void ThrowInvalidQuantityException_WhenQuantityIsZero()
        {
            var apple = new Product("Apple", 1.00m);

            Assert.Throws<ArgumentException>(() => _cart.AddItem(apple, 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        [InlineData(-5)]
        public void ThrowInvalidQuantityException_WhenQuantityIsLessThanZero(int quantity)
        {
            var apple = new Product("Apple", 1.00m);

            Assert.Throws<ArgumentException>(() => _cart.AddItem(apple, quantity));
        }

        [Fact]
        public void GetTotal_WhenItemsInCart()
        {
            // Arrange
            var apple = new Product("Apple", 100m);
            var orange = new Product("Orange", 100m);

            var appleItem = new Item(apple, 3);
            var orangeItem = new Item(orange, 2);

            _cart.AddItem(apple, 3);
            _cart.AddItem(orange, 2);


            _discountServiceMock.Setup(s => s.CalculateTotal(
                It.Is<Item>(i => i.Product.Name == "Apple" && i.Quantity == 3)))
                .Returns(200m);

            _discountServiceMock.Setup(s => s.CalculateTotal(
                It.Is<Item>(i => i.Product.Name == "Orange" && i.Quantity == 2)))
                .Returns(180m);

            // Act
            var totalPrice = _cart.GetTotalPrice();

            // Assert
            Assert.Equal(380m, totalPrice);

            _discountServiceMock.Verify(s => s.CalculateTotal(
                It.Is<Item>(i => i.Product.Name == "Apple" && i.Quantity == 3)), Times.Once);

            _discountServiceMock.Verify(s => s.CalculateTotal(
                It.Is<Item>(i => i.Product.Name == "Orange" && i.Quantity == 2)), Times.Once);
        }
    }
}
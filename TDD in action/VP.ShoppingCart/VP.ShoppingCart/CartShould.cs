namespace VP.ShoppingCart
{
    public class CartShould
    {
        Cart _cart = new Cart();

        [Fact]
        public void AddItem_WhenValidQuantity()
        {
            var apple = new Product("Apple", 100m);

            _cart.AddItem(apple, 2);

            Assert.Equal(2, _cart.Items.Single(x => x.Product.Name == "Apple").Quantity);
        }

        [Fact]
        public void ThrowInvalidQuantityException_WhenQuantityIsZero()
        {
            var apple = new Product("Apple", 100m);

            Assert.Throws<ArgumentException>(() => _cart.AddItem(apple, 0));
        }

        [Fact]
        public void ThrowInvalidQuantityException_WhenQuantityIsNegative()
        {
            var apple = new Product("Apple", 100m);

            Assert.Throws<ArgumentException>(() => _cart.AddItem(apple, -1));
        }

        [Fact]
        public void GetTotalPrice_ForTheItemsInCart_WhenSingleProduct()
        {
            var apple = new Product("Apple", 100m);

            _cart.AddItem(apple, 1);

            var total = _cart.GetTotalPrice();

            Assert.Equal(total, 100m);
        }

        [Fact]
        public void GetTotalPrice_ForTheItemsInCart_WhenMultipleProducts()
        {
            var apple = new Product("Apple", 100m);
            var orange = new Product("Orange", 200m);

            _cart.AddItem(apple, 2);
            _cart.AddItem(orange, 2);

            var total = _cart.GetTotalPrice();

            Assert.Equal(total, 600m);
        }

        [Fact]
        public void GetDiscountedTotal_WhenAppleIsInCart()
        {
            var apple = new Product("Apple", 100m);

            _cart.AddItem(apple, 3);

            var total = _cart.GetTotalPrice();

            Assert.Equal(total, 200m);
        }
    }
}
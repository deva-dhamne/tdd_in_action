
using FluentAssertions;
using ShoppingCart.Core;

namespace ShoppingCart.UnitTests
{
    public class ShoppingCartShould
    {


        [Fact]
        public void AddItemToCart()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 2, 10);  

            Assert.Equal(1, cart._items.Count); 
            Assert.True(cart._items.First().Name == "Apple"); 
            Assert.True(cart._items.First().Quantity == 2); 
            Assert.True(cart._items.First().Price == 10); 
        }
        
        [Fact]
        public void Returns_DiscountedPrice_WhenDiscountApplicable()
        {
            // Arrange
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 3, 10);  
            cart.ApplyDiscount(new Buy2Get1FreeDiscount("Apple"));

            // Act
            var total = cart.CalculateTotal();

            // Assert
            Assert.Equal(20, total); 
        }

        [Fact]
        public void Returns_ActualPrice_WhenDiscountNotApplicable()
        {
            // Arrange
            var cart = new ShoppingCart();
            cart.AddItem("Orange", 3, 10);
            cart.ApplyDiscount(new Buy2Get1FreeDiscount("Apple"));

            // Act
            var total = cart.CalculateTotal();

            // Assert
            Assert.Equal(30, total);
        }
    }

    public class ShoppingCart
    {
        public List<CartItem> _items = new();
        private IBuy2Get1FreeDiscount _buy2Get1FreeDiscount;

        public void AddItem(string name, int quanity, decimal price)
        {
            _items.Add(new CartItem (name, quanity, price));
        }

        public void ApplyDiscount(IBuy2Get1FreeDiscount buy2Get1FreeDiscount)
        {
            _buy2Get1FreeDiscount = buy2Get1FreeDiscount;
        }

        public decimal CalculateTotal()
        {
            return _items.Sum(item =>
            {
                if (_buy2Get1FreeDiscount.AppliesTo(item.Name))
                {
                    return _buy2Get1FreeDiscount.Apply(item.Quantity, item.Price);
                }

                return item.Quantity * item.Price;
            });
        }
    }

    public class CartItem
    {
        public string Name { get; }

        public int Quantity { get; }
        
        public decimal Price { get; }

        public CartItem(string name, int quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}

namespace Villaplus.ShoppingCart
{
    public class Cart
    {
        private readonly List<Item> _items;
        private readonly IDiscountService _discountService;
        public IEnumerable<Item> Items => _items.AsReadOnly();

        public Cart(IDiscountService discountService)
        {
            _items = new List<Item>();
            _discountService = discountService;
        }

        public void AddItem(Product product, int quantity = 1)
        {
            Validate(quantity);

            var existingItem = _items.SingleOrDefault(item => item.Product.Name == product.Name);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                _items.Add(new Item(product, quantity));
            }
        }

        private void Validate(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity should be more than 0");
            }
        }

        public decimal GetTotalPrice()
        {
            return _items.Sum(item => _discountService.CalculateTotal(item));
        }
    }

    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Item(Product product, int quantity = 1)
        {
            Product = product;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        internal decimal GetTotalPrice()
        {
            return Product.Price * Quantity;
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
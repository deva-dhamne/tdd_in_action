namespace VP.ShoppingCart
{
    public class Cart
    {
        private readonly List<Item> _items;

        public IEnumerable<Item> Items => _items.AsReadOnly();

        public Cart()
        {
            _items = new List<Item>();
        }

        public void AddItem(Product product, int quantity)
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

        public decimal GetTotalPrice()
        {
            return _items.Sum(item => GetTotal(item));
        }

        public decimal GetTotal(Item item)
        {
            if (item.Product.Name == "Apple")
                return ApplyBuy2Get1Discount(item);

            return item.GetTotal();
        }

        private decimal ApplyBuy2Get1Discount(Item item)
        {
            int freeItems = item.Quantity / 3;
            int payableItems = item.Quantity - freeItems;
            return payableItems * item.Product.Price;
        }

        private void Validate(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException();
            }
        }
    }
}
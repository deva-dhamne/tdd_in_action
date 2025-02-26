namespace VP.ShoppingCart
{
    public class Item
    {
        public Item(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
namespace VP.ShoppingCart
{
    public class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;

            if (price < 0)
            {
                throw new ArgumentException();
            }

            Price = price;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
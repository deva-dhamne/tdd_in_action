namespace Villaplus.ShoppingCart
{
    public interface IDiscount
    {
        decimal Apply(Item product);
    }

    public class BuyTwoGetOneFree : IDiscount
    {
        public decimal Apply(Item item)
        {
            int freeItems = item.Quantity / 3;
            int payableItems = item.Quantity - freeItems;
            return payableItems * item.Product.Price;
        }
    }

    public class PercentageDiscount : IDiscount
    {
        private readonly decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal Apply(Item item)
        {
            decimal discount = item.Product.Price * (_percentage / 100);
            return (item.Product.Price - discount) * item.Product.Price;
        }
    }
}

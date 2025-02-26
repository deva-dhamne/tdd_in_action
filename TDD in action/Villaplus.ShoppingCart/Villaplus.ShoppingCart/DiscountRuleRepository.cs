namespace Villaplus.ShoppingCart
{
    public class DiscountRuleRepository
    {
        private readonly Dictionary<string, IDiscount> _dicounts;

        public DiscountRuleRepository()
        {
            _dicounts = new Dictionary<string, IDiscount>
        {
            { "Apple", new BuyTwoGetOneFree() },
            { "Orange", new PercentageDiscount(30) }
        };
        }

        public IDiscount GetDiscount(string productName)
        {
            if (_dicounts.TryGetValue(productName, out var discount))
            {
                return discount;
            }

            return null;
        }
    }
}

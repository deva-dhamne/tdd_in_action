namespace Villaplus.ShoppingCart
{
    public interface IDiscountService
    {
        decimal CalculateTotal(Item item);
    }

    public class DiscountService: IDiscountService
    {
        private readonly DiscountRuleRepository _ruleRepository;

        public DiscountService(DiscountRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public decimal CalculateTotal(Item item)
        {
            var discount = _ruleRepository.GetDiscount(item.Product.Name);

            if (discount != null)
            {
                return discount.Apply(item);
            }

            return item.GetTotalPrice();
        }
    }
}

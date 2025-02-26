namespace ShoppingCart.Core
{
    public interface IBuy2Get1FreeDiscount
    {
        bool AppliesTo(string itemName);

        decimal Apply(int quantity, decimal price);
    }

    public class Buy2Get1FreeDiscount: IBuy2Get1FreeDiscount
    {
        private readonly string _applicableItem;

        public Buy2Get1FreeDiscount(string applicableItem)
        {
            _applicableItem = applicableItem;
        }

        public decimal Apply(int quantity, decimal price)
        {
            var discountedQuantity = quantity / 3;

            return price * (quantity - discountedQuantity);
        }

        public bool AppliesTo(string itemName)
        {
            return itemName == _applicableItem;
        }
    }
}

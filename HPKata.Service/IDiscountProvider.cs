using HPKata.Service.Types;

namespace HPKata.Service
{
    public interface IDiscountProvider
    {
        public decimal GetProjectedDiscount(BookSet bookBundle, decimal bookCost);
    }
}
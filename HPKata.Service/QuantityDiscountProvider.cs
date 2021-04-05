using System;
using HPKata.Service.Types;

namespace HPKata.Service
{
    public class QuantityDiscountProvider : IDiscountProvider
    {
        public decimal GetProjectedDiscount(BookSet bookBundle, decimal bookCost)
        {
            if (bookBundle == null) throw new ArgumentNullException(nameof(bookBundle));

            return CalculateDiscountOnQuantity(bookBundle.Books.Count, bookBundle.GrossBundleCost, bookCost);
        }
        private decimal CalculateDiscountOnQuantity(int currentBundleBookCount, decimal currentBundleGrossCost, decimal newBookCost)
        {
            return (currentBundleBookCount) switch
            {
                1 => CalculateDiscountOfAdditionalBundledBook(5, currentBundleGrossCost + newBookCost),
                2 => CalculateDiscountOfAdditionalBundledBook(10, currentBundleGrossCost + newBookCost), 
                3 => CalculateDiscountOfAdditionalBundledBook(20, currentBundleGrossCost + newBookCost),
                4 => CalculateDiscountOfAdditionalBundledBook(25, currentBundleGrossCost + newBookCost),
                _ => throw new ArgumentOutOfRangeException(nameof(currentBundleBookCount))
            };
        }

        private decimal CalculateDiscountOfAdditionalBundledBook(int percentage, decimal grossCost)
        {
            var discountAmount = (grossCost / 100) * percentage;
            return grossCost - discountAmount;
        }
    }
}
using System;

namespace HPKata.Service
{
    public class DiscountProvider
    {
        public double GetProjectedDiscount(int currentBundleBookCount, double currentBundleGrossCost, double newBookCost)
        {
            
            //Added +1 to make code easier to read. Case statement will be the price of that bundle amount, eg case 4 will be price of a 4 book bundle!
            switch (currentBundleBookCount + 1)
            {
                case 1:
                    return newBookCost; // No Discount is applied to single books in a bundle.
                case 2:
                    return calculateDiscount(5, currentBundleGrossCost+newBookCost);
                case 3:
                    return calculateDiscount(10, currentBundleGrossCost+newBookCost);
                case 4:
                    return calculateDiscount(20, currentBundleGrossCost+newBookCost);
                case 5:
                    return calculateDiscount(25, currentBundleGrossCost+newBookCost);
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentBundleBookCount));
            }
        }

        private double calculateDiscount(int percentage, double grossCost)
        {
            var discountAmount = (grossCost / 100) * percentage;
            return grossCost - discountAmount;
        }
    }
}
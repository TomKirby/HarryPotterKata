using System.Diagnostics.CodeAnalysis;

namespace HPKata.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public class BundleCalculatorHelper
    {
        public BundleCalculatorHelper(double bookPrice)
        {
            _bookPrice = bookPrice;
        }

        private readonly double _bookPrice;
        
        private double _total;

        public BundleCalculatorHelper AddTwoBookBundle()
        {
            _total += CalculateDiscountCost(5,_bookPrice,2);
            return this;
        }

        public BundleCalculatorHelper AddThreeBookBundle()
        {
            _total += CalculateDiscountCost(10, _bookPrice, 3);
            return this;
        }

        public BundleCalculatorHelper AddFourBookBundle()
        {
            _total += CalculateDiscountCost(20, _bookPrice, 4);
            return this;
        }

        public BundleCalculatorHelper AddFiveBookBundle()
        {
            _total += CalculateDiscountCost(25, _bookPrice, 5);
            return this;
        }

        public BundleCalculatorHelper AddSingleBook()
        {
            _total += 8;
            return this;
        }


        private double CalculateDiscountCost(int percentage, double bookPrice, int quantity)
        {
            var cost = bookPrice * quantity;
            var discountAmount = (cost / 100) * percentage;

            return cost - discountAmount;
        }

        public double Calculate => _total;
    }
}
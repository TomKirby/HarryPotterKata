using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HPKata.Service;
using NUnit.Framework;

namespace HPKata.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class DiscountProviderTests
    {
        [Test]
        [TestCase(1,8,15.20)] // 2 Book Bundle
        [TestCase(2,16,21.60)] // 3 Book Bundle
        [TestCase(3,24,25.60)] // 4 Book Bundle
        [TestCase(4,32,30)] // 5 Book Bundle
        public void ShouldProvideCorrectDiscountCalculationBasedOnAmountOfBooks(int currentBookCount,double currentBundlePrice,double expectedBundlePrice)
        {
            var discountProvider = new DiscountProvider();

            var newBundleCost = discountProvider.GetProjectedDiscount(currentBookCount, currentBundlePrice, 8);

            newBundleCost.Should().Be(expectedBundlePrice);
        }

        [Test]
        public void ShouldProvideNoDiscountIfBundleOnlyHadOneBook()
        {
            var discountProvider = new DiscountProvider();

            var newBundleCost = discountProvider.GetProjectedDiscount(0, 8, 8);

            newBundleCost.Should().Be(8);
        }

        [Test]
        [TestCase(6)]
        [TestCase(-10)]
        public void ShouldThrowExceptionIfBundleIsOutsideOfDiscountRange(int bookCount)
        {
            var discountProvider = new DiscountProvider();

            Action act = () =>
            {
                discountProvider.GetProjectedDiscount(bookCount, 8, 8);
            };

            act.Should().Throw<ArgumentOutOfRangeException>()
               .And.ParamName.Should().Be("currentBundleBookCount");
        }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HPKata.Service;
using HPKata.Service.Types;
using NUnit.Framework;

namespace HPKata.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class QuantityDiscountProviderTests
    {
        [Test]
        [TestCase(1,8,15.20)] // 2 Book Bundle
        [TestCase(2,16,21.60)] // 3 Book Bundle
        [TestCase(3,24,25.60)] // 4 Book Bundle
        [TestCase(4,32,30)] // 5 Book Bundle
        public void ShouldProvideCorrectDiscountCalculationBasedOnAmountOfBooks(int currentBookCount,decimal currentBundlePrice,decimal expectedBundlePrice)
        {
            var discountProvider = new QuantityDiscountProvider();
            var bookSet = BookSetBuilder(currentBookCount, currentBundlePrice);

            var newBundleCost = discountProvider.GetProjectedDiscount(bookSet, 8);

            newBundleCost.Should().Be(expectedBundlePrice);
        }

        [Test]
        public void ShouldThrowExceptionIfBundleIsOutsideOfDiscountRange()
        {
            var discountProvider = new QuantityDiscountProvider();
            var bookSet = BookSetBuilder(6, 8);

            Action act = () =>
            {
                discountProvider.GetProjectedDiscount(bookSet, 8);
            };

            act.Should().Throw<ArgumentOutOfRangeException>()
               .And.ParamName.Should().Be("currentBundleBookCount");
        }

        [Test]
        public void ShouldThrowExceptionIfNoBundleDependencyIsProvided()
        {
            var discountProvider = new QuantityDiscountProvider();
            Action act = () =>
            {
                discountProvider.GetProjectedDiscount(null, 8);
            };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("bookBundle");
        }

        private BookSet BookSetBuilder(int bookQuantity, decimal bundlePrice)
        {
            var bookSet = new BookSet(new Book(bundlePrice, "volume 1")) {BundleTotal = bundlePrice};

            if (bookQuantity == 1) return bookSet;//No need to loop over adding more books if all we need is one book!

            for (int i = 1; i < bookQuantity; i++)
            {
                bookSet.Books.Add(new Book(0,"volume"));
            }

            return bookSet;
        }
    }
}
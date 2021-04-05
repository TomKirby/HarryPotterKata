using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.PortableExecutable;
using FluentAssertions;
using HPKata.Service.Types;
using NUnit.Framework;

namespace HPKata.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BookSetTypeTest
    {
        [Test]
        public void ShouldAddBookOnConstruction()
        {
            var bookSet = new BookSet(new Book(8.0, "Volume 1"));

            bookSet.Books.Should().HaveCount(1);
            bookSet.Books.Single().Cost.Should().Be(8.0);
            bookSet.Books.Single().Volume.Should().Be("Volume 1");
            bookSet.GrossBundleCost.Should().Be(8.0);
        }

        [Test]
        public void ShouldThrowExceptionOnMissingBookDependency()
        {
            Action act = () => { new BookSet(null); };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("book");
        }

        [Test]
        [TestCase(10.0,18.0)]
        [TestCase(10.5,18.5)]
        [TestCase(1000,1008)]
        [TestCase(2.27,10.27)]
        [TestCase(4.27,12.54,8.27)]

        public void ShouldReturnCorrectGrossCost(double newBookCost,double expected,double initialBookPrice = 8.0)
        {
            var bookSet = new BookSet(new Book(initialBookPrice,"volume 1"));

            bookSet.Books.Add(new Book(newBookCost,"volume 2"));

            bookSet.GrossBundleCost.Should().Be(expected);
        }
    }
}
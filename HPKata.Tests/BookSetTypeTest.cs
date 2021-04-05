using FluentAssertions;
using HPKata.Service.Types;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HPKata.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BookSetTypeTest
    {
        [Test]
        public void ShouldAddBookOnConstruction()
        {
            var bookSet = new BookSet(new Book(8, "Volume 1"));

            bookSet.Books.Should().HaveCount(1);
            bookSet.Books.Single().Cost.Should().Be(8);
            bookSet.Books.Single().Volume.Should().Be("Volume 1");
            bookSet.GrossBundleCost.Should().Be(8);
        }

        [Test]
        public void ShouldThrowExceptionOnMissingBookDependency()
        {
            Action act = () => { new BookSet(null); };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("book");
        }

        [Test]
        [TestCase(10.00,18.00,8)]
        [TestCase(10.5,18.5,8)]
        [TestCase(1000,1008,8)]
        [TestCase(2.27,10.27,8)]
        [TestCase(4.27,12.54,8.27)]

        public void ShouldReturnCorrectGrossCost(decimal newBookCost,decimal expected,decimal initialBookPrice)
        {
            var bookSet = new BookSet(new Book(initialBookPrice,"volume 1"));

            bookSet.Books.Add(new Book(newBookCost,"volume 2"));

            bookSet.GrossBundleCost.Should().Be(expected);
        }

        [Test]
        public void CheckForBookShouldReturnTrueIfBookIsAlreadyInBundle()
        {
            var bundle = new BookSet(new Book(1,"Volume 1"));
            bundle.Books.Add(new Book(1,"Volume 2"));

            bundle.Contains(new Book(1, "Volume 2")).Should().BeTrue();
        }

        [Test]
        public void AddMethodShouldAddBookToBundle()
        {
            var bundle = new BookSet(new Book(1,"Volume 1"));
            bundle.Add(new Book(1, "Volume 2"));

            bundle.Books.Should().HaveCount(2);
        }

        [Test]
        public void AddMethodShouldThrowExceptionOfBookNotProvided()
        {
            var bundle = new BookSet(new Book(1,"Volume 1"));

            Action act = () => { bundle.Add(null); };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("book");
        }
    }
}
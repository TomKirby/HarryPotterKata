using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using FluentAssertions;
using HPKata.Service.Types;
using NuGet.Frameworks;
using NUnit.Framework;

namespace HPKata.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BookTypeTests
    {
        [Test]
        public void BookShouldReturnCost()
        {
            var book = new Book(8.0,"Volume 1");

            book.Cost.Should().Be(8.0);
        }

        [Test]
        public void BookShouldReturnVolume()
        {
            var book = new Book(8,"Volume 1");

            book.Volume.Should().Be("Volume 1");
        }

        [Test]
        public void TwoIdenticalBooksShouldCompareCorrectly()
        {
            var book1 = new Book(8,"Volume 1");
            var book2 = new Book(8,"Volume 1");

            book1.Should().Be(book2);
        }

        [Test]
        public void BookTitleShouldBeRequired()
        {
            Action act = () =>
            {
                var book = new Book(8, null);
            };

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be("volume");
        }

        [Test]
        public void EqualityShouldReturnFalseIfNotSameType()
        {
            var book = new Book(1,"Volume 1");

            var newFakeBook = new {Cost = 8.0, Volume = "Volume 1" };

            book.Equals(newFakeBook).Should().BeFalse();
        }

        [TestCase(1, "Volume 1", 1, "Volume 1",true)]
        [TestCase(2, "Volume 2", 2, "Volume 2",true)]
        [TestCase(2, "misMatch", 2, "Volume 2",false)]
        [TestCase(22222, "Volume 2", 2, "Volume 2",false)]

        public void GetHashCodeShouldHashToSameOnlyWhenObjectsMatch(double price1, string volume1, double price2, string volume2,bool expected)
        {
            var book1 = new Book(price1, volume1);
            var book2 = new Book(price2, volume2);


            var hashesMatch = book1.GetHashCode() == book2.GetHashCode();
            hashesMatch.Should().Be(expected);
        }


    }
}


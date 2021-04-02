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
    }
}


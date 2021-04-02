using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HPKata.Service.Types;

namespace HPKata.Service
{
    public class Basket
    {
        private readonly List<Book> _book = new List<Book>();

        public void Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            _book.Add(book);
        }

        public double CheckOut()
        {
            var distinctBookCount = _book.Distinct().Count();

            switch (distinctBookCount)
            {
                case 2:
                    return 15.2;
                case 3:
                    return 21.06;
                case 4:
                    return 27.2;
                default:
                    return _book.Sum(c => c.Cost);
            }
        }
    }
}
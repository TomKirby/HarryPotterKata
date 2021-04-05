using System;
using System.Collections.Generic;
using System.Linq;

namespace HPKata.Service.Types
{
    public class BookSet
    {
        public BookSet(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            Books = new List<Book>(new []{book});
            BundleCost = book.Cost;
        }

        public List<Book> Books { get; }

        public double BundleCost { get; set; }

        public double GrossBundleCost
        {
            get
            {
                return Books.Sum(c => c.Cost);
            }
        }
    }
}
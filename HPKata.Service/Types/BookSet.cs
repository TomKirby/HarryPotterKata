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
            BundleTotal = book.Cost;
        }

        public List<Book> Books { get; }

        public decimal BundleTotal { get; set; }

        public decimal GrossBundleCost
        {
            get
            {
                return Books.Sum(c => c.Cost);
            }
        }


        public bool Contains(Book book)
        {
            return Books.Contains(book);
        }

        public void Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            Books.Add(book);
        }
    }
}
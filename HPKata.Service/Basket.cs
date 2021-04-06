using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HPKata.Service.Types;

namespace HPKata.Service
{
    public class Basket
    {
        public Basket(IDiscountProvider discountProvider)
        {
            _discountProvider = discountProvider ?? throw new ArgumentNullException(nameof(discountProvider));
        }

        private readonly IDiscountProvider _discountProvider;
        private readonly Dictionary<int,BookSet> _bundles = new Dictionary<int, BookSet>();
        private readonly List<Book> _basket = new List<Book>();

        public void Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _basket.Add(book);
        }

        public decimal CalculateTotal()
        {
            ProcessBasketContents();

            return GetCurrentBasketTotal();
        }

        private void ProcessBasketContents()
        {
            var sortedBooks = _basket.GroupBy(b => b)
                                     .OrderByDescending(v => v.Count())
                                     .SelectMany(b => b);
            
            foreach (var book in sortedBooks)
            {
                var basketBundlePrices = new Dictionary<int, decimal>();

                foreach (var (key, bundle) in _bundles.Where(b => !b.Value.Contains(book)))
                {
                    basketBundlePrices[key] = CalculateTotalWithNewBook(bundle, book);
                }

                if (basketBundlePrices.Any())
                {
                    var bestBundle = basketBundlePrices.OrderBy(c => c.Value).First().Key;
                    var bundle = _bundles[bestBundle];

                    bundle.BundleTotal = CalculateBundlePrice(bundle, book);
                    bundle.Add(book);
                }
                else
                {
                    _bundles.Add(_bundles.Count + 1, new BookSet(book));
                }
            }
        }
        
        private decimal GetCurrentBasketTotal()
        {
            return _bundles.Sum(c => c.Value.BundleTotal);
        }

        private decimal CalculateTotalWithNewBook(BookSet bundle, Book book)
        {
            return GetCurrentBasketTotal() - bundle.BundleTotal + CalculateBundlePrice(bundle, book);
        }

        private decimal CalculateBundlePrice(BookSet bundle, Book book)
        {
            return _discountProvider.GetProjectedDiscount(bundle, book.Cost);
        }
    }
}
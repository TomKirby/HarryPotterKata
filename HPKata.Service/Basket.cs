using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HPKata.Service.Types;

namespace HPKata.Service
{
    public class Basket
    {
        public Basket(DiscountProvider discountProvider)
        {
            _discountProvider = discountProvider ?? throw new ArgumentNullException(nameof(discountProvider));
        }

        private readonly DiscountProvider _discountProvider;
        private readonly Dictionary<int,BookSet> _bundles = new Dictionary<int, BookSet>();

        public void Add(Book book)
        {
            var idealBundleId = 0;
            var newBundlePrice = double.MaxValue;
            var suitableBundleFound = false;
            
            if (book == null) throw new ArgumentNullException(nameof(book));

            foreach (var (key, bundle) in _bundles)
            {
                if(bundle.Books.Contains(book)) continue; //Skip this bundle if its already got that book!

                var projectedDiscount = CalculateProjectedDiscountCost(bundle, book.Cost);

                if (projectedDiscount < newBundlePrice)
                {
                    //found a bundle to place this book in which will yield the most discount.
                    idealBundleId = key;
                    newBundlePrice = projectedDiscount;
                    suitableBundleFound = true;
                }
            }

            if (suitableBundleFound)
            {
                _bundles[idealBundleId].Books.Add(book);
                _bundles[idealBundleId].BundleCost = newBundlePrice;
            }
            else
            {
                _bundles.Add(_bundles.Count + 1,new BookSet(book));
            }
        }

        private double CalculateProjectedDiscountCost(BookSet bundle, double bookCost)
        {
            return _discountProvider.GetProjectedDiscount(bundle.Books.Count,bundle.GrossBundleCost, bookCost);
        }

        public double CalculateTotal()
        {
            return _bundles.Sum(c => c.Value.BundleCost);
        }
    }
}
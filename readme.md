## Description 
-----------------------------
A book shop is running a promotion on the first five books of the Harry Potter series. One copy of any of the five books costs 8 EUR. If, however, you buy two different books from the series, you get a 5% discount on those two books. If you buy 3 different books, you get a 10% discount. With 4 different books, you get a 20% discount. If you go the whole hog, and buy all 5, you get a huge 25% discount. Note that if you buy, say, four books, of which 3 are different titles, you get a 10% discount on the 3 that form part of a set, but the fourth book still costs 8 EUR. 

Produce a solution to calculate the price of any conceivable shopping basket, giving as big a discount as possible. The solution must include a set of tests to prove that it fulfils the requirements, but needn’t be comprehensive – input validation tests, for example, can just be indicative. 

## Example 
-----------------------------
### How much does this basket of books cost? 

- 2 copies of the first book 
- 2 copies of the second book 
- 2 copies of the third book 
- 1 copy of the fourth book 
- 1 copy of the fifth book 

### Answer: 

```
  (4 * 8) - 20% [first book, second book, third book, fourth book] 
+ (4 * 8) - 20% [first book, second book, third book, fifth book] 

= 51.20 
```
using System;

namespace HPKata.Service.Types
{
    public class Book
    {
        public Book(double cost, string volume)
        {
            Cost = cost;
            Volume = volume ?? throw new ArgumentNullException(nameof(volume));
        }

        public double Cost { get; }
        public string Volume { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Book))return false;
 
            var other = (Book) obj;

            if (Cost != other.Cost || Volume != other.Volume)
            {
                return false;
            }

            return true;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Cost, Volume);
        }
    }
}
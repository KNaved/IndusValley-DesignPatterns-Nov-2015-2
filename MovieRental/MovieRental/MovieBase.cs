namespace MovieRental
{
    public abstract class MovieBase
    {
        public abstract string Title { get; set; }
        public abstract double GetAmount(int daysRented);
        public abstract int GetFrequentRenterPoints(int daysRented);
    }
}
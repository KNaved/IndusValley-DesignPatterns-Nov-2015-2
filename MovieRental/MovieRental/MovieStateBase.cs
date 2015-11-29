namespace MovieRental
{
    public abstract class MovieStateBase
    {
        public static int PriceCode { get; set; }

        public virtual int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }
        public abstract double GetAmount(int daysRented);
    }
}
namespace MovieRental
{
    public class NewReleaseMovieState : MovieStateBase
    {
        private static int _priceCode = 1;

        public static int PriceCode
        {
            get { return _priceCode; }

        }
        public int GetFrequentRenterPoints(int daysRented)
        {
            if (daysRented > 1) return 2;
            return 1;
        }

        public override double GetAmount(int daysRented)
        {
            return daysRented * 3;
        }
    }
}
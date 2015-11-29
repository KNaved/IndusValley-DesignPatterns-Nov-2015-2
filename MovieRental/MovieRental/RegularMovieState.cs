namespace MovieRental
{
    public class RegularMovieState : MovieStateBase
    {
        private static int _priceCode = 0;

        public  static int PriceCode
        {
            get { return _priceCode; }

        }
        public int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }

        public override double GetAmount(int daysRented)
        {
            double thisAmount = 2;
            if (daysRented > 2)
                thisAmount += (daysRented - 2) * 1.5;
            return thisAmount;
        }
    }
}
namespace MovieRental
{
    public class ChildrensMovieState : MovieStateBase
    {
        private static int _priceCode = 2;

        public static int PriceCode
        {
            get { return _priceCode; }
            
        }

        

        public int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }

        public override double GetAmount(int daysRented)
        {
            double thisAmount = 1.5;
            if (daysRented > 3)
                thisAmount += (daysRented - 3) * 1.5;
            return thisAmount;
        }
    }
}
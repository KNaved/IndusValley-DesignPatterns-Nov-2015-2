namespace MovieRental
{
    public class NullMovieState : MovieStateBase
    {
        public override double GetAmount(int daysRented)
        {
            return 0;
        }

        public override int GetFrequentRenterPoints(int daysRented)
        {
            return 0;
        }
    }
}
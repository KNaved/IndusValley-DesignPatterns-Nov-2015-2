namespace MovieRental
{
    public class NullMovie : MovieBase
    {
        public override string Title
        {
            get { return ""; }
            set { }
        }

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
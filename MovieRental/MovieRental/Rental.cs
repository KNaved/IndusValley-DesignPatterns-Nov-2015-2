namespace MovieRental
{
    class Rental
    {
        private Movie _movie;
        private int _daysRented;

        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }
        public int getDaysRented()
        {
            return _daysRented;
        }
        public Movie getMovie()
        {
            return _movie;
        }

        public double GetAmount()
        {
            return getMovie().GetAmount(getDaysRented());
        }

        public int GetFrequentRenterPoints()
        {
            return getMovie().GetFrequentRenterPoints(getDaysRented());
        }
    }
}
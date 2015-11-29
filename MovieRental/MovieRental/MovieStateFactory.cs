namespace MovieRental
{
    public class MovieStateFactory : IMovieStateFactory
    {
        public MovieStateBase CreateMovieState(int priceCode)
        {
            if (priceCode == ChildrensMovieState.PriceCode) return new ChildrensMovieState();
            if (priceCode == RegularMovieState.PriceCode) return new RegularMovieState();
            if (priceCode == NewReleaseMovieState.PriceCode) return new NewReleaseMovieState();
            return new NullMovieState();
        }
    }
}
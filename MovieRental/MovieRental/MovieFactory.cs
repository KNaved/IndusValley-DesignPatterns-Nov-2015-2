namespace MovieRental
{
    public class MovieFactory
    {
        private readonly IMovieStateFactory _movieStateFactory;

        public MovieFactory(IMovieStateFactory movieStateFactory)
        {
            _movieStateFactory = movieStateFactory;
        }

        public MovieBase Create(string title, int priceCode)
        {
            var movieState = _movieStateFactory.CreateMovieState(priceCode);
            return new Movie(title, priceCode, movieState);
        }
    }
}
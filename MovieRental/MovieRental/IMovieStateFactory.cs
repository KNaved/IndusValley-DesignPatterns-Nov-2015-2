namespace MovieRental
{
    public interface IMovieStateFactory
    {
        MovieStateBase CreateMovieState(int priceCode);
    }
}
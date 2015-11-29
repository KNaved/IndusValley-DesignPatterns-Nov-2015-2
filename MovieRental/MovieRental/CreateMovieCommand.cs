namespace MovieRental
{
    public class CreateMovieCommand : ICommand
    {
        private readonly MovieFactory _movieFactory;
        private readonly MovieConsole _movieConsole;

        public CreateMovieCommand(MovieFactory movieFactory, MovieConsole movieConsole)
        {
            _movieFactory = movieFactory;
            _movieConsole = movieConsole;
        }


        public void Execute()
        {
            var name = _movieConsole.GetName();
            int priceCode = _movieConsole.GetPriceCode();
            var movie = _movieFactory.Create(name, priceCode);
        }
    }
}
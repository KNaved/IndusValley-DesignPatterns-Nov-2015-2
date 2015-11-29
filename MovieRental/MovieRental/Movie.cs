using System;
using System.Runtime.Remoting.Messaging;

namespace MovieRental
{
    public interface ICommand
    {
        void Execute();
    }

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

    public class MovieConsole
    {
        public string GetName()
        {
            Console.WriteLine("Enter the movie name");
            return Console.ReadLine();
        }

        public int GetPriceCode()
        {
            Console.WriteLine("Enter the price code");
            return int.Parse(Console.ReadLine());

        }
    }

    public abstract class MovieStateBase
    {
        public static int PriceCode { get; set; }

        public virtual int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }
        public abstract double GetAmount(int daysRented);
    }

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

    public interface IMovieStateFactory
    {
        MovieStateBase CreateMovieState(int priceCode);
    }

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

    public class MovieFactory
    {
        private readonly IMovieStateFactory _movieStateFactory;

        public MovieFactory(IMovieStateFactory movieStateFactory)
        {
            _movieStateFactory = movieStateFactory;
        }

        public Movie Create(string title, int priceCode)
        {
            var movieState = _movieStateFactory.CreateMovieState(priceCode);
            return new Movie(title, priceCode, movieState);
        }
    }

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

    public abstract class MovieBase
    {
        public abstract string Title { get; set; }
        public abstract double GetAmount(int daysRented);
        public abstract int GetFrequentRenterPoints(int daysRented);
    }

    public class Movie : MovieBase {

        
        private MovieStateBase _movieState;
   
        
        private string _title;
        private readonly int _priceCode;

        public MovieStateBase MovieState
        {
            set
            {
                _movieState = value;
            }
            get { return _movieState; }
        }

        public Movie(String title, int priceCode, MovieStateBase movieState)
        {
            _title = title;
            _priceCode = priceCode;
            _movieState = movieState;
        }

        public int PriceCode
        {
            get { return _priceCode; }
        }

        public override string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public override double GetAmount(int daysRented)
        {
            return this._movieState.GetAmount(daysRented);
        }

        public override int GetFrequentRenterPoints(int daysRented)
        {
            return this._movieState.GetFrequentRenterPoints(daysRented);
        }
    }
}
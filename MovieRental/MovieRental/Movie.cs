using System;
using System.Runtime.Remoting.Messaging;

namespace MovieRental
{
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
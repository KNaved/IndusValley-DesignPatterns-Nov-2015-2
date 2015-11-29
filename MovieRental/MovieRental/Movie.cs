using System;

namespace MovieRental
{
    public class Movie {

        public const  int  CHILDRENS = 2;
        public const  int  REGULAR = 0;
        public const  int  NEW_RELEASE = 1;

   
        private int _priceCode;
        private string _title;

        public Movie(String title, int priceCode) {
            _title = title;
            _priceCode = priceCode;
        }

					
        public int PriceCode {
            get { return _priceCode; }
            set { _priceCode = value; }
        }


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental
{
    class Customer
    {
        private String _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(String name)
        {
            _name = name;
        }

    

        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public String getName()
        {
            return _name;
        }

        public String statement()
        {
            String result = _rentals.Aggregate(("Rental Record for " + getName() + "\n"),
                (repResult, r) => repResult += "\t" + r.getMovie().Title + "\t" + r.GetAmount() + "\n");

            result += "Amount owed is " + GetTotalAmount() + "\n";
            result += "You earned " + GetTotalFrequentRenterPoints() + " frequent renter points";
            return result;

        }

        public double GetTotalAmount()
        {
            return _rentals.Sum(r => r.GetAmount());
        }

        public int GetTotalFrequentRenterPoints()
        {
            return _rentals.Sum(r => r.GetFrequentRenterPoints());
        }
    }
}
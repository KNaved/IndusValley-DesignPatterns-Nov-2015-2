using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MovieRental
{
    class Program
    {
        static void Main(string[] args)
        {

          /*  var customer = new Customer("Magesh");
            customer.RentalAdded += (o, e) => Console.WriteLine("A new rental is added");
            customer.addRental(new Rental(new Movie("Harry Potter", 0, new ChildrensMovieState()),2 ));*/

            var movieFactory = new MovieFactory(new MovieStateFactory());
            var movieConsole = new MovieConsole();
            var createMovieCommand = new CreateMovieCommand(movieFactory, movieConsole);
            createMovieCommand.Execute();
            Console.ReadLine();
        }
    }
}

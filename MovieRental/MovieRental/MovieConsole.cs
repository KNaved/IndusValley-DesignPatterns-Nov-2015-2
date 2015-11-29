using System;

namespace MovieRental
{
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
}
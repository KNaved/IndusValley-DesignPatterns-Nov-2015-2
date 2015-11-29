using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _01_GreetingApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the name:");
            var name = Console.ReadLine();
            
            Console.ReadLine();
        }
    }

    public interface ITimeService
    {
        DateTime GetTime();
    }

    public class TimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }


    public class Greeter
    {
        private readonly ITimeService _timeService;

        public Greeter(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public string Greet(string name)
        {
            if (_timeService.GetTime().Hour < 12)
            {
                return string.Format("Hi {0}, Good Morning", name);
            }
            return string.Format("Hi {0}, Good Evening", name);
        }
    }
}

/*
          internal class Program{
            private static void Main(string[] args)
            {
                var greeter = new Greeter();
                Program.Run(greeter);
             }
         static void Run(IGreeter greeter)
        {
            Console.WriteLine("Enter you name:");
            var name = Console.ReadLine();
            var greetMsg = greeter.Greet(name);
            Console.WriteLine(greetMsg);
            Console.ReadLine();
        }
    }

    public interface IGreeter
    {
        string Greet(string name);
    }

    class Greeter : IGreeter
    {
        public string Greet(string name)
        {
            var greetMsg = "";
            if (DateTime.Now.Hour < 12)
            {
                greetMsg = string.Format("Hi {0}, Good Morning!", name);
            }
            else
            {
                greetMsg = string.Format("Hi {0}, Good Evening", name);
            }
            return greetMsg;
        } 
    }

    class AdvancedGreeter : IGreeter
    {
        public string Greet(string name)
        {
            var currentHour = DateTime.Now.Hour;
            if (currentHour < 12) return string.Format("Hi {0}, Good Morning!", name);
            if (currentHour < 18) return string.Format("Hi {0}, Good Afternoon!", name);
            if (currentHour < 20) return string.Format("Hi {0}, Good Evening!", name);
            return string.Format("Hi {0}, Good Night!", name);
        }
    }
}
*/
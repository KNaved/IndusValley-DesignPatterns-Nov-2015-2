using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _01_GreetingApp;

namespace GreetingAppTests
{
    public class MorningTimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return new DateTime(2015, 11, 11, 9,0,0);
        }
    }

    public class EveningTimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return new DateTime(2015, 11, 11, 17, 0, 0);
        }
    }

    [TestClass]
    public class GreeterTests
    {
        [TestMethod]
        public void When_Greeted_Before_12_Greets_GoodMorning()
        {
            //Arrange
            var timeService = new MorningTimeService();
            var greeter = new Greeter(timeService);
            var name = "Magesh";
            var expectedResult = "Hi Magesh, Good Morning";
            //Act
            var greetMsg = greeter.Greet(name);

            //Assert
            Assert.AreEqual(expectedResult, greetMsg);
        }

        [TestMethod]
        public void When_Greeted_After_12_Greets_GoodEvening()
        {
            //Arrange
            var timeService = new EveningTimeService();
            var greeter = new Greeter(timeService);
            var name = "Magesh";
            var expectedResult = "Hi Magesh, Good Evening";
            //Act
            var greetMsg = greeter.Greet(name);

            //Assert
            Assert.AreEqual(expectedResult, greetMsg);
        }
    }
}

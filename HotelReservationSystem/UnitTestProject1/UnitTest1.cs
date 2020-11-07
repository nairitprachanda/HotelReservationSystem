using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelReservationSystem;


namespace HotelReservationSystemTestProject
{
    [TestClass]
    public class UnitTest1
    {
        HotelSystem hotelSystem = new HotelSystem();
        //1
        [TestMethod]
        public void TestMethod1()
        {
            string hotelName = "Lakewood";
            int ratesForRegularCustomer = 110;
            Hotel hotel = new Hotel(hotelName, ratesForRegularCustomer);
            hotelSystem.AddHotel(hotel);
            Assert.AreEqual("Lakewood", hotelSystem.hotelList[0].name);
        }
        //2
        [TestMethod]
        public void GivenHotelOptionsReturnCheapestHotel()
        {
            hotelSystem.AddHotel(new Hotel("Lakewood", 110));
            hotelSystem.AddHotel(new Hotel("Bridgewood", 160));
            hotelSystem.AddHotel(new Hotel("Ridgewood", 220));
            string[] dates = "10Dec2020,11Dec2020".Split(",");
            Hotel cheapestHotel = hotelSystem.GetCheapestHotel(dates);
            Assert.AreEqual("Lakewood", cheapestHotel.name);
        }
        //3
        [TestMethod]
        public void GivenWeekendAndWeekdayRatesReturnCheapestHotel()
        {
            hotelSystem.AddHotel(new Hotel("Lakewood", 110, 90));
            hotelSystem.AddHotel(new Hotel("Bridgewood",160, 50));
            hotelSystem.AddHotel(new Hotel("Ridgewood", 220, 150));
            Assert.AreEqual(90, hotelSystem.hotelList[0].weekendRatesForRegularCustomer);
        }
        //4
        [TestMethod]
        public void GivenWeekendAndWeekdayRateReturnCheapestHotel()
        {
            hotelSystem.AddHotel(new Hotel("Lakewood", 110, 90));
            hotelSystem.AddHotel(new Hotel("Bridgewood", 150, 50));
            hotelSystem.AddHotel(new Hotel("Ridgewood", 220, 150));
            string[] dates = "11Sep2020,12Sep2020".Split(",");
            DateTime[] date = new DateTime[2];
            date[0] = DateTime.Parse(dates[0]);
            date[1] = DateTime.Parse(dates[1]);
            Hotel cheapestHotel = hotelSystem.GetCheapestHotel(date);
            //Console.WriteLine(cheapestHotel.name);
            Assert.AreEqual("Bridgewood", cheapestHotel.name);
        }

        //5
        [TestMethod]
        public void GivenRatingReturnRequiredRating()
        {
            hotelSystem.AddHotel(new Hotel("Lakewood", 3, 110, 90));
            hotelSystem.AddHotel(new Hotel("Bridgewood",4, 150, 50));
            hotelSystem.AddHotel(new Hotel("Ridgewood", 5, 220, 150));
            Assert.AreEqual(3, hotelSystem.hotelList[0].rating);
        }
        //6
        [TestMethod]
        public void GivenWeekendAndWeekdayRateReturnCheapestHotelAlongWithRating()
        {
            hotelSystem.AddHotel(new Hotel("Lakewood", 3, 110, 90));
            hotelSystem.AddHotel(new Hotel("Bridgewood", 4, 150, 50));
            hotelSystem.AddHotel(new Hotel("Ridgewood", 5, 220, 150));
            string[] dates = "11Sep2020,12Sep2020".Split(",");
            DateTime[] date = new DateTime[2];
            date[0] = DateTime.Parse(dates[0]);
            date[1] = DateTime.Parse(dates[1]);
            Hotel cheapestHotel = hotelSystem.GetCheapestHotelWithBestRating(date);
            Assert.AreEqual(4, cheapestHotel.rating);
        }
    }
}
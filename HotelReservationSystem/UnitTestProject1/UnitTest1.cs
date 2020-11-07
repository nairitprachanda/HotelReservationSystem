using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelReservationSystem;
namespace HotelReservationSystemTestProject
{
    [TestClass]
    public class UnitTest1
    {
        HotelSystem hotelSystem = new HotelSystem();
        [TestMethod]
        public void TestMethod1()
        {
            string hotelName = "Lakewood";
            int ratesForRegularCustomer = 110;
            Hotel hotel = new Hotel(hotelName, ratesForRegularCustomer);
            hotelSystem.AddHotel(hotel);
            Assert.AreEqual("Lakewood", hotelSystem.hotelList[0].name);
        }
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
        [TestMethod]
        public void GivenWeekendAndWeekdayRatesReturnCheapestHotel()
        {
            hotelSystem.AddHotel(new Hotel("Lakewood", 110, 90));
            hotelSystem.AddHotel(new Hotel("Bridgewood",160, 50));
            hotelSystem.AddHotel(new Hotel("Ridgewood", 220, 150));
            Assert.AreEqual(90, hotelSystem.hotelList[0].weekendRatesForRegularCustomer);
        }
    }
}
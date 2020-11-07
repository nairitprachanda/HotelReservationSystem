﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace HotelReservationSystem
{
    public class HotelSystem
    {
        public List<Hotel> hotelList;
        CheckDate checkDate = new CheckDate();
        public HotelSystem()
        {
            hotelList = new List<Hotel>();
        }
        public void AddHotel(Hotel hotel)
        {
            hotelList.Add(hotel);
        }
        public Hotel GetCheapestHotel(string[] dates)
        {
            DateTime[] validatedDates = checkDate.ValidateAndReturnDates(dates);
            hotelList.Sort((e1, e2) => e1.weekdayRatesForCustomer.CompareTo(e2.weekdayRatesForCustomer));
            return hotelList[0];
        }
        public Dictionary<Hotel, double> CalculatePriceOfStay(DateTime[] dates)
        {
            double noOfWeekend = 0;
            double noOfWeekday = 0;
            Dictionary<Hotel, double> listOfHotelAndPrice = new Dictionary<Hotel, double>();
            foreach (var date in dates)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    noOfWeekend++;
            }
            TimeSpan timeSpan = dates[1].Subtract(dates[0]);
            noOfWeekday = timeSpan.TotalDays - noOfWeekend + 1;
            foreach (var hotel in hotelList)
            {
                double priceDuringStay = hotel.weekdayRatesForCustomer * noOfWeekday + hotel.weekendRatesForCustomer * noOfWeekend;
                listOfHotelAndPrice.Add(hotel, priceDuringStay);
            }
            return listOfHotelAndPrice;
        }
        public Hotel GetCheapestHotel(DateTime[] dates)
        {
            Dictionary<Hotel, double> cheapestHotelAndPrice = CalculatePriceOfStay(dates);
            var sortedListOfHotelAndPriceDuringGivenDate = cheapestHotelAndPrice.OrderBy(x => x.Value);
            return sortedListOfHotelAndPriceDuringGivenDate.ElementAt(0).Key;
        }
        public Hotel GetCheapestHotelWithBestRating(DateTime[] dates)
        {
            Dictionary<Hotel, double> HotelAndPrice = CalculatePriceOfStay(dates);
            var sortedListOfHotelAndPriceDuringGivenDateByRate = HotelAndPrice.OrderBy(x => x.Value);
            var sortedListOfHotelAndPriceDuringGivenDateByRateAndRating = HotelAndPrice.OrderByDescending(x => x.Key.rating);
            return sortedListOfHotelAndPriceDuringGivenDateByRateAndRating.ElementAt(0).Key;
        }
        public Hotel GetHotelWithBestRating(DateTime[] dates)
        {
            Dictionary<Hotel, double> cheapestHotelAndPrice = CalculatePriceOfStay(dates);
            var sortedListOfHotelDependingOnRating = cheapestHotelAndPrice.OrderByDescending(x => x.Key.rating);
            return sortedListOfHotelDependingOnRating.ElementAt(0).Key;
        }
    }
}
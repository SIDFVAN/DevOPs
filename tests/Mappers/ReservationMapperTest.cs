using Blanche.Domain.Customers.Mappers;
using Blanche.Domain.Customers;
using Blanche.Shared.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Fakers.Customer;
using tests.Fakers.Reservations;
using Blanche.Domain.Reservations;
using Blanche.Mappers.Reservations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blanche.Shared.Reservations;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace tests.Mappers
{
    public class ReservationMapperTest
    {

        public static IEnumerable<object[]> ReservationTestData()
        {

            yield return new object[] {
                new ReservationFaker().Generate()};

        }

        public static IEnumerable<object[]> ReservationDtoTestData()
        {

            yield return new object[] {
                new ReservationDtoFaker().Generate()};

        }

        [Theory]
        [MemberData(nameof(ReservationTestData))]
        public void ToReservationDto_ShouldMapCorrectly(Reservation reservation)
        {
 
            // Act
            ReservationDto reservationDto = ReservationMapper.ReservationToReservationDto(reservation);

            // Assert 
            Assert.AreEqual(reservation.StartDate, reservationDto.StartDate);
            Assert.AreEqual(reservation.EndDate, reservationDto.EndDate);
            Assert.AreEqual(reservation.TotalPrice, reservationDto.TotalPrice);
            Assert.AreEqual(reservation.IsConfirmed, reservationDto.IsConfirmed);
            Assert.AreEqual(reservation.NumberOfPersons, reservationDto.NumberOfPersons);

            // Assert on customer data
            Assert.AreEqual(reservation.Customer.FirstName, reservationDto.Customer.FirstName);
            Assert.AreEqual(reservation.Customer.LastName, reservationDto.Customer.LastName);
            Assert.AreEqual(reservation.Customer.PhoneNumber, reservationDto.Customer.PhoneNumber);
            Assert.AreEqual(reservation.Customer.Email.Value, reservationDto.Customer.Email.Value);

            Assert.AreEqual(reservation.Customer.CustomerAddress.Country, reservationDto.Customer.CustomerAddress.Country);
            Assert.AreEqual(reservation.Customer.CustomerAddress.PostalCode, reservationDto.Customer.CustomerAddress.PostalCode);
            Assert.AreEqual(reservation.Customer.CustomerAddress.Number, reservationDto.Customer.CustomerAddress.Number);
            Assert.AreEqual(reservation.Customer.CustomerAddress.City, reservationDto.Customer.CustomerAddress.City);

            // Assert on formula
            //Assert.AreEqual(reservation.Formula, reservationDto.Formula);

            CollectionAssert.AreEqual(reservation.Items.ToList(), reservationDto.Items.ToList());
             

        }

        [Theory]
        [MemberData(nameof(ReservationDtoTestData))]
        public void ToReservation_ShouldMapCorrectly(ReservationDto reservationDto)
        {
            // Act
            Reservation reservation = ReservationMapper.ReservationDtoToReservation(reservationDto);

            // Assert 
            Assert.AreEqual(reservationDto.StartDate, reservation.StartDate);
            Assert.AreEqual(reservationDto.EndDate, reservation.EndDate);
            Assert.AreEqual(reservationDto.TotalPrice, reservation.TotalPrice);
            Assert.AreEqual(reservationDto.IsConfirmed, reservation.IsConfirmed);
            Assert.AreEqual(reservationDto.NumberOfPersons, reservation.NumberOfPersons);

            // Assert on customer data
            Assert.AreEqual(reservationDto.Customer.FirstName, reservation.Customer.FirstName);
            Assert.AreEqual(reservationDto.Customer.LastName, reservation.Customer.LastName);
            Assert.AreEqual(reservationDto.Customer.PhoneNumber, reservation.Customer.PhoneNumber);
            Assert.AreEqual(reservationDto.Customer.Email.Value, reservation.Customer.Email.Value);

            Assert.AreEqual(reservationDto.Customer.CustomerAddress.Country, reservation.Customer.CustomerAddress.Country);
            Assert.AreEqual(reservationDto.Customer.CustomerAddress.PostalCode, reservation.Customer.CustomerAddress.PostalCode);
            Assert.AreEqual(reservationDto.Customer.CustomerAddress.Number, reservation.Customer.CustomerAddress.Number);
            Assert.AreEqual(reservationDto.Customer.CustomerAddress.City, reservation.Customer.CustomerAddress.City);

            // Assert on formula
            //Assert.AreEqual(reservationDto.Formula, reservation.Formula);

            CollectionAssert.AreEqual(reservationDto.Items.ToList(), reservation.Items.ToList());
        }



    }
}

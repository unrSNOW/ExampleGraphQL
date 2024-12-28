using AirlineTicketSales.Models;
using Faker;
using System;
using System.Linq;

namespace AirlineTicketSales.Data
{
    public static class DataSeeder
    {
        public static void SeedData(AirlineDbContext db)
        {
            // Если в базе данных нет авиарейсов, добавляем их
            if (!db.Flights.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    var flight = new Flight
                    {
                        FlightNumber = $"FL-{i:000}",
                        DepartureTime = DateTime.Now.AddDays(Faker.RandomNumber.Next(1, 30)),
                        DepartureLocation = "Moscow",
                        ArrivalLocation = "New York",
                        TotalSeats = 100,
                        SoldSeats = Faker.RandomNumber.Next(0, 100)
                    };

                    db.Flights.Add(flight);

                    // Добавление билетов для рейса
                    for (int j = 0; j < flight.TotalSeats; j++)
                    {
                        var ticket = new Ticket
                        {
                            Price = Faker.RandomNumber.Next(5000, 10000),
                            DateOfPurchase = DateTime.Now.AddDays(-Faker.RandomNumber.Next(1, 30)),
                            IsSold = Faker.RandomNumber.Next(0, 2) == 1,
                            Flight = flight
                        };
                        db.Tickets.Add(ticket);
                    }
                }
            }

            // Если в базе данных нет пассажиров, добавляем их
            if (!db.Passengers.Any())
            {
                for (int i = 1; i <= 20; i++)
                {
                    var passenger = new Passenger
                    {
                        FullName = Name.FullName(),
                        Email = Internet.Email(),
                        PhoneNumber = Phone.Number(),
                        PassportNumber = Faker.RandomNumber.Next(1000000, 9999999).ToString()
                    };
                    db.Passengers.Add(passenger);

                    // Добавление билетов для пассажиров
                    for (int j = 0; j < 5; j++)
                    {
                        var ticket = new Ticket
                        {
                            Price = Faker.RandomNumber.Next(5000, 10000),
                            DateOfPurchase = DateTime.Now.AddDays(-Faker.RandomNumber.Next(1, 30)),
                            IsSold = Faker.RandomNumber.Next(0, 2) == 1,
                            Passenger = passenger,
                            Flight = db.Flights.Skip(Faker.RandomNumber.Next(0, db.Flights.Count())).First()
                        };
                        db.Tickets.Add(ticket);
                    }
                }
            }

            // Если в базе данных нет продавцов, добавляем их
            if (!db.Sellers.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    var seller = new Seller
                    {
                        Name = Name.FullName(),
                        ContactInformation = Internet.Email()
                    };
                    db.Sellers.Add(seller);
                }
            }

            // Сохраняем все изменения в базе данных
            db.SaveChanges();
        }
    }
}
using AirlineTicketSales.Models;
using HotChocolate;
using System;
using System.Linq;

namespace AirlineTicketSales.Data
{
    public class Mutation
    {
        // Мутация для создания нового билета
        public Ticket CreateTicket([Service] AirlineDbContext db, int passengerId, int flightId, decimal price)
        {
            var passenger = db.Passengers.Find(passengerId);
            var flight = db.Flights.Find(flightId);

            if (passenger == null || flight == null)
                throw new ArgumentException("Passenger or Flight not found");

            var ticket = new Ticket
            {
                Price = price,
                DateOfPurchase = DateTime.Now,
                IsSold = false, // Новый билет по умолчанию не продан
                Passenger = passenger,
                Flight = flight
            };

            db.Tickets.Add(ticket);
            db.SaveChanges();
            return ticket;
        }

        // Мутация для продажи билета
        public Ticket SellTicket([Service] AirlineDbContext db, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            if (ticket == null) throw new ArgumentException("Ticket not found");

            ticket.IsSold = true;
            db.SaveChanges();
            return ticket;
        }

        // Мутация для создания нового пассажира
        public Passenger CreatePassenger([Service] AirlineDbContext db, string fullName, string email, string phoneNumber, string passportNumber)
        {
            var passenger = new Passenger
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                PassportNumber = passportNumber
            };

            db.Passengers.Add(passenger);
            db.SaveChanges();
            return passenger;
        }
    }
}
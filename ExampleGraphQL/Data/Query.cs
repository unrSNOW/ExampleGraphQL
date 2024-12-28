using AirlineTicketSales.Models;
using HotChocolate;
using System.Linq;

namespace AirlineTicketSales.Data
{
    public class Query
    {
        // Получение всех авиарейсов
        public IQueryable<Flight> GetFlights([Service] AirlineDbContext db)
        {
            return db.Flights;
        }

        // Получение всех пассажиров для определенного рейса
        public IQueryable<Passenger> GetPassengers([Service] AirlineDbContext db, int flightId)
        {
            return db.Tickets.Where(t => t.FlightId == flightId).Select(t => t.Passenger);
        }

        // Получение свободных мест для рейса
        public int GetAvailableSeats([Service] AirlineDbContext db, int flightId)
        {
            var flight = db.Flights.FirstOrDefault(f => f.Id == flightId);
            if (flight == null) return 0;
            return flight.TotalSeats - flight.SoldSeats;
        }

        // Получение информации о пассажире по ID
        public IQueryable<Ticket> GetTicketsByPassenger([Service] AirlineDbContext db, int passengerId)
        {
            return db.Tickets.Where(t => t.PassengerId == passengerId);
        }
    }
}
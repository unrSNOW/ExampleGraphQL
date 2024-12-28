using AirlineTicketSales.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketSales.DAO
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirlineDbContext _context;

        public FlightRepository(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> AddFlightAsync(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> UpdateFlightAsync(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await GetFlightByIdAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
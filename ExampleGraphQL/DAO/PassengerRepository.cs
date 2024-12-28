using AirlineTicketSales.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketSales.DAO
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly AirlineDbContext _context;

        public PassengerRepository(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<Passenger> GetPassengerByIdAsync(int id)
        {
            return await _context.Passengers.FindAsync(id);
        }

        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync()
        {
            return await _context.Passengers.ToListAsync();
        }

        public async Task<Passenger> AddPassengerAsync(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<Passenger> UpdatePassengerAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<bool> DeletePassengerAsync(int id)
        {
            var passenger = await GetPassengerByIdAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
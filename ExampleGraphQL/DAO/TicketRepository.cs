using AirlineTicketSales.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketSales.DAO
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AirlineDbContext _context;

        public TicketRepository(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.Include(t => t.Passenger).Include(t => t.Flight).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByPassengerIdAsync(int passengerId)
        {
            return await _context.Tickets.Include(t => t.Flight)
                                          .Where(t => t.PassengerId == passengerId)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByFlightIdAsync(int flightId)
        {
            return await _context.Tickets.Include(t => t.Passenger)
                                          .Where(t => t.FlightId == flightId)
                                          .ToListAsync();
        }

        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await GetTicketByIdAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}




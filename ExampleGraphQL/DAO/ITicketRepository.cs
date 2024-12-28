using AirlineTicketSales.Models;

namespace AirlineTicketSales.DAO
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsByPassengerIdAsync(int passengerId);
        Task<IEnumerable<Ticket>> GetTicketsByFlightIdAsync(int flightId);
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
    }
}
}



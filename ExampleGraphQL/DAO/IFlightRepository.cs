using AirlineTicketSales.Models;

namespace AirlineTicketSales.DAO
{
    public interface IFlightRepository
    {
        Task<Flight> GetFlightByIdAsync(int id);
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> AddFlightAsync(Flight flight);
        Task<Flight> UpdateFlightAsync(Flight flight);
        Task<bool> DeleteFlightAsync(int id);
    }
}
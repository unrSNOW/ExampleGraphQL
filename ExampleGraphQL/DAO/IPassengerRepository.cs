using AirlineTicketSales.Models;

namespace AirlineTicketSales.DAO
{
    public interface IPassengerRepository
    {
        Task<Passenger> GetPassengerByIdAsync(int id);
        Task<IEnumerable<Passenger>> GetAllPassengersAsync();
        Task<Passenger> AddPassengerAsync(Passenger passenger);
        Task<Passenger> UpdatePassengerAsync(Passenger passenger);
        Task<bool> DeletePassengerAsync(int id);
    }
}
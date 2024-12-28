using AirlineTicketSales.Models;

namespace AirlineTicketSales.DAO
{
    public interface ISellerRepository
    {
        Task<Seller> GetSellerByIdAsync(int id);
        Task<IEnumerable<Seller>> GetAllSellersAsync();
        Task<Seller> AddSellerAsync(Seller seller);
        Task<Seller> UpdateSellerAsync(Seller seller);
        Task<bool> DeleteSellerAsync(int id);
    }
}
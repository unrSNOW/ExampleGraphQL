using AirlineTicketSales.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketSales.DAO
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AirlineDbContext _context;

        public SellerRepository(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            return await _context.Sellers.FindAsync(id);
        }

        public async Task<IEnumerable<Seller>> GetAllSellersAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task<Seller> AddSellerAsync(Seller seller)
        {
            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();
            return seller;
        }

        public async Task<Seller> UpdateSellerAsync(Seller seller)
        {
            _context.Sellers.Update(seller);
            await _context.SaveChangesAsync();
            return seller;
        }

        public async Task<bool> DeleteSellerAsync(int id)
        {
            var seller = await GetSellerByIdAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
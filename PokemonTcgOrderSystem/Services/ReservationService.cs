using PokemonTcgOrderSystem.Data;
using PokemonTcgOrderSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace PokemonTcgOrderSystem.Services
{
    public class ReservationService
    {
        private readonly AppDbContext _dbContext;

        public ReservationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: List all products
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        // POST: Create a reservation
        public async Task<Reservation> CreateReservationAsync(int productId, int userId)
        {
            // Start database transaction
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var product = await _dbContext.Products.FindAsync(productId);

                if (product == null)
                    throw new Exception("Product not found");

                if (product.TotalStock <= 0) // or AvailableStock if you separate it
                    throw new Exception("Out of stock");

                // Decrement stock
                product.TotalStock--;

                // Create reservation
                var reservation = new Reservation
                {
                    ProductId = productId,
                    UserId = userId,
                    Status = "PENDING",
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.Reservations.Add(reservation);

                // Save changes to DB
                await _dbContext.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                // Return the created reservation
                return reservation;
            }
            catch
            {
                // Rollback if anything fails
                await transaction.RollbackAsync();
                throw; // Let controller handle the error
            }
        }
    }
}
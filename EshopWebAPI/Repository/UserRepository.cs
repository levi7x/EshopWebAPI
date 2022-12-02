using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;

namespace EshopWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(string userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(string userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}

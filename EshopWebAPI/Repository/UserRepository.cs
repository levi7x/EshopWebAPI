using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        public User GetUserAsNoTracking(string userId)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool UserExists(string userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public User UserByToken(string token)
        {
            return _context.Users.FirstOrDefault(u => u.RefreshToken == token);
        }
    }
}

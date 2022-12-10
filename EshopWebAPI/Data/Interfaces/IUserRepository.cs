using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        public User GetUser(string userId);
        public ICollection<User> GetUsers();
        public bool UserExists(string userId);

    }
}

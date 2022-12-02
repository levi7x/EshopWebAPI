using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IAddressRepository
    {
        ICollection<Address> GetAddresses();
        Address GetAddress(int id);
        Address GetAddressByUser(string userId);
        bool AddressExists(int id);
    }
}

using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IAddressRepository
    {
        public ICollection<Address> GetAddresses();
        public Address GetAddress(int id);
        public Address GetAddressByUser(string userId);
        public bool UpdateAddress(Address address);
        public bool DeleteAddress(int id);
        public bool AddressExists(int id);
    }
}

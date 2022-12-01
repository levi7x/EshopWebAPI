using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;

namespace EshopWebAPI.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public bool AddressExists(int id)
        {
            throw new NotImplementedException();
        }

        public Address GetAddressByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Address> GetAddresses()
        {
            throw new NotImplementedException();
        }

        public Address GetCountry(int id)
        {
            throw new NotImplementedException();
        }
    }
}

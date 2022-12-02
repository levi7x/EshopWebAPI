using AutoMapper;
using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;

namespace EshopWebAPI.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool AddressExists(int id)
        {
            return _context.Adresses.Any(a => a.Id == id);
        }

        public Address GetAddressByUser(string userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(a => a.Address).FirstOrDefault();
        }

        public ICollection<Address> GetAddresses()
        {
            return _context.Adresses.ToList();
        }

        public Address GetAddress(int id)
        {
            return _context.Adresses.Where(a => a.Id == id).FirstOrDefault();
        }

    }
}

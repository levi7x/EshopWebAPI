using AutoMapper;
using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Users.AsNoTracking().Where(u => u.Id == userId).Select(a => a.Address).FirstOrDefault();
        }

        public ICollection<Address> GetAddresses()
        {
            return _context.Adresses.ToList();
        }

        public Address GetAddress(int id)
        {
            return _context.Adresses.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool UpdateAddress(Address address)
        {
            _context.Adresses.Update(address);
            return Save();
        }

        public bool DeleteAddress(int id)
        {
            var address = GetAddress(id);
            _context.Adresses.Remove(address);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

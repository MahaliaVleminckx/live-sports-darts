using Microsoft.EntityFrameworkCore;
using Pin.LiveSports.Blazor.Data;
using Pin.LiveSports.Core.Interfaces;
using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services
{
    internal class CountryService : ICountryService
    {
        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
        {
            _context = context;
        }
        public List<Country> GetAll()
            => _context.Countries
            .Include(c => c.Players)
            .ToList();

        public Country? GetById(int id)
            => _context.Countries.FirstOrDefault(c => c.Id == id);

        public void Add(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public void Update(Country country)
        {
            var existing = _context.Countries.FirstOrDefault(c => c.Id == country.Id);

            if (existing == null)
                return;

            existing.Name = country.Name;

            _context.SaveChanges(); 
        }

        public void Delete(int id)
        {
            var country = _context.Countries.Find(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                _context.SaveChanges();
            }
        }
    }
}

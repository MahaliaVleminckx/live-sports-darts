using Pin.LiveSports.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin.LiveSports.Core.Interfaces
{
    public interface ICountryService
    {
        List<Country> GetAll();
        Country? GetById(int id);
        void Add(Country country);
        void Update(Country country);
        void Delete(int id);
    }
}

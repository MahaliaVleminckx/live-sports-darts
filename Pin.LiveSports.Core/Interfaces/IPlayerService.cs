using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Interfaces
{
    public interface IPlayerService
    {
        List<Player> GetAll();
        Player? GetById(int id);
        void Add(Player player);
        void Update(Player player);
        void Delete(int id);
    }
}

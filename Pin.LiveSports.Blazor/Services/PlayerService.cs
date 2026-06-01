using Microsoft.EntityFrameworkCore;
using Pin.LiveSports.Blazor.Data;
using Pin.LiveSports.Core.Interfaces;
using Pin.LiveSports.Core.Models;
using System.Diagnostics.Metrics;

namespace Pin.LiveSports.Blazor.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _context;

        public PlayerService(AppDbContext context)
        {
            _context = context;
        }

        public List<Player> GetAll()
            => _context.Players.Include(p => p.Country).ToList();

        public Player? GetById(int id)
            => _context.Players.FirstOrDefault(p => p.Id == id);

        public void Add(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void Update(Player player)
        {
            Console.WriteLine($"Updating player ID: {player.Id}");
            var existing = _context.Players.FirstOrDefault(c => c.Id == player.Id);

            if (existing == null)
            {
                Console.WriteLine("Player not found!");
                return;
            }
                

            existing.Name = player.Name;
            existing.Nickname = player.Nickname;
            existing.CountryId = player.CountryId;

            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var player = _context.Players.Find(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin.LiveSports.Core.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Nickname { get; set; } = "";

        public int CountryId {  get; set; }
        public Country Country { get; set; } = null!;
    }
}

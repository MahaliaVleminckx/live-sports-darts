using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin.LiveSports.Core.Models
{
    public class DartMatch
    {
        public Player Player1 { get; set; } = default!;
        public Player Player2 { get; set; } = default!;

        public int Player1Score { get; set; } = 501;
        public int Player2Score { get; set; } = 501;

        public bool IsStarted { get; set; }
        public string Tournament { get; set; } = "";
        public string Location { get; set; } = "";
        public string MatchName { get; set; } = "";
    }
}

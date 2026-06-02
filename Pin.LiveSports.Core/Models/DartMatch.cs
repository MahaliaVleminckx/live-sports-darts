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
        public DateTime StartTime { get; set; }

        
        public int CurrentSet { get; set; } = 1;
        public int CurrentLeg { get; set; } = 1;

        public int LegsToWinSet { get; set; } = 3;
        public int SetsToWinMatch { get; set; } = 3;

        public int Player1Legs { get; set; }
        public int Player2Legs { get; set; }
    }
}

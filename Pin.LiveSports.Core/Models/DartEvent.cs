using Pin.LiveSports.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pin.LiveSports.Core.Enums;

namespace Pin.LiveSports.Core.Models
{
    public class DartEvent
    {
        public DateTime Time { get; set; } = DateTime.Now;

        public Player Player { get; set; } = default!;

        public EventType Type { get; set; }

        public int ScoreChange { get; set; }

        public string Message { get; set; } = "";
    }
}

using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Blazor.Services
{
    public class DartGameService
    {
        public DartMatch CurrentMatch { get; private set; } = new();

        public List<DartEvent> Events { get; } = new();

        public event Func<Task>? OnChange;

        public bool CanRenderMatch =>
      CurrentMatch?.IsStarted == true &&
      CurrentMatch.Player1 != null &&
      CurrentMatch.Player2 != null;
        public async Task StartMatch(Player p1, Player p2)
        {
            CurrentMatch = new DartMatch
            {
                Player1 = p1,
                Player2 = p2,
                Player1Score = 501,
                Player2Score = 501,
                IsStarted = true
            };

            Events.Clear();

            await Notify();
        }

        // ✅ DIT IS JE NIEUWE CORE METHOD
        public async Task AddEvent(DartEvent ev)
        {
            ev.Time = DateTime.Now;

            Events.Add(ev);

            // 🎯 SCORE LOGIC (alleen als score bestaat)
            if (ev.ScoreChange > 0)
            {
                if (ev.Player.Id == CurrentMatch.Player1.Id)
                    CurrentMatch.Player1Score -= ev.ScoreChange;
                else
                    CurrentMatch.Player2Score -= ev.ScoreChange;
            }

            await Notify();
        }

        private async Task Notify()
        {
            if (OnChange != null)
                await OnChange.Invoke();
        }

    }

}

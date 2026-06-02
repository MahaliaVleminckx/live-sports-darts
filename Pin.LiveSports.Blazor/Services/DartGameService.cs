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

        public async Task StartMatch(
            Player p1,
            Player p2,
            string matchName,
            string tournament,
            string location,
            DateTime startTime,
            int legsToWinSet,
            int setsToWinMatch)
        {
            CurrentMatch = new DartMatch
            {
                Player1 = p1,
                Player2 = p2,
                MatchName = matchName,
                Tournament = tournament,
                Location = location,
                StartTime = startTime,

                LegsToWinSet = legsToWinSet,
                SetsToWinMatch = setsToWinMatch,

                Player1Score = 501,
                Player2Score = 501,

                CurrentLeg = 1,
                CurrentSet = 1,

                IsStarted = true
            };

            Events.Clear();
            await Notify();
        }

        public async Task AddEvent(DartEvent ev)
        {
            ev.Time = DateTime.Now;
            Events.Add(ev);

            if (ev.ScoreChange > 0)
            {
                if (ev.Player.Id == CurrentMatch.Player1.Id)
                    CurrentMatch.Player1Score -= ev.ScoreChange;
                else
                    CurrentMatch.Player2Score -= ev.ScoreChange;
            }

            HandleLegAndSetProgression();

            await Notify();
        }

        private void HandleLegAndSetProgression()
        {
            
            if (CurrentMatch.Player1Score <= 0)
            {
                CurrentMatch.Player1Legs++;
                ResetLeg();
            }
            else if (CurrentMatch.Player2Score <= 0)
            {
                CurrentMatch.Player2Legs++;
                ResetLeg();
            }
        }

        private void ResetLeg()
        {
            CurrentMatch.Player1Score = 501;
            CurrentMatch.Player2Score = 501;

            CurrentMatch.CurrentLeg++;

            HandleSetCheck();
        }

        private void HandleSetCheck()
        {
            if (CurrentMatch.Player1Legs >= CurrentMatch.LegsToWinSet ||
                CurrentMatch.Player2Legs >= CurrentMatch.LegsToWinSet)
            {
                CurrentMatch.CurrentSet++;
                CurrentMatch.CurrentLeg = 1;

                CurrentMatch.Player1Legs = 0;
                CurrentMatch.Player2Legs = 0;
            }
        }

        private async Task Notify()
        {
            if (OnChange != null)
                await OnChange.Invoke();
        }
    }

}

using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Blazor.Services
{
    public class DartGameService
    {
        public DartMatch CurrentMatch { get; private set; } = new();

        public List<DartEvent> Events { get; } = new();

        public event Func<Task>? OnChange;

        private bool _legFinished = false;

        public bool MatchFinished { get; private set; }
        public Player? Winner { get; private set; }

    
        private readonly Dictionary<int, List<int>> _playerScores = new();

        public bool CanRenderMatch =>
            CurrentMatch?.IsStarted == true &&
            CurrentMatch?.Player1 != null &&
            CurrentMatch?.Player2 != null;

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

                Player1Legs = 0,
                Player2Legs = 0,
                Player1Sets = 0,
                Player2Sets = 0,

                IsStarted = true
            };

            Events.Clear();
            _legFinished = false;

            MatchFinished = false;
            Winner = null;

            _playerScores.Clear();
            _playerScores[p1.Id] = new List<int>();
            _playerScores[p2.Id] = new List<int>();

            await Notify();
        }

        public async Task AddEvent(DartEvent ev)
        {
            ev.Time = DateTime.Now;

            if (MatchFinished)
                return;

            if (_legFinished)
            {
                Events.Add(ev);
                await Notify();
                return;
            }

            Events.Add(ev);

            if (ev.ScoreChange > 0)
            {
                if (ev.Player.Id == CurrentMatch.Player1.Id)
                    CurrentMatch.Player1Score -= ev.ScoreChange;
                else
                    CurrentMatch.Player2Score -= ev.ScoreChange;
            }

            if (!_playerScores.ContainsKey(ev.Player.Id))
                _playerScores[ev.Player.Id] = new List<int>();

            int score = ev.Type == Core.Enums.EventType.NoScore
                ? 0
                : ev.ScoreChange;

            _playerScores[ev.Player.Id].Add(score);

            CheckLegWin();
            await Notify();
        }

        public double GetAverage(Player player)
        {
            if (!_playerScores.ContainsKey(player.Id))
                return 0;

            var list = _playerScores[player.Id];

            if (list.Count == 0)
                return 0;

            return list.Average();
        }

        private void CheckLegWin()
        {
            var match = CurrentMatch;

            if (match.Player1Score <= 0)
                HandleLegWin(match.Player1);

            else if (match.Player2Score <= 0)
                HandleLegWin(match.Player2);
        }

        private void HandleLegWin(Player winner)
        {
            var match = CurrentMatch;

            _legFinished = true;

            Events.Add(new DartEvent
            {
                Player = winner,
                Time = DateTime.Now,
                Type = Core.Enums.EventType.Highlight,
                Message = $"🎯 LEG {match.CurrentLeg} won by {winner.Nickname}"
            });

            if (winner.Id == match.Player1.Id)
                match.Player1Legs++;
            else
                match.Player2Legs++;

            if (IsSetWonBy(winner))
            {
                HandleSetWin(winner);
                return;
            }

            match.CurrentLeg++;

            ResetLeg();
        }

        private bool IsSetWonBy(Player player)
        {
            var match = CurrentMatch;

            return player.Id == match.Player1.Id
                ? match.Player1Legs >= match.LegsToWinSet
                : match.Player2Legs >= match.LegsToWinSet;
        }

        private bool IsMatchWonBy(Player player)
        {
            var match = CurrentMatch;

            return player.Id == match.Player1.Id
                ? match.Player1Sets >= match.SetsToWinMatch
                : match.Player2Sets >= match.SetsToWinMatch;
        }

        private void HandleSetWin(Player winner)
        {
            var match = CurrentMatch;

            Events.Add(new DartEvent
            {
                Player = winner,
                Time = DateTime.Now,
                Type = Core.Enums.EventType.Highlight,
                Message = $"🏆 SET {match.CurrentSet} won by {winner.Nickname}"
            });

            if (winner.Id == match.Player1.Id)
                match.Player1Sets++;
            else
                match.Player2Sets++;

            if (IsMatchWonBy(winner))
            {
                MatchFinished = true;
                Winner = winner;

                Events.Add(new DartEvent
                {
                    Player = winner,
                    Time = DateTime.Now,
                    Type = Core.Enums.EventType.Highlight,
                    Message = $"👑 MATCH WON BY {winner.Nickname}"
                });

                return;
            }

            match.CurrentSet++;

            match.Player1Legs = 0;
            match.Player2Legs = 0;

            match.CurrentLeg = 1;

            ResetLeg();
        }

        private void ResetLeg()
        {
            CurrentMatch.Player1Score = 501;
            CurrentMatch.Player2Score = 501;

            _legFinished = false;
        }

        private async Task Notify()
        {
            if (OnChange != null)
                await OnChange.Invoke();
        }
    }
}
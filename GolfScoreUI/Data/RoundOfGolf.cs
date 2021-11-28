using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GolfScoreUI.Data
{
    public class RoundOfGolf
    {
        public RoundOfGolf(string player1, string player2, string player3)
        {
            Player1 = player1;
            Player2 = player2;
            Player3 = player3;
        }

        public string Player1 { get; }
        public string Player2 { get; }
        public string Player3 { get; }

        public int[] Player1Score { get; } = new int[18];
        public int[] Player2Score { get; } = new int[18];
        public int[] Player3Score { get; } = new int[18];
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itransition.Task3
{
    using System;
    using System.Collections.Generic;

    public class GameRules
    {
        private readonly List<string> moves;

        public GameRules(List<string> moves)
        {
            this.moves = moves;
        }

        // Determine the winner
        public string DetermineWinner(string playerMove, string computerMove)
        {
            int playerIndex = moves.IndexOf(playerMove);
            int computerIndex = moves.IndexOf(computerMove);

            int halfSize = moves.Count / 2;

            if (playerIndex == computerIndex)
            {
                return "Draw";
            }
            else if ((computerIndex > playerIndex && computerIndex - playerIndex <= halfSize) ||
                     (playerIndex > computerIndex && playerIndex - computerIndex > halfSize))
            {
                return "Computer Wins!";
            }
            else
            {
                return "Player Wins!";
            }
        }
    }

}

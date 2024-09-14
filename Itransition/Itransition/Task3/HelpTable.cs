using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itransition.Task3
{
    using System;
    using System.Collections.Generic;

    public class HelpTable
    {
        private readonly List<string> moves;

        public HelpTable(List<string> moves)
        {
            this.moves = moves;
        }

        public void DisplayHelp()
        {
            Console.WriteLine("Help Table:");
            Console.Write("     ");
            foreach (var move in moves)
            {
                Console.Write(move.PadRight(10));
            }
            Console.WriteLine();

            for (int i = 0; i < moves.Count; i++)
            {
                Console.Write(moves[i].PadRight(5));
                for (int j = 0; j < moves.Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write("Draw".PadRight(10));
                    }
                    else
                    {
                        var gameRules = new GameRules(moves);
                        Console.Write(gameRules.DetermineWinner(moves[i], moves[j]).PadRight(10));
                    }
                }
                Console.WriteLine();
            }
        }
    }

}

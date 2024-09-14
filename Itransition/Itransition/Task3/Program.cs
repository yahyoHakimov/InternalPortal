using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itransition.Task3
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3 || args.Length % 2 == 0 || HasDuplicates(args))
            {
                Console.WriteLine("Error: You must pass an odd number of non-repeating moves (at least 3).");
                Console.WriteLine("Example: rock paper scissors");
                return;
            }

            List<string> moves = new List<string>(args);
            Game game = new Game(moves);
            game.Start();
        }

        private static bool HasDuplicates(string[] args)
        {
            HashSet<string> uniqueMoves = new HashSet<string>();
            foreach (var move in args)
            {
                if (!uniqueMoves.Add(move))
                {
                    return true;
                }
            }
            return false;
        }
    }

}

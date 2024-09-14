using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itransition.Task3
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private readonly List<string> moves;
        private readonly KeyGenerator keyGenerator;
        private readonly GameRules gameRules;

        public Game(List<string> moves)
        {
            this.moves = moves;
            keyGenerator = new KeyGenerator();
            gameRules = new GameRules(moves);
        }

        public void Start()
        {
            string computerMove = moves[new Random().Next(moves.Count)];
            string hmac = keyGenerator.CalculateHMAC(computerMove);
            Console.WriteLine($"HMAC: {hmac}");

            while (true)
            {
                DisplayMenu();
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (input == "?")
                {
                    HelpTable helpTable = new HelpTable(moves);
                    helpTable.DisplayHelp();
                }
                else if (int.TryParse(input, out int choice) && choice > 0 && choice <= moves.Count)
                {
                    string playerMove = moves[choice - 1];
                    Console.WriteLine($"Your move: {playerMove}");
                    Console.WriteLine($"Computer move: {computerMove}");

                    string result = gameRules.DetermineWinner(playerMove, computerMove);
                    Console.WriteLine(result);
                    Console.WriteLine($"HMAC key: {keyGenerator.GetKeyString()}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
            Console.Write("Enter your move: ");
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter first player's name: ");
            string player1Name = Console.ReadLine();

            Console.Write("Enter second player's name: ");
            string player2Name = Console.ReadLine();

            new Match(player1Name, player2Name);

            Console.ReadLine();
        }
    }
}

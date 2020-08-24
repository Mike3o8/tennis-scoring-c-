using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoring
{
    class Match
    {
        private Player player1;
        private Player player2;
        private bool deuce;
        private bool tieBreaker;
        private Player winner = null;
        private Dictionary<int, int> gamePoints = new Dictionary<int, int>()
        {
            {0, 0},
            {1, 15},
            {2, 30},
            {3, 40}
        };

        public Match(string player1Name, string player2Name)
        {
            player1 = new Player(player1Name);
            player2 = new Player(player2Name);

            while(winner == null)
            {
                Console.Write("Winner of point: ");
                string playerName = Console.ReadLine();
                PointWonBy(playerName);
                Console.WriteLine("Score: " + Score());
            }
        }

        private void PointWonBy(string player)
        {
            if (player != this.player1.name && player != this.player2.name)
            {
                Console.WriteLine("Player name does not match either of the current players");
                return;
            }
            if (this.winner == null)
            {
                if (player == this.player1.name)
                {
                    AddPoint(this.player1, this.player2);
                } else
                {
                    AddPoint(this.player2, this.player1);
                }
                return;
            }
            Player loser = this.winner.name == this.player1.name ? this.player2 : this.player1;
            Console.WriteLine($"{this.winner.name} has won the set with a final score of {this.winner.games} - {loser.games}. Please start a new game.");
        }

        private string Score()
        {
            if (this.tieBreaker && (this.player1.tieBreakerPoints > 0 || this.player2.tieBreakerPoints > 0))
            {
                return $"{this.player1.games} - {this.player2.games}, {this.player1.tieBreakerPoints} - {this.player2.tieBreakerPoints}";
        }
            if (this.deuce)
            {
                if (!this.player1.advantage && !this.player2.advantage)
                {
                    return $"{this.player1.games} - { this.player2.games}, Deuce";
            }
                if (this.player1.advantage)
                {
                    return $"{ this.player1.games} - { this.player2.games}, Advantage { this.player1.name}";
            }
                return $"{ this.player1.games} - { this.player2.games}, Advantage { this.player2.name}";
        }

            if (this.player1.points > 0 || this.player2.points > 0)
            {
                return $"{ this.player1.games} - { this.player2.games}, { this.gamePoints[this.player1.points]} - { this.gamePoints[this.player2.points]}";
        }
            return $"{ this.player1.games} - { this.player2.games}";
        }

        private void AddPoint(Player player, Player opponent)
        {
            if (this.tieBreaker)
            {
                IncrementTieBreaker(player, opponent);
                return;
            }
            if (!this.deuce)
            {
                if (player.points == 3)
                {
                    IncrementGame(player, opponent);
                    return;
                }
                if (player.points == 2 && opponent.points == 3)
                {
                    this.deuce = true;
                }
                player.points++;
                return;
            }

            if (player.advantage)
            {
                IncrementGame(player, opponent);
                return;
            }
            if (opponent.advantage)
            {
                opponent.advantage = false;
                return;
            }
            player.advantage = true;
        }

        private void IncrementGame(Player player, Player opponent)
        {
            player.games++;
            Player[] players = { player, opponent };
            ResetPoints(players);

            if (player.games >= 6)
            {
                if ((player.games - opponent.games) >= 2)
                {
                    SetWinner(player);
                    return;
                }
                if (player.games == 6 && opponent.games == 6)
                {
                    this.tieBreaker = true;
                }
            }
        }

        private void ResetPoints(Player[] players)
        {
            foreach (Player player in players)
            {
                player.points = 0;
                player.advantage = false;
            }
            this.deuce = false;
        }

        private void IncrementTieBreaker(Player player, Player opponent)
        {
            player.tieBreakerPoints++;
            if (player.tieBreakerPoints >= 7 && player.tieBreakerPoints - opponent.tieBreakerPoints >= 2)
            {
                player.games++;
                SetWinner(player);
            }
        }

        private void SetWinner(Player player)
        {
            Console.WriteLine($"{ player.name} has won the set!");
            this.winner = player;
        }
    }
}

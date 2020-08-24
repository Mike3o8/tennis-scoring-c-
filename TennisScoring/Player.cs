using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoring
{
    class Player
    {
        public string name;
        public int games;
        public int points;
        public bool advantage;
        public int tieBreakerPoints;

        public Player(string name)
        {
            this.name = name;
            this.games = 0;
            this.points = 0;
            this.advantage = false;
            this.tieBreakerPoints = 0;
        }
    }
}

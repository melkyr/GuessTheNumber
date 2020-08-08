using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumberLibrary
{
    public class Player
    {
        public string Name { get; set; }
        public Dictionary<string, int> Score { get; set; }

        public Player()
        {
            Score = new Dictionary<string, int>();
            Score.Add("Wins", 0);
            Score.Add("Loses", 0);
        }
    }
}

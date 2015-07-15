using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissors.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string Type { get; set; }
        public string EnemyType { get; set; }
        public int Number { get; set; }
        public string Result { get; set; }
        public int Wins { get; set; }
        public int AllWins { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    public enum Player { Black, White };

    class Move
    {
        private int x, y;


        public Player player { get; set; }
        public int X => x;
        public int Y => y;
    }
}

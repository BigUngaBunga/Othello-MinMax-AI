using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI.GameBoard
{
    class Move
    {
        int row, column;


        public string Player { get; set; }
        public int Row => row;
        public int Column => column;
    }
}

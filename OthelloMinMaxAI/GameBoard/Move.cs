using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    public enum Player { Black, White };

    class Move
    {
        private int x, y;

        public Move(int x, int y, Player player)
        {
            this.x = x;
            this.y = y;
            this.player = player;
        }

        public Move (Point point, Player player)
        {
            x = point.X;
            y = point.Y;
            this.player = player;
        }

        public Player player { get; set; }
        public int X => x;
        public int Y => y;
    }
}

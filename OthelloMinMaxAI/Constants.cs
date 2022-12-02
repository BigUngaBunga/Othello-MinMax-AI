using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    public static class Constants
    {
        public static int BoardSize, TileWidth, ColorWidth, MaxTileSize, MinTileSize;
        public static Point MenuSize;
        public const int MAX_TREE_DEPTH = 8;


        public static void LoadConstants()
        {
            BoardSize = 10;
            TileWidth = 60;
            ColorWidth = 24;
            MenuSize = new Point(640);
            MaxTileSize = 150;
            MinTileSize = 30;
        }
    }
}
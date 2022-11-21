using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace OthelloMinMaxAI
{
    public static class SpriteClass
    {
        public static Texture2D Tile, Disk, Cross, ColorTile, Selected, Star, Circle, Diamond, Border, StartButton, Letters;

        public static List<Texture2D> Confetti;

        public static SpriteFont font;


        public static void LoadContent(ContentManager content)
        {
            Tile = content.Load<Texture2D>("Textures/Tile");
            Disk = content.Load<Texture2D>("Textures/disk");
            Cross = content.Load<Texture2D>("Textures/cross");
            ColorTile = content.Load<Texture2D>("Textures/colortile");
            Selected = content.Load<Texture2D>("Textures/selected");
            Star = content.Load<Texture2D>("Textures/star");
            Circle = content.Load<Texture2D>("Textures/circle");
            Diamond = content.Load<Texture2D>("Textures/diamond");
            Border = content.Load<Texture2D>("Textures/border");
            StartButton = content.Load<Texture2D>("Textures/startbutton");
            Letters = content.Load<Texture2D>("Textures/letters");

            Confetti = new List<Texture2D>() { Star, Circle, Diamond };

            font = content.Load<SpriteFont>("File");

        }


    }
}

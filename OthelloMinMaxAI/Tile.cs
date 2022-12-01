using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace OthelloMinMaxAI
{

    class Tile
    {
        public enum DisplayState { None, Player, AI};

        public Point Location { get; }
        public Point Index { get; private set; }
        Color color;

        public Color tileColor;

        public Rectangle hitbox;

        public bool redCrossed;

        public DisplayState displayState;

        public int currentDiskIndex;

        public static int maxDiskIndex = SpriteClass.Disk.Width / SpriteClass.Disk.Height - 1;

        /// <summary>
        /// 0 equals empty. 1 equals Dark. 2 equals Light. 3 equals Border.
        /// </summary>
        public int currentTile;
            
        public Tile(int X, int Y)
        {
            Index = new Point(X, Y);
            Location = new Point(X * Constants.TileWidth, Y * Constants.TileWidth);
            color = Color.Transparent;
            tileColor = Color.Gray;
            //currentTile = 0;
            hitbox = new Rectangle(Location, new Point(Constants.TileWidth));
        }

        public void AssignTile(int playerValue)
        {
            if (playerValue == 0)
            {
                color = Color.Transparent;
            }
            else if (playerValue == 1)
            {
                color = Menu.playerOne.color;
            }
            else if (playerValue == 2 ||playerValue ==-1)
            {
                color = Menu.playerTwo.color;
            }

            currentTile = playerValue;
        }

        public void Draw(SpriteBatch sb)
        {
            Rectangle source = new Rectangle(SpriteClass.Disk.Width * currentDiskIndex / (maxDiskIndex + 1), 0, SpriteClass.Disk.Width / (maxDiskIndex + 1), SpriteClass.Disk.Height);

            if (currentTile == -1)
            {
                sb.Draw(SpriteClass.Tile, hitbox, GameManager.board.GetCurrentColor());
                sb.Draw(SpriteClass.Border, hitbox, Color.White);
            }
            else
            {
                switch (displayState)
                {
                    case DisplayState.Player:
                        sb.Draw(SpriteClass.Tile, hitbox, Color.LightGray);
                        break;
                    case DisplayState.AI:
                        sb.Draw(SpriteClass.Tile, hitbox, Color.DarkGoldenrod);
                        break;
                    default:
                        sb.Draw(SpriteClass.Tile, hitbox, tileColor);
                        break;
                }
                sb.Draw(SpriteClass.Disk, hitbox, source, color);
            }
            
            if (redCrossed)
            {
                sb.Draw(SpriteClass.Cross, hitbox, Color.Red);
                redCrossed = false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OthelloMinMaxAI
{
    class ColorOption
    {
        public Rectangle hitbox;
        public Color color;
        

        public ColorOption(Vector2 location, Color color)
        {
            this.color = color;
            hitbox = new Rectangle(location.ToPoint(), new Point(Constants.ColorWidth));
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(SpriteClass.ColorTile, hitbox, color);
        }
    }

    class SelectedOption
    {
        public Rectangle hitbox;
        public Color color;

        public SelectedOption(Vector2 location, Color color)
        {
            this.color = color;
            hitbox = new Rectangle(location.ToPoint(), new Point(Constants.ColorWidth));
        }

        public SelectedOption(SelectedOption other)
        {
            color = other.color;
            hitbox = other.hitbox;
        }

        public SelectedOption(ColorOption other)
        {
            color = other.color;
            hitbox = other.hitbox;
        }

        public void CopyColor(ColorOption option)
        {
            color = option.color;
            hitbox.Location = option.hitbox.Location;
        }

        public void CopyColor(SelectedOption option)
        {
            color = option.color;
            hitbox.Location = new Point(hitbox.Location.X, option.hitbox.Location.Y);
        }

        public static bool operator ==(SelectedOption a, ColorOption b)
        {
            if (a is null || b is null)
                return false;

            return a.color == b.color;
        }

        public static bool operator !=(SelectedOption a, ColorOption b)
        {
            if (a is null || b is null)
                return false;

            return a.color != b.color;
        }

        public void SwitchPlace(SelectedOption other)
        {
            SelectedOption temp;
            temp = new SelectedOption(this);
            CopyColor(other);
            other.CopyColor(temp);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(SpriteClass.Selected, hitbox, Color.White);
        }
    }

    static class Menu
    {
        static List<ColorOption> colorOptionsOne, colorOptionsTwo;
        public static SelectedOption playerOne, playerTwo;

        static Tile showcase;
        private static bool useAi;

        static ButtonManager buttonManager;
        static Button startButton;
        static Button aiButton;



        public static void LoadMenu()
        {
            showcase = new Tile(0, 0);
            PlaceTile();
            colorOptionsOne = new List<ColorOption>();
            colorOptionsTwo = new List<ColorOption>();

            buttonManager = new ButtonManager();
            startButton = new Button(SpriteClass.StartButton, new Point(Constants.MenuSize.X / 2 - 64, Constants.MenuSize.Y * 3 / 4), new Point(128, 64), 0.5f);
            aiButton = new Button(SpriteClass.StartButton, new Point(Constants.MenuSize.X / 2 - 64, Constants.MenuSize.Y * 2 / 4), new Point(128, 64), 0.5f);
            buttonManager.Add(startButton);
            buttonManager.Add(aiButton);

            LoadColors(colorOptionsOne, new Vector2(Constants.ColorWidth));
            LoadColors(colorOptionsTwo, new Vector2(Constants.MenuSize.X - Constants.ColorWidth * 2, Constants.ColorWidth));

            playerOne = new SelectedOption(colorOptionsOne[0]);
            playerTwo = new SelectedOption(colorOptionsTwo[1]);

        }

        static void LoadColors(List<ColorOption> list, Vector2 startLocation)
        {
            list.Add(new ColorOption(startLocation, new Color(40, 40, 40)));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 2), Color.White));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 24), Color.SaddleBrown));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 22), Color.Red));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 20), Color.Orange));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 18), Color.Yellow));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 16), Color.Lime));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 14), Color.Green));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 12), Color.DarkCyan));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 10), Color.Blue));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 8), Color.Purple));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 6), Color.Magenta));
            list.Add(new ColorOption(startLocation + new Vector2(0, Constants.ColorWidth * 4), Color.Pink));
        }

        static void PlaceTile()
        {
            showcase.hitbox = new Rectangle(Constants.MenuSize.X / 2 - Constants.TileWidth / 2, Constants.MenuSize.Y / 2 - Constants.TileWidth / 2, Constants.TileWidth, Constants.TileWidth);
        }

        public static void Update(GameTime gameTime)
        {
            buttonManager.Update(gameTime);

            if (aiButton.buttonPressed)
            {
                useAi = !useAi;
                aiButton.ChangeColour(new Color(64,128,64,255), new Color(255,128,128,255), useAi);
                aiButton.buttonPressed = false;
            }

            if (startButton.buttonPressed)
            {
                GameManager.NewGame(useAi);
                startButton.buttonPressed = false;
            }

            if (KeyMouseReader.KeyPressed(Keys.Up))
            {
                if (Constants.TileWidth < Constants.MaxTileSize)
                {
                    Constants.TileWidth += 30;
                    PlaceTile();
                }

            }
            else if (KeyMouseReader.KeyPressed(Keys.Down))
            {
                if (Constants.TileWidth > Constants.MinTileSize)
                {
                    Constants.TileWidth -= 30;
                    PlaceTile();
                }
            }

            if (KeyMouseReader.LeftClick())
            {
                foreach (ColorOption c in colorOptionsOne)
                {
                    if (c.hitbox.Contains(KeyMouseReader.mouseState.Position))
                    {
                        if (playerTwo == c)
                        {
                            playerOne.SwitchPlace(playerTwo);
                        }
                        else
                        {
                            playerOne.CopyColor(c);
                        }
                        break;
                    }
                }
                foreach (ColorOption c in colorOptionsTwo)
                {
                    if (c.hitbox.Contains(KeyMouseReader.mouseState.Position))
                    {
                        if (playerOne == c)
                        {
                            playerTwo.SwitchPlace(playerOne);
                        }
                        else
                        {
                            playerTwo.CopyColor(c);
                        }
                        break;
                    }
                }
            }
        }


        public static void Draw(SpriteBatch sb)
        {
            buttonManager.Draw(sb);

            foreach (ColorOption c in colorOptionsOne)
                c.Draw(sb);
            foreach (ColorOption c in colorOptionsTwo)
                c.Draw(sb);

            playerOne.Draw(sb);
            playerTwo.Draw(sb);
            //showcase.Draw(sb);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    public static class CustomTextClass
    {
        static List<Point> PointList;

        public static Point LetterBig, LetterSmall;

        static RenderTarget2D renderTarget;

        static GraphicsDevice graphicsDevice;
        static SpriteBatch spriteBatch;

        static Texture2D Letters;

        public static void LoadClass(GraphicsDevice g, SpriteBatch sb)
        {
            PointList = new List<Point>();
            LetterBig = new Point(22, 38);
            LetterSmall = new Point(16, 38);
            graphicsDevice = g;
            spriteBatch = sb;
            Letters = SpriteClass.Letters;
        }

        public static RenderTarget2D TextToImage(string Input, Color color)
        {
            PointList.Clear();
            foreach (Char c in Input)
            {
                PointList.Add(LetterPoint(c));
            }

            DrawRenderTarget(color);

            return renderTarget;
        }


        static void DrawRenderTarget(Color color)
        {
            int renderWidth = 0;
            foreach (Point p in PointList)
            {
                if (p.Y == 0)
                {
                    renderWidth += LetterBig.X;
                }
                else
                {
                    renderWidth += LetterSmall.X;
                }
            }

            renderTarget = new RenderTarget2D(graphicsDevice, renderWidth, LetterBig.Y);
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            int width = 0;

            foreach (Point p in PointList)
            {
                if (p.Y == 0)
                {
                    spriteBatch.Draw(Letters, new Vector2(width, 0), new Rectangle(new Point(1 + p.X * (LetterBig.X + 1), 1 + p.Y * (LetterBig.Y + 1)), LetterBig), color);
                    width += LetterBig.X;
                }
                else
                {
                    spriteBatch.Draw(Letters, new Vector2(width, 0), new Rectangle(new Point(1 + p.X * (LetterSmall.X + 1), 1 + p.Y * (LetterSmall.Y + 1)), LetterSmall), color);
                    width += LetterSmall.X;
                }
            }

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
        }

        public static Point ScaleRenderToPoint(RenderTarget2D render, float scale)
        {
            return new Point((int)(render.Width * scale), (int)(render.Height * scale));
        }

        public static Point LetterPoint(Char c)
        {
            if (c == 'A')
            {
                return new Point(0, 0);
            }
            else if (c == 'B')
            {
                return new Point(1, 0);
            }
            else if (c == 'C')
            {
                return new Point(2, 0);
            }
            else if (c == 'D')
            {
                return new Point(3, 0);
            }
            else if (c == 'E')
            {
                return new Point(4, 0);
            }
            else if (c == 'F')
            {
                return new Point(5, 0);
            }
            else if (c == 'G')
            {
                return new Point(6, 0);
            }
            else if (c == 'H')
            {
                return new Point(7, 0);
            }
            else if (c == 'm')
            {
                return new Point(8, 0);
            }
            else if (c == 'J')
            {
                return new Point(9, 0);
            }
            else if (c == 'K')
            {
                return new Point(10, 0);
            }
            else if (c == 'L')
            {
                return new Point(11, 0);
            }
            else if (c == 'M')
            {
                return new Point(12, 0);
            }
            else if (c == 'N')
            {
                return new Point(13, 0);
            }
            else if (c == 'O')
            {
                return new Point(14, 0);
            }
            else if (c == 'P')
            {
                return new Point(15, 0);
            }
            else if (c == 'Q')
            {
                return new Point(16, 0);
            }
            else if (c == 'R')
            {
                return new Point(17, 0);
            }
            else if (c == 'S')
            {
                return new Point(18, 0);
            }
            else if (c == 'T')
            {
                return new Point(19, 0);
            }
            else if (c == 'U')
            {
                return new Point(20, 0);
            }
            else if (c == 'V')
            {
                return new Point(21, 0);
            }
            else if (c == 'W')
            {
                return new Point(22, 0);
            }
            else if (c == 'X')
            {
                return new Point(23, 0);
            }
            else if (c == 'Y')
            {
                return new Point(24, 0);
            }
            else if (c == 'Z')
            {
                return new Point(25, 0);
            }
            else if (c == 'w')
            {
                return new Point(26, 0);
            }
            else if (c == '?')
            {
                return new Point(27, 0);
            }
            else if (c == '+')
            {
                return new Point(28, 0);
            }
            else if (c == ' ')
            {
                return new Point(27, 1);
            }
            else if (c == 'a')
            {
                return new Point(0, 1);
            }
            else if (c == 'b')
            {
                return new Point(1, 1);
            }
            else if (c == 'c')
            {
                return new Point(2, 1);
            }
            else if (c == 'd')
            {
                return new Point(3, 1);
            }
            else if (c == 'e')
            {
                return new Point(4, 1);
            }
            else if (c == 'f')
            {
                return new Point(5, 1);
            }
            else if (c == 'g')
            {
                return new Point(6, 1);
            }
            else if (c == 'h')
            {
                return new Point(7, 1);
            }
            else if (c == 'i')
            {
                return new Point(8, 1);
            }
            else if (c == 'j')
            {
                return new Point(9, 1);
            }
            else if (c == 'k')
            {
                return new Point(10, 1);
            }
            else if (c == 'l')
            {
                return new Point(11, 1);
            }
            else if (c == 'I')
            {
                return new Point(12, 1);
            }
            else if (c == 'n')
            {
                return new Point(13, 1);
            }
            else if (c == 'o')
            {
                return new Point(14, 1);
            }
            else if (c == 'p')
            {
                return new Point(15, 1);
            }
            else if (c == 'q')
            {
                return new Point(16, 1);
            }
            else if (c == 'r')
            {
                return new Point(17, 1);
            }
            else if (c == 's')
            {
                return new Point(18, 1);
            }
            else if (c == 't')
            {
                return new Point(19, 1);
            }
            else if (c == 'u')
            {
                return new Point(20, 1);
            }
            else if (c == 'v')
            {
                return new Point(21, 1);
            }
            else if (c == '!')
            {
                return new Point(22, 1);
            }
            else if (c == 'x')
            {
                return new Point(23, 1);
            }
            else if (c == 'y')
            {
                return new Point(24, 1);
            }
            else if (c == 'z')
            {
                return new Point(25, 1);
            }
            else if (c == '-')
            {
                return new Point(26, 1);
            }
            else if (c == '0')
            {
                return new Point(0, 2);
            }
            else if (c == '1')
            {
                return new Point(1, 2);
            }
            else if (c == '2')
            {
                return new Point(2, 2);
            }
            else if (c == '3')
            {
                return new Point(3, 2);
            }
            else if (c == '4')
            {
                return new Point(4, 2);
            }
            else if (c == '5')
            {
                return new Point(5, 2);
            }
            else if (c == '6')
            {
                return new Point(6, 2);
            }
            else if (c == '7')
            {
                return new Point(7, 2);
            }
            else if (c == '8')
            {
                return new Point(8, 2);
            }
            else if (c == '9')
            {
                return new Point(9, 2);
            }
            else if (c == '.')
            {
                return new Point(10, 2);
            }
            else if (c == ':')
            {
                return new Point(11, 2);
            }
            else
            {
                return new Point(13, 2);
            }
        }
    }
}

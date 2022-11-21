using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    class GameOver
    {
        Tile[,] tiles;

        string playerOneName, playerTwoName;
        string winner;//final;

        int winnerPoints, loserPoints, highestPoint;

        bool isTie, stopScroll;

        float timer;
        Random random;
        Color textColor;

        RenderTarget2D winnerText;
        RenderTarget2D scoreText;

        Point winnerSize, scoreSize;
        float winnerScale, scoreScale;
        float scaleLimit;

        ButtonManager buttonManager;
        Button menuButton, rematch;

        ParticleEngine particleEngine1, particleEngine2;

        public GameOver(int onePoints, int twoPoints, Tile[,] tiles)
        {
            playerOneName = "Player 1";
            playerTwoName = "Player 2";

            //buttonManager = new ButtonManager();
            //startButton = new Button(SpriteClass.StartButton, new Point(Constants.MenuSize.X / 2 - 64, Constants.MenuSize.Y * 3 / 4), new Point(128, 64), 0.5f);
            //menuButton= new Button(SpriteClass)
            //buttonManager.Add(startButton);


            this.tiles = tiles;

            timer = Constants.BoardSize / 2 + 1;

            if (onePoints == twoPoints)
            {
                isTie = true;
                winnerPoints = onePoints;
                textColor = NewColor();
            }
            else if (onePoints > twoPoints)
            {
                winner = playerOneName;
                winnerPoints = onePoints;
                loserPoints = twoPoints;
                textColor = Menu.playerOne.color;
            }
            else
            {
                winner = playerTwoName;
                winnerPoints = twoPoints;
                loserPoints = onePoints;
                textColor = Menu.playerTwo.color;
            }

            winnerScale = 0;
            scoreScale = 0;

            if (!isTie)
            {
                //if (playerOnePoints * playerTwoPoints == 0)
                //{
                //    final = playerOneName + " scored " + playerOnePoints + " points \n" + playerTwoName + " scored " + playerTwoPoints + " points \n" + winner + " absolutely destroyed it!!!";
                //    winnerText= CustomTextClass.TextToImage(playerOneName)

                //}
                //else
                //{
                //    final = playerOneName + " scored " + playerOnePoints + " points \n" + playerTwoName + " scored " + playerTwoPoints + " points \n" + winner + " is the the winner!!!";
                //}
                winnerText = CustomTextClass.TextToImage(winner + " is the winner!!!", textColor);
                scoreText = CustomTextClass.TextToImage("They won with " + winnerPoints + " vs " + loserPoints + " points", textColor);
                //winnerSize = new Point((int)(winnerText.Width * winnerScale), (int)(winnerText.Height * winnerScale));
                //scoreSize = new Point((int)(scoreText.Width * scoreScale), (int)(scoreText.Height * scoreScale));
            }
            else
            {
                winnerText = CustomTextClass.TextToImage("It ended in a tie!", textColor);
                scoreText = CustomTextClass.TextToImage("Both scored " + winnerPoints + " points", textColor);
            }
            winnerSize = CustomTextClass.ScaleRenderToPoint(winnerText, winnerScale);
            scoreSize = CustomTextClass.ScaleRenderToPoint(scoreText, scoreScale);

            scaleLimit = GameManager.GetBoardSize().X / winnerText.Width;

            random = new Random();
            particleEngine1 = new ParticleEngine(new Vector2(0, Constants.BoardSize / 3 * Constants.TileWidth), SpriteClass.Confetti, 200, textColor, 1, random);
            particleEngine2 = new ParticleEngine(new Vector2(Constants.BoardSize * Constants.TileWidth, Constants.BoardSize / 3 * Constants.TileWidth), SpriteClass.Confetti, 200, textColor, -1, random);

            //NewColor();
        }

        Color NewColor()
        {
            int R = (int)((Menu.playerOne.color.R + Menu.playerTwo.color.R) / 2);
            int G = (int)((Menu.playerOne.color.G + Menu.playerTwo.color.G) / 2);
            int B = (int)((Menu.playerOne.color.B + Menu.playerTwo.color.B) / 2);

            if (R == 127 && G == 127 && B == 127)
            {
                R = 195;
                G = 195;
                B = 195;
            }

            return new Color(R, G, B);
        }

        public void Update(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!stopScroll)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    for (int x = 0; x < tiles.GetLength(0); x++)
                    {
                        if (y > timer * 2)
                        {
                            tiles[x, y].hitbox.Y -= Constants.TileWidth / 30;
                            if (tiles[x, y].hitbox.Y + tiles[x, y].hitbox.Height < 0)
                                stopScroll = true;
                        }
                        else
                            break;
                    }
                }
            }
            else
            {
                if (scaleLimit >= winnerScale)
                {
                    winnerScale += 0.02f * scaleLimit;
                    scoreScale = winnerScale / 2;

                    winnerSize = CustomTextClass.ScaleRenderToPoint(winnerText, winnerScale);
                    scoreSize = CustomTextClass.ScaleRenderToPoint(scoreText, scoreScale);
                }
                else
                {
                    particleEngine1.Update(gameTime);
                    particleEngine2.Update(gameTime);

                }

            }
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.DrawString(SpriteClass.font, final, new Vector2(100), textColor);

            sb.Draw(winnerText, new Rectangle(new Point((GameManager.GetBoardSize().X - winnerSize.X) / 2, GameManager.GetBoardSize().Y / 4), winnerSize), Color.White);

            sb.Draw(scoreText, new Rectangle(new Point((GameManager.GetBoardSize().X - scoreSize.X) / 2, GameManager.GetBoardSize().Y / 4 + winnerSize.Y), scoreSize), Color.White);


            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = tiles.GetLength(1) - 1; y >= 0; y--)
                {
                    tiles[x, y].Draw(sb);
                }
            }

            if (stopScroll && !(scaleLimit >= winnerScale))
            {
                particleEngine1.Draw(sb);
                particleEngine2.Draw(sb);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DisplayType = OthelloMinMaxAI.Tile.DisplayState;

namespace OthelloMinMaxAI
{
    class Board
    {
        public List<Tile> Placeables { get; private set; }

        private bool useAi, currentlyAi;
        private float timer;
        private int currentPlayer, currentOpponent, lockDetection, onePoints, twoPoints;

        Tile[,] tiles;
        int[,] tileValues;


        List<Point> turnPotentials, pointsToTurn;

        bool transition, enterEndGame;
        float diskAnimationInterval;
        int timerDirection, currentDiskFrame;

        int /*mostDisksAI,*/ countDisksAI;
        Point turnDiskAI;
        float timeAI = 1;


        private DisplayType currentDisplayType => (useAi && currentlyAi) ? DisplayType.AI : DisplayType.Player;

        public Board(bool useAi)
        {
            this.useAi = useAi;

            tiles = new Tile[Constants.BoardSize, Constants.BoardSize];
            tileValues = new int[Constants.BoardSize, Constants.BoardSize];

            Placeables = new List<Tile>();
            turnPotentials = new List<Point>();
            pointsToTurn = new List<Point>();

            timerDirection = 1;
            diskAnimationInterval = 0.05f;
        }

        public int[,] TileValues => tileValues;

        /// <summary>
        /// Reset the board to the start.
        /// </summary>
        public void FreshBoard()
        {
            for (int x = 0; x < tileValues.GetLength(0); x++)
            {
                for (int y = 0; y < tileValues.GetLength(1); y++)
                {
                    tiles[x, y] = new Tile(x, y);
                    if ((x == Constants.BoardSize / 2 - 1 && y == Constants.BoardSize / 2 - 1) || (x == Constants.BoardSize / 2 && y == Constants.BoardSize / 2))
                    {
                        tileValues[x, y] = 2;
                        //tiles[x, y].TurnDisc(2, 0);
                    }
                    else if ((x == Constants.BoardSize / 2 && y == Constants.BoardSize / 2 - 1) || (x == Constants.BoardSize / 2 - 1 && y == Constants.BoardSize / 2))
                    {
                        tileValues[x, y] = 1;
                        //tiles[x, y].TurnDisc(1, 0);
                    }
                    else if (x == 0 || y == 0 || x == Constants.BoardSize - 1 || y == Constants.BoardSize - 1)
                    {
                        tileValues[x, y] = -1;
                        //tiles[x, y].IsBorder();
                    }
                    else
                    {
                        tileValues[x, y] = 0;
                    }
                }
            }
            SwitchSides();
        }

        /// <summary>
        /// Finds placeables that the player can press. May be replaced!
        /// </summary>
        public void FindPlaceables()
        {
            ShowValidMoves(false);

            Placeables.Clear();
            timer = 0;

            for (int x = 0; x < tileValues.GetLength(0); x++)
            {
                for (int y = 0; y < tileValues.GetLength(1); y++)
                {
                    if (tileValues[x, y] == currentPlayer)
                    {
                        for (int a = -1; a < 2; a++)
                        {
                            for (int b = -1; b < 2; b++)
                            {
                                if (a == 0 & b == 0)
                                    continue;

                                if (tileValues[x + a, y + b] == currentOpponent)
                                {
                                    KeepChecking(x + a, y + b, a, b);
                                }
                            }
                        }
                    }
                }
            }

            ShowValidMoves(true);

            if (lockDetection >= 2)
            {
                enterEndGame = true;
            }
            else if (Placeables.Count == 0)
            {
                lockDetection++;
                SwitchSides();
            }
            else
            {
                lockDetection = 0;
            }
        }

        private void ShowValidMoves(bool showMove)
        {
            foreach (Tile t in Placeables)
                t.displayState = showMove ? currentDisplayType : DisplayType.None;
        }

        /// <summary>
        /// Related to FindPlaceables(). Iterates in a direction until there isn't an opppoing tile. May be replaced!
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a">Direction in X</param>
        /// <param name="b"> Direction in Y</param>
        private void KeepChecking(int x, int y, int a, int b)
        {
            if (currentPlayer == 2)
            {
                countDisksAI++;
            }

            if (tileValues[x + a, y + b] == currentOpponent)
            {
                KeepChecking(x + a, y + b, a, b);
            }
            else if (tileValues[x + a, y + b] == 0)
            {
                if (!Placeables.Contains(tiles[x + a, y + b]))
                {
                    Placeables.Add(tiles[x + a, y + b]);

                    //if (activeAI && currentPlayer == 2)
                    //{
                    //    CheckTileValue(x + a, y + b);

                    //    if (mostDisksAI < countDisksAI)
                    //    {
                    //        mostDisksAI = countDisksAI;
                    //        countDisksAI = 0;
                    //        turnDiskAI = new Point(x + a, y + b);
                    //    }
                    //}
                }
            }
            else
            {
                countDisksAI = 0;
            }
        }


        /// <summary>
        /// Iterates in a direction and add opposing tiles in a list. If 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void KeepTurning(int x, int y, int a, int b)
        {
            if (tileValues[x + a, y + b] == currentOpponent)
            {
                turnPotentials.Add(new Point(x + a, y + b));
                KeepTurning(x + a, y + b, a, b);
            }
            else if (tileValues[x + a, y + b] == currentPlayer)
            {
                foreach (Point p in turnPotentials)
                {
                    pointsToTurn.Add(p);
                }

                turnPotentials.Clear();
            }
            else
            {
                turnPotentials.Clear();
            }
        }

        /// <summary>
        /// Spelet är över. Går till game over screen
        /// </summary>
        public void EndGame()
        {
            ShowValidMoves(false);

            Placeables.Clear();

            GameManager.GameOver(onePoints, twoPoints, tiles);
        }


        /// <summary>
        /// uppdaterade points innan. bör nog bytas ut.
        /// </summary>
        /// <param name="pointMaker"></param>
        /// <param name="pointLoser"></param>
        public void UpdatePoints(int pointMaker, int pointLoser)
        {
            if (pointMaker == 1)
                onePoints++;
            else if (pointMaker == 2)
                twoPoints++;

            if (pointLoser == 1)
                onePoints--;
            else if (pointLoser == 2)
                twoPoints--;
        }


        void IntToTile(int[,] intTiles)
        {
            for(int x = 0; x< intTiles.GetLength(0); x++)
            {
                for (int y = 0; y < intTiles.GetLength(1); y++)
                {
                    tiles[x, y].AssignTile(intTiles[x, y]);
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            if (!transition)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (useAi && currentPlayer == 1)
                {
                    if (timer > timeAI)
                    {
                        Point move = Tree.GetMove(TileValues);
                        MakeMove(move);
                        transition = true;
                        timer = 0;

                    }
                }
                else if (PressedATile(out Tile tile))
                {
                    if (Placeables.Contains(tile))
                    {
                        MakeMove(tile);
                        transition = true;
                        timer = 0;
                    }
                    else
                        tile.redCrossed = true;
                }
                else if (KeyMouseReader.KeyPressed(Keys.Space))
                {
                    enterEndGame = true;
                    EndGame();
                }
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= diskAnimationInterval)
                {
                    timer = 0;
                    currentDiskFrame += timerDirection;

                    for(int x = 0; x < tileValues.GetLength(0); x++)
                    {
                        for (int y = 0; y < tileValues.GetLength(1); y++)
                        {
                            if(pointsToTurn.Contains(new Point(x, y)))
                            {
                                tiles[x,y].currentDiskIndex=currentDiskFrame;
                            }
                        }
                    }

                    if (currentDiskFrame >= Tile.maxDiskIndex)
                    {
                        timerDirection = -1;
                        
                        for (int x = 0; x < tileValues.GetLength(0); x++)
                        {
                            for (int y = 0; y < tileValues.GetLength(1); y++)
                            {
                                if (pointsToTurn.Contains(new Point(x, y)))
                                {
                                    //TODO kolla varför spel ibland använder fel aktiv spelare
                                    tileValues[x, y] = currentPlayer;
                                }
                            }
                        }
                        SwitchSides();
                    }
                    else if (currentDiskFrame <= 0)
                    {
                        timerDirection = 1;
                        transition = false;
                        pointsToTurn.Clear();
                        if (enterEndGame)
                            EndGame();
                    }
                }
            }
        }

        private bool PressedATile(out Tile pressedTile)
        {
            pressedTile = null;
            if (!KeyMouseReader.LeftClick()) 
                return false;
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y].hitbox.Contains(KeyMouseReader.mouseState.Position))
                    {
                        pressedTile = tiles[x,y];
                        return true;
                    }
                }
            }
            return false;
        }

        public void MakeMove(Point tile) => MakeMove(tile.X, tile.Y);

        public void MakeMove(Tile tile) => MakeMove(tile.Index.X, tile.Index.Y);

        private void MakeMove(int x, int y)
        {
            tileValues[x, y] = currentPlayer;
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    if (a == 0 & b == 0)
                        continue;

                    if (tiles[x + a, y + b].currentTile == currentOpponent)
                    {
                        turnPotentials.Add(new Point(x + a, y + b));
                        KeepTurning(x + a, y + b, a, b);
                    }
                }
            }
            FindPlaceables();
        }

        public void SwitchSides()
        {
            if (currentPlayer == 1)
            {
                currentPlayer = 2;
                currentOpponent = 1;
                currentlyAi = false;
                GameManager.UpdateWindowTitle("Player 2's turn!");
            }
            else
            {
                currentPlayer = 1;
                currentOpponent = 2;
                currentlyAi = true;
                GameManager.UpdateWindowTitle("Player 1's turn!");
            }

            //lägg till logik för att den ska läsa av int array

            IntToTile(tileValues);
            FindPlaceables();
        }

        public Color GetCurrentColor()
        {
            if (enterEndGame)
                return Color.Gray;
            else if (currentPlayer == 1)
                return Menu.playerOne.color;
            else
                return Menu.playerTwo.color;
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw(sb);
            }
            sb.DrawString(SpriteClass.font, "Player 1 Score: " + onePoints, new Vector2(Constants.TileWidth, Constants.TileWidth * Constants.BoardSize), Menu.playerOne.color);
            sb.DrawString(SpriteClass.font, "Player 2 Score: " + twoPoints, new Vector2(Constants.TileWidth, Constants.TileWidth * Constants.BoardSize + SpriteClass.font.MeasureString("I").Y + 5), Menu.playerTwo.color);
            if (currentPlayer == 1)
            {
                sb.DrawString(SpriteClass.font, "Player 1's turn!", new Vector2(Constants.TileWidth, Constants.TileWidth * Constants.BoardSize + SpriteClass.font.MeasureString("I").Y * 2 + 10), Menu.playerOne.color);
            }
            else
            {
                sb.DrawString(SpriteClass.font, "Player 2's turn!", new Vector2(Constants.TileWidth, Constants.TileWidth * Constants.BoardSize + SpriteClass.font.MeasureString("I").Y * 2 + 10), Menu.playerTwo.color);
            }
        }
    }
}

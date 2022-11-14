using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    public enum TileState {Empty, Black, White};

    class GameBoard
    {
        private TileState[,] tiles;
        private int Width => tiles.GetLength(0);
        private int Height => tiles.GetLength(1);


        public GameBoard(TileState[,] tiles)
        {
            this.tiles = tiles;
        }

        public GameBoard(int width, int height)
        {
            tiles = new TileState[width, height];
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    int random = Game1.random.Next(30);
                    if (random > 25)
                    {
                        tiles[x, y] = TileState.White;
                    }
                    else if (random > 20 && random <= 25)
                    {
                        tiles[x, y] = TileState.Black;
                    }
                    else
                    {
                        tiles[x, y] = TileState.Empty;
                    }
                }
            }
        }

        public bool IsWithinBounds(Point point) => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        public bool IsWithinBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
        private bool IsOwn(int x, int y, Player currentPlayer)
        {
            TileState tile = tiles[x, y];
            return tile switch
            {
                TileState.Black => currentPlayer == Player.Black,
                TileState.White => currentPlayer == Player.White,
                _ => false,
            };
        }
        private bool IsOpponent(int x, int y, Player currentPlayer)
        {
            TileState tile = tiles[x, y];
            return tile switch
            {
                TileState.Black => currentPlayer == Player.White,
                TileState.White => currentPlayer == Player.Black,
                _ => false,
            };
        }


        public bool AttemptMove(Move move)
        {
            bool isMoveValid = IsWithinBounds(move.X, move.Y) && MoveIsValid(move);
            if(isMoveValid)
                PlaceToken(move);
            return isMoveValid;
        }

        private void PlaceToken(Move move)
        {

        }

        private bool MoveIsValid(Move move) => MoveIsValidDiagonal(move) || MoveIsValidHorizontal(move) || MoveIsValidVertical(move);

        private bool MoveIsValidHorizontal(Move move)
        {
            bool validChain = true;
            for (int i = move.X; i < Width; i++)
            {
                if (validChain && IsOwn(i, move.Y, move.player))
                    return true;
                else if (!IsOpponent(i, move.Y, move.player))
                    break;
            }
            for (int i = Width - 1; i >= move.X; i--)
            {
                if (validChain && IsOwn(i, move.Y, move.player))
                    return true;
                else if (!IsOpponent(i, move.Y, move.player))
                    break;
            }
            return false;
        }

        private bool MoveIsValidVertical(Move move)
        {
            bool validChain = true;
            for (int i = move.Y; i < Height; i++)
            {
                if (validChain && IsOwn(i, move.X, move.player))
                    return true;
                else if (!IsOpponent(i, move.X, move.player))
                    break;
            }
            for (int i = Height - 1; i >= move.Y; i--)
            {
                if (validChain && IsOwn(i, move.X, move.player))
                    return true;
                else if (!IsOpponent(i, move.X, move.player))
                    break;
            }
            return false;
        }

        private bool MoveIsValidDiagonal(Move move)
        {
            int closestBorder = 0;
            for (int i = 0; i < Width; i++)
            {

            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null) return;



            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Rectangle pos = new Rectangle(x * Game1.TILE_SIZE + Game1.TILE_SIZE /6, y * Game1.TILE_SIZE + 256 + Game1.TILE_SIZE / 6, (int)(Game1.TILE_SIZE / 1.5), (int)(Game1.TILE_SIZE / 1.5));
                    Vector2 recPos = new Vector2 (x * Game1.TILE_SIZE, y * Game1.TILE_SIZE + 256);
                    switch (tiles[x,y])
                    {
                        case TileState.Empty:
                            spriteBatch.Draw(Game1.rectangleTexture, recPos, Color.White);
                            break;
                        case TileState.Black:
                            spriteBatch.Draw(Game1.rectangleTexture, recPos, Color.White);
                            spriteBatch.Draw(Game1.circleTexture, pos, Color.Black);
                            break;
                        case TileState.White:
                            spriteBatch.Draw(Game1.rectangleTexture, recPos, Color.White);
                            spriteBatch.Draw(Game1.circleTexture, pos, Color.White);
                            break;
                        default:
                            spriteBatch.Draw(Game1.rectangleTexture, recPos, Color.White);
                            break;
                    }
                }
            }
            

        }
    }


}

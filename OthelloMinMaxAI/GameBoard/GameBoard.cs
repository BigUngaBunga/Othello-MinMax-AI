﻿using Microsoft.Xna.Framework;
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

        public int whitePieces;
        public int blackPieces;

        public int BlackScore => blackPieces - whitePieces;
        public int WhiteScore => whitePieces - blackPieces;


        public GameBoard(TileState[,] tiles)
        {
            this.tiles = tiles;
        }

        public GameBoard(int width, int height)
        {
            tiles = new TileState[width, height];
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
        private List<Point> GetEmptyAdjacent(int x, int y)
        {
            List<Point> result = new List<Point>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Point point = new Point(x + i, y + j);
                    if (IsWithinBounds(point) && tiles[point.X, point.Y] == TileState.Empty)
                        result.Add(point);
                }
            }
            return result;
        }

        public List<Move> GetPossibleMoves(Player player) 
        { 
            List<Move> possibleMoves = new List<Move>();
            HashSet<Point> evaluationPoints = new HashSet<Point>();

            TileState opposingTile = player == Player.Black ? TileState.White: TileState.Black;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    if (tiles[x, y] == opposingTile)
                        evaluationPoints.UnionWith(GetEmptyAdjacent(x, y));

            foreach (var point in evaluationPoints)
            {
                Move move = new Move(point, player);
                if (MoveIsValid(move))
                    possibleMoves.Add(move);
            }

            return possibleMoves;
        }

        public bool AttemptMove(Move move)
        {
            bool isMoveValid = IsWithinBounds(move.X, move.Y) && MoveIsValid(move);
            if(isMoveValid)
                PlaceToken(move);
            return isMoveValid;
        }

        //TODO flip relevant tokens to the right colour
        private void PlaceToken(Move move)
        {

        }

        private bool MoveIsValid(Move move) => MoveIsValidDiagonal(move) || MoveIsValidHorizontal(move) || MoveIsValidVertical(move);

        private bool MoveIsValidHorizontal(Move move)
        {
            for (int i = move.X; i < Width; i++)
            {
                if (IsOwn(i, move.Y, move.player))
                    return true;
                else if (!IsOpponent(i, move.Y, move.player))
                    break;
            }
            for (int i = move.X; i >= move.X; i--)
            {
                if (IsOwn(i, move.Y, move.player))
                    return true;
                else if (!IsOpponent(i, move.Y, move.player))
                    break;
            }
            return false;
        }

        private bool MoveIsValidVertical(Move move)
        {
            for (int i = move.Y; i < Height; i++)
            {
                if (IsOwn(move.X, i, move.player))
                    return true;
                else if (!IsOpponent(move.X, i, move.player))
                    break;
            }
            for (int i = move.Y; i >= move.Y; i--)
            {
                if (IsOwn(move.X, i, move.player))
                    return true;
                else if (!IsOpponent(move.X, i, move.player))
                    break;
            }
            return false;
        }

        private bool MoveIsValidDiagonal(Move move)
        {
            int x, y;
            return SearchDiagonal(false, false) || SearchDiagonal(true, false) || SearchDiagonal(false, true) || SearchDiagonal(true, true);

            bool SearchDiagonal(bool positiveX, bool positiveY)
            {
                int i = 0;
                while (true)
                {
                    ++i;
                    x = positiveX ? move.X + i : move.X - i;
                    y = positiveY ? move.Y + i : move.Y - i;
                    if (!IsWithinBounds(x, y))
                        return false;
                    if (IsOwn(x, y, move.player))
                        return true;
                    if (!IsOpponent(x, y, move.player))
                        return false;
                }
            }
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

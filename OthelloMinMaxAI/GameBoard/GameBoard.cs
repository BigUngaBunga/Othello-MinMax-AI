using Microsoft.Xna.Framework;
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
    }


}

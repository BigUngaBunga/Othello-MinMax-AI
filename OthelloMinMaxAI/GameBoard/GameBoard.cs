using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    public enum TileState {Empty, Black, White};
    public enum Turn {Black, White};

    class GameBoard
    {
        private TileState[,] tiles; 

        public bool IsWithinBounds(Point point) => point.X >= 0 && point.X < tiles.GetLength(0) && point.Y >= 0 && point.Y < tiles.GetLength(1);

        public bool EvaluateMove(Point movePoint, Turn turn)
        {
            bool isMoveValid = IsWithinBounds(movePoint) && IsValidMove(movePoint, turn);
            if(isMoveValid)
                MakeMove(movePoint, turn);
            return isMoveValid;
        }

        private void MakeMove(Point movePoint, Turn turn)
        {

        }

        private bool IsValidMove(Point movePoint, Turn turn)
        {   
            bool isValidMove = false;
            //for(int i = 0; i <){

            //}

            return false;
        }

    }


}

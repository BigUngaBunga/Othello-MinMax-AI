using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    class Tree
    {
        private Node root;
        private GameBoard gameState;

        public Tree(GameBoard gameState)
        {
            this.gameState = gameState;
        }

        public Move GetMove()
        {
            return new Move();
        }
    }
}

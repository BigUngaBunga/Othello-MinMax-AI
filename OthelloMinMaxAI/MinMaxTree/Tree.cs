using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    class Tree
    {

        private Node root;
        private GameBoard gameState;

        public Tree(GameBoard gameState, Player player)
        {
            this.gameState = gameState;
            root = new Node(gameState, new Move(), false, true, 0);
        }

        public Move GetMove()
        {
            return new Move();
        }
    }
}

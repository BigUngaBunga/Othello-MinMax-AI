using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI.Tree
{
    class Tree
    {
        private Node root;
        private GameBoard gameState;

        public Tree(GameBoard gameState)
        {
            this.gameState = gameState;
        }
    }
}

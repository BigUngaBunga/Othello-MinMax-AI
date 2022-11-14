using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI.Tree
{
    class Node
    {

        private GameBoard gameState;
        private bool isLeaf;
        private bool isMax;

        private List<Node> children;

        public Node(GameBoard gameState, bool isLeaf, bool isMax)
        {
            this.gameState = gameState;
            this.isLeaf = isLeaf;
            this.isMax = isMax;
        }

        //Getters & Setters
        public int Value { get; set; }
        public bool IsLeaf => isLeaf;
        public bool IsMax => isMax;

        public void CreateChildren()
        {

        }


    }
}

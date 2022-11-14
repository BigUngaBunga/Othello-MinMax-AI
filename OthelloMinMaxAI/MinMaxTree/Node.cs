using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    class Node
    {

        private GameBoard gameState;
        private Move move;
        private bool isLeaf;
        private bool isMax;
        private int value;
        private int depth;

        private List<Move> viableMoves;
        private List<Node> children;

        public Node(GameBoard gameState, Move move, bool isLeaf, bool isMax, int depth)
        {
            this.gameState = gameState;
            this.move = move;
            this.isLeaf = isLeaf;
            this.isMax = isMax;
            this.depth = depth;
            CalculateValue();
            
        }

        //Getters & Setters
        public int Value => value;
        public bool IsLeaf => isLeaf;
        public bool IsMax => isMax;

        private void CalculateValue()
        {
            //value = gameState.ValueForBlack;
        }

        public void CreateChildren()
        {
            foreach (Move move in viableMoves)
            {
                children.Add(new Node(gameState, move, (depth == 5), !IsMax, depth));
            }
            
        }


    }
}

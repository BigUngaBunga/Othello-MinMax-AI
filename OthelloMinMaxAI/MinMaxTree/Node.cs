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
        Player player;

        private List<Move> viableMoves;
        private List<Node> children;

        public Node(GameBoard gameState, Move move, bool isLeaf, bool isMax, int depth, Player player)
        {
            this.gameState = gameState;
            this.move = move;
            this.isLeaf = isLeaf;
            this.isMax = isMax;
            this.depth = depth;
            this.player = player;
            if (IsLeaf)
            {
                CalculateValue();
            }
            else
            {
                TraverseTree();
            }
            CreateChildren();
        }

        //Getters & Setters
        public int Value => value;
        public bool IsLeaf => isLeaf;
        public bool IsMax => isMax;

        private void CalculateValue()
        {
            value = player == Player.Black ? gameState.BlackScore : gameState.WhiteScore;
        }

        public void CreateChildren()
        {
            if (IsLeaf)
            {

                return;
            }
            foreach (Move move in viableMoves)
            {
                children.Add(new Node(gameState, move, (depth == Game1.MAX_TREE_DEPTH-1), !IsMax, depth, player == Player.Black ? Player.White : Player.Black));
            }
            
        }


    }
}

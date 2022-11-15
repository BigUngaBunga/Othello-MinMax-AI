using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    class Node
    {


        private GameBoard gameState;
        private Move move;
        private int depth;
        private bool isLeaf;
        private bool isPruned = false;
        private int value;
        public Player Player { get; private set; }
        public Player OppositePlayer => Player == Player.Black ? Player.White : Player.Black;

        private List<Move> viableMoves;
        private List<Node> children;

        public Node(GameBoard gameState, Move move, bool isLeaf, int depth, Player player)
        {
            this.gameState = gameState;
            this.move = move;
            this.isLeaf = isLeaf;
            this.depth = depth;
            this.Player = player;
            if (IsLeaf)
            {
                CalculateValue();
            }
            else
            {
                //TraverseTree();
            }
            CreateChildren();
        }

        //Getters & Setters
        public int Value => value;
        public bool IsLeaf => isLeaf;

        private void CalculateValue()
        {
            value = Player == Player.Black ? gameState.BlackScore : gameState.WhiteScore;
        }

        public void CreateChildren()
        {
            if (IsLeaf)
            {

                return;
            }
            foreach (Move move in viableMoves)
            {
                children.Add(new Node(gameState, move, (depth == Game1.MAX_TREE_DEPTH-1), depth, OppositePlayer));
            }
            
        }
    }
}

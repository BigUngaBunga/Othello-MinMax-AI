using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    abstract class Node
    {

        protected GameBoard gameState;
        protected Move move;
        protected int depth;
        protected bool isLeaf;
        public bool isPruned = false;
        protected int value;
        public Player Player { get; private set; }
        public Player OppositePlayer => Player == Player.Black ? Player.White : Player.Black;

        protected List<Move> viableMoves;
        protected List<Node> children;

        public Node(GameBoard gameState, Move move, bool isLeaf, int depth, Player player)
        {
            this.gameState = gameState;
            this.move = move;
            this.isLeaf = isLeaf;
            this.depth = depth;
            this.Player = player;
            viableMoves = gameState.GetPossibleMoves(player);
        }

        //Getters & Setters
        public int Value => value;
        public bool IsLeaf => isLeaf;

        public abstract void CalculateValue();

        public abstract void TraverseTree(ref int alpha, ref int beta);

        protected abstract void CreateChildren();
    }
}

using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OthelloMinMaxAI
{
    class Node
    {
        //Getters & Setters
        public int Value => value;
        public bool IsLeaf => isLeaf;
        public int Player { get; private set; }
        public bool isPruned = false;
        public int OppositePlayer => Player == 1 ? 2 : 1;
        public Point Move => move;
        public Node BestChild => bestChild;

        protected int[,] gameState;
        protected Point move;
        protected int depth;
        protected bool isLeaf;
        protected int value;
        protected Node bestChild;
        protected List<Point> viableMoves = new List<Point>();
        protected List<Node> children;

        public Node(int[,] gameState, Point move, bool isLeaf, int depth, int player)
        {
            this.gameState = (int[,])gameState.Clone();
            this.isLeaf = isLeaf;
            this.depth = depth + 1;
            children = new List<Node>();
            Player = player;
            this.move = move;
            if(move.X != -1 && move.Y != -1)
                AiBoard.MakeMove(this.gameState, Player, OppositePlayer, move);
            viableMoves = AiBoard.FindPlaceables(this.gameState, Player, OppositePlayer);
            EvaluateLeafStatus();
        }



        public void CalculateValue()
        {
            value = AiBoard.CalculateComparativeScore(gameState, Player);
        }

        public virtual void TraverseTree(ref int alpha, ref int beta)
        {
        }

        protected virtual void CreateChildren()
        {
            foreach (Point move in viableMoves)
            {
                children.Add(new MinNode(gameState, move, (depth == Game1.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
            }
        }

        protected void EvaluateLeafStatus()
        {
            if (viableMoves.Count == 0)
            {
                isLeaf = true;
            }
        }

        public void ExpandTree()
        {
            depth--;
            for (int i = 0; i < children.Count; i++)
            {
                children[i].ExpandTree();
            }
            if (isLeaf)
                CreateChildren();
        }

        public bool GameStateIsSame(int[,] compareGameState)//=> compareGameState.Equals(gameState);
        {
            for (int x = 0; x < compareGameState.GetLength(0); x++)
            {
                for (int y = 0; y < compareGameState.GetLength(1); y++)
                {
                    if (gameState[x, y] != compareGameState[x, y])
                        return false;
                }
            }
            return true;
        }

        public bool HasNodeWithState(int[,] targetGameState, out Node node)
        {
            node = null;
            if (isLeaf)
            {
                if (!GameStateIsSame(targetGameState))
                    return false;
                node = this;
                return true;
            }

            foreach(var child in children)
            {
                if (child.HasNodeWithState(targetGameState, out Node childNode))
                {
                    node = childNode;
                    return true;
                }
            }
            return false;
        }
    }
}

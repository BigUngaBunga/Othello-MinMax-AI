using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OthelloMinMaxAI
{
    class MaxNode : Node
    {
        public MaxNode(int[,] gameState, Point move, bool isLeaf, int parentDepth, int player) : base(gameState, move, isLeaf, parentDepth, player)
        {
            if (IsLeaf) { CalculateValue(); }
            else { CreateChildren(); }
        }

        public override void TraverseTree(int alpha, int beta, out int depthVisited, out int nodesSearched)
        {
            nodesSearched = 1;
            depthVisited = depth;

            for (int i = children.Count - 1; i >= 0; i--)
            {
                children[i].TraverseTree(alpha, beta, out depthVisited, out int _nodesSearched);
                nodesSearched += _nodesSearched;
                if (children[i].isPruned)
                {
                    children.RemoveAt(i);
                    continue;
                }

                if (alpha < children[i].Value)
                {
                    bestChild = children[i];
                    Value = alpha = children[i].Value;
                }
            }
            if (bestChild == null && children.Count > 0)
                bestChild = children[0];
        }

        protected override void CreateChildren()
        {
            foreach (Point move in viableMoves)
            {
                children.Add(new MinNode(gameState, move, (depth == Constants.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
            }
            if (children.Count > 0)
                IsLeaf = false;
        }
    }
}

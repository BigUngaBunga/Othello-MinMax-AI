using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OthelloMinMaxAI
{
    class MaxNode : Node
    {
        public MaxNode(int[,] gameState, Point move, bool isLeaf, int depth, int player) : base(gameState, move, isLeaf, depth, player)
        {
            if (isLeaf) { CalculateValue(); }
            else { CreateChildren(); }

            
        }

        public override void TraverseTree(ref int alpha, ref int beta, out int depthVisited, out int nodesSearched)
        {
            nodesSearched = 1;
            depthVisited = depth;

            int deboogAlpha = 0;
            int deboogBeta = 0;

            foreach (Node node in children)
            {
                node.TraverseTree(ref deboogAlpha, ref deboogBeta, out depthVisited, out int _nodesSearched);
                nodesSearched += _nodesSearched;
                node.CalculateValue();
                if (alpha < node.Value)
                {
                    bestChild = node;
                    value = alpha = node.Value;
                }
            }
        }

        protected override void CreateChildren()
        {
            foreach (Point move in viableMoves)
            {
                children.Add(new MinNode(gameState, move, (depth == Constants.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
            }
        }
    }
}

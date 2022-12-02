using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace OthelloMinMaxAI
{
    class MinNode : Node
    {
        public MinNode(int[,] gameState, Point move, bool isLeaf, int depth, int player) : base(gameState, move, isLeaf, depth, player)
        {
            
            if (isLeaf)
            {
                CalculateValue();
            }
            else
            {
                CreateChildren();
            }
        }

        public override void TraverseTree(ref int alpha, ref int beta, out int depthVisited, out int nodesSearched)
        {
            nodesSearched = 1;
            depthVisited = depth;

            int deboogAlpha = 0;
            int deboogBeta = 0;

            for (int i = children.Count - 1; i >= 0; i--)
            {
                children[i].TraverseTree(ref deboogAlpha, ref deboogBeta, out depthVisited, out int _nodesSearched);
                nodesSearched += _nodesSearched;
                children[i].CalculateValue();

                if (alpha >= beta)
                    children.RemoveAt(i);
                else if (beta > children[i].Value)
                {
                    bestChild = children[i];
                    value = beta = children[i].Value;
                }
            }
        }

        protected override void CreateChildren()
        {
            foreach (Point move in viableMoves)
            {
                children.Add(new MaxNode(gameState, move, (depth == Constants.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
            }
        }
    }
}

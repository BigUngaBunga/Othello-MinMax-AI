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

        public override void TraverseTree(ref int alpha, ref int beta)
        {
            foreach (Node node in children)
            {
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
                children.Add(new MinNode(gameState, move, (depth == Game1.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OthelloMinMaxAI
{
    class MinNode : Node
    {
        //public MinNode(GameBoard gameState, Move move, bool isLeaf, int depth, Player player) : base(gameState, move, isLeaf, depth, player)
        //{
        //    if (isLeaf)
        //    {
        //        CalculateValue();
        //    }
        //    else
        //    {
        //        CreateChildren();
        //    }
        //}

        //public override void CalculateValue()
        //{
        //    if (Player == Player.Black)
        //    {
        //        value = gameState.BlackScore;
        //    }
        //    else
        //    {
        //        value = gameState.WhiteScore;
        //    }
        //}

        //public override void TraverseTree(ref int alpha, ref int beta)
        //{
        //    foreach (Node node in children)
        //    {
        //        node.CalculateValue();
        //        if (alpha >= beta)
        //        {
        //            node.isPruned = true;
        //        }
        //        else if (beta > node.Value)
        //        {
        //            value = beta = node.Value;
        //        }
        //    }
        //}

        //protected override void CreateChildren()
        //{
        //    foreach (Move move in viableMoves)
        //    {
        //        children.Add(new MaxNode(gameState, move, (depth == Game1.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    class MaxNode : Node
    {
        //public MaxNode(GameBoard gameState, Move move, bool isLeaf, int depth, Player player) : base(gameState, move, isLeaf, depth, player)
        //{
        //    if (isLeaf){ CalculateValue();}
        //    else{ CreateChildren(); }

        //    gameState.AttemptMove(move);
        //}

        //public override void CalculateValue()
        //{
        //    //no pruning for a max node because it wants to get the highest value so has to check all...
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
        //        if (alpha < node.Value)
        //        {
        //            value = alpha = node.Value;
        //        }
        //    }
        //}

        //protected override void CreateChildren()
        //{
        //    foreach (Move move in viableMoves)
        //    {
        //        children.Add(new MinNode(gameState, move, (depth == Game1.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
        //    }
        //}
    }
}

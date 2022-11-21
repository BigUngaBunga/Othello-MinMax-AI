using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloMinMaxAI
{
    static class Tree
    {

        private static Node root;
        private static int[,] gameState;
        private static int alpha, beta;

        public static void InitializeTree(int[,] currentGameState, int AiPlayerIndex)
        {
            gameState = currentGameState;
            root = new MaxNode(gameState, new Point(-1, -1), false, 0, AiPlayerIndex);
        }

        /// <summary>
        /// Evaluates the tree, returns the best move, moves root down one in depth and expands the tree
        /// </summary>
        /// <param name="currentGameState">Compare the recorded gamestate with the actual gamestate and update the root node accordingly</param>
        /// <returns></returns>
        public static Point GetMove(int[,] currentGameState)
        {
            if (!root.GameStateIsSame(currentGameState) && root.HasNodeWithState(currentGameState, out Node node))
            {
                root = node;
                root.ExpandTree();
            }
            alpha = int.MinValue;
            beta = int.MaxValue;
            root.TraverseTree(ref alpha, ref beta);
            root = root.BestChild;
            root.ExpandTree();

            return root.Move;
        }
    }
}

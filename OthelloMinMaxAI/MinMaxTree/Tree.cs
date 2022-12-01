using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OthelloMinMaxAI
{
    static class Tree
    {

        private static Node root;
        private static int[,] gameState;
        private static int alpha, beta;
        private static Point emptyMove;

        public static void InitializeTree(int[,] currentGameState, int AiPlayerIndex)
        {
            gameState = currentGameState;
            emptyMove = new Point(-1, -1);
            root = new MaxNode(gameState, emptyMove, false, 0, AiPlayerIndex);
        }

        /// <summary>
        /// Evaluates the tree, returns the best move, moves root down one in depth and expands the tree
        /// </summary>
        /// <param name="currentGameState">Compare the recorded gamestate with the actual gamestate and update the root node accordingly</param>
        /// <returns></returns>
        public static Point GetMove(int[,] currentGameState)
        {
            if (root.Move != emptyMove && root.HasChildWithState(currentGameState, out Node node))
                MoveRootTo(node);
            alpha = int.MinValue;
            beta = int.MaxValue;
            root.TraverseTree(ref alpha, ref beta, out int depthVisited, out int nodesSearched);
            //TODO skriv ut depthVisited och nodesSearched
            MoveRootTo(root.BestChild);

            return root.Move;
        }

        private static void MoveRootTo(Node node)
        {
            root = node;
            root.ExpandTree();
        }
    }
}

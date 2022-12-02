using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OthelloMinMaxAI
{
    static class Tree
    {

        private static Node root;
        private static int[,] gameState;
        private static int alpha, beta;
        private static Point emptyMove;

        public static void GenerateTree(int[,] currentGameState, int AiPlayerIndex)
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (root.Move != emptyMove && root.HasChildWithState(currentGameState, out Node node))
                MoveRootTo(node);
            alpha = int.MinValue;
            beta = int.MaxValue;
            root.TraverseTree(alpha, beta, out int depthVisited, out int nodesSearched);
            MoveRootTo(root.BestChild);
            stopwatch.Stop();
            Debug.WriteLine($"Search depth: {depthVisited}, nodes searched: {nodesSearched}. It took {stopwatch.ElapsedMilliseconds} milliseconds");
            return root.Move;
        }

        private static void MoveRootTo(Node node)
        {
            root = node;
            root.ExpandTree();
        }
    }
}

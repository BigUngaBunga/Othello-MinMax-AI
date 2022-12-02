using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OthelloMinMaxAI
{
    class Tree
    {

        private Node root;
        private int[,] gameState;
        private int alpha, beta;
        private Point emptyMove;
        public string LatestMoveDescription { get; private set; }

        public Tree(int[,] currentGameState, int AiPlayerIndex)
        {
            gameState = currentGameState;
            emptyMove = new Point(-1, -1);
            root = new MaxNode(gameState, emptyMove, false, 0, AiPlayerIndex);
            LatestMoveDescription = "Has yet to perform a move";
        }

        /// <summary>
        /// Evaluates the tree, returns the best move, moves root down one in depth and expands the tree
        /// </summary>
        /// <param name="currentGameState">Compare the recorded gamestate with the actual gamestate and update the root node accordingly</param>
        /// <returns></returns>
        public  Point GetMove(int[,] currentGameState)
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
            LatestMoveDescription = $"Search depth: {depthVisited}, nodes searched: {nodesSearched}. It took {stopwatch.ElapsedMilliseconds} milliseconds";
            return root.Move;
        }

        private void MoveRootTo(Node node)
        {
            root = node;
            root.ExpandTree();
        }
    }
}

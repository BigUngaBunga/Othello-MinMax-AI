using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OthelloMinMaxAI
{
    class Node
    {
        //Getters & Setters
        public int Value { get; protected set; }
        public bool IsLeaf {get; protected set;}
        public int Player { get; private set; }
        public bool isPruned = false;
        public int OppositePlayer => Player == 1 ? 2 : 1;
        public Point Move => move;
        public Node BestChild => bestChild;

        protected int[,] gameState;
        protected Point move;
        protected int depth;
        protected Node bestChild;
        protected List<Point> viableMoves = new List<Point>();
        protected List<Node> children;

        public Node(int[,] gameState, Point move, bool isLeaf, int parentDepth, int player)
        {
            this.gameState = (int[,])gameState.Clone();
            IsLeaf = isLeaf;
            this.depth = parentDepth + 1;
            children = new List<Node>();
            Player = player;
            this.move = move;
            if(move.X != -1 && move.Y != -1)
                AiBoard.MakeMove(this.gameState, OppositePlayer, Player, move);
            viableMoves = AiBoard.FindPlaceables(this.gameState, Player, OppositePlayer);
            EvaluateLeafStatus();
        }



        public void CalculateValue()
        {
            Value = AiBoard.CalculateComparativeScore(gameState, Player);
        }

        //TODO byt ut alpha och beta från ref till lokala variabler och uppdatera rätt variabel i rätt metod.
        public virtual void TraverseTree(int alpha, int beta, out int depthVisited, out int nodesSearched)
        {
            depthVisited = 0;
            nodesSearched = 1;
        }

        protected virtual void CreateChildren()
        {
                
            foreach (Point move in viableMoves)
            {
                children.Add(new MinNode(gameState, move, (depth == Constants.MAX_TREE_DEPTH - 1), depth, OppositePlayer));
            }
            if (children.Count > 0)
                IsLeaf = false;
        }

        protected void EvaluateLeafStatus()
        {
            if (viableMoves.Count == 0)
            {
                IsLeaf = true;
            }
        }

        public void ExpandTree()
        {
            depth--;
            for (int i = 0; i < children.Count; i++)
                children[i].ExpandTree();
            if (IsLeaf && viableMoves.Count > 0)
                CreateChildren();
        }

        public bool GameStateIsSame(int[,] compareGameState)
        {
            for (int x = 0; x < compareGameState.GetLength(0); x++)
                for (int y = 0; y < compareGameState.GetLength(1); y++)
                    if (gameState[x, y] != compareGameState[x, y])
                        return false;
            return true;
        }

        public bool HasChildWithState(int[,] targetGameState, out Node node)
        {
            node = null;
            foreach(var child in children)
            {
                if (child.HasChildWithState(targetGameState, out Node childNode))
                {
                    node = childNode;
                    return true;
                }
            }
           
            if (GameStateIsSame(targetGameState))
            {
                node = this;
                return true;
            }
            return false;
        }
    }
}

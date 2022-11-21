using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static OthelloMinMaxAI.GameManager;

namespace OthelloMinMaxAI
{
    static class AiBoard
    {
        private static List<Point> turnPotentials, pointsToTurn;
        private static HashSet<Point> placables;
        //private static int[,] gameState;


        public static void MakeMove(int[,] gameState, int player, int opponent, Point move)
        {
            gameState[move.X, move.Y] = player;
            turnPotentials = new List<Point>();
            pointsToTurn = new List<Point>();
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    if (a == 0 & b == 0)
                        continue;

                    Point direction = new Point(a, b);
                    Point position = new Point(move.X + direction.X, move.Y + direction.Y);

                    if (gameState[position.X, position.Y] == opponent)
                    {
                        turnPotentials.Add(position);
                        KeepTurning(position, direction, player, opponent, gameState);
                    }
                }
            }

            TurnPoints(gameState, player);
        }

        public static int CalculateComparativeScore(int[,] gameState, int player)
        {
            int scoreAi = 0;
            int scorePlayer = 0;
            for (int x = 0; x < gameState.GetLength(0); x++)
            {
                for (int y = 0; y < gameState.GetLength(1); y++)
                {
                    if (gameState[x, y] == 1)
                    {
                        scoreAi++;
                    }
                    if (gameState[x, y] == 2)
                    {
                        scorePlayer++;
                    }
                }
            }
            if (player == 1) return scoreAi - scorePlayer;
            else return scorePlayer - scoreAi;
        }

        public static List<Point> FindPlaceables(int[,] tileValues, int player, int opponent)
        {
            placables = new HashSet<Point>();

            for (int x = 0; x < tileValues.GetLength(0); x++)
            {
                for (int y = 0; y < tileValues.GetLength(1); y++)
                {
                    if (tileValues[x, y] == player)
                    {
                        for (int a = -1; a < 2; a++)
                        {
                            for (int b = -1; b < 2; b++)
                            {
                                if (a == 0 & b == 0)
                                    continue;

                                if (tileValues[x + a, y + b] == opponent)
                                {
                                    KeepChecking(new Point(x, y), new Point(a, b), tileValues, opponent);
                                }
                            }
                        }
                    }
                }
            }

            return placables.ToList();
        }

        private static void KeepTurning(Point position, Point direction, int player, int opponent, int[,] gameState)
        {
            position += direction;
            if (gameState[position.X, position.Y] == opponent)
            {
                turnPotentials.Add(position);
                KeepTurning(position, direction, player, opponent, gameState);
            }
            else if (gameState[position.X, position.Y] == player)
            {
                foreach (Point p in turnPotentials)
                {
                    pointsToTurn.Add(p);
                }

                turnPotentials.Clear();
            }
            else
            {
                turnPotentials.Clear();
            }
        }

        private static void TurnPoints(int[,] gameState, int player)
        {
            
            for (int i = 0; i < pointsToTurn.Count; i++)
            {
                gameState[pointsToTurn[i].X, pointsToTurn[i].Y] = player;
            }
            
        }

        private static void KeepChecking(Point position, Point direction, int[,] gameState, int opponent)
        {

            position += direction;
            if (gameState[position.X, position.Y] == opponent)
            {
                KeepChecking(position, direction, gameState, opponent);
            }
            else if (gameState[position.X, position.Y] == 0)
                placables.Add(position);
        }

    
    }
}

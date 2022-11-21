using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    static class GameManager
    {
        static GraphicsDeviceManager graphics;

        static GameWindow Window;

        public enum GameState
        {
            MainMenu,
            Play,
            GameOver
        }
        public static GameState currentGameState;
        public static Board board;
        static GameOver gameOver;

        public static void Initialization(GraphicsDeviceManager g, GameWindow window)
        {
            Window = window;
            graphics = g;
            Menu.LoadMenu();
            graphics.PreferredBackBufferWidth = Constants.MenuSize.X;
            graphics.PreferredBackBufferHeight = Constants.MenuSize.Y;
            graphics.ApplyChanges();
            currentGameState = GameState.MainMenu;
        }

        public static void NewGame()
        {
            graphics.PreferredBackBufferWidth = Constants.BoardSize * Constants.TileWidth;
            graphics.PreferredBackBufferHeight = Constants.BoardSize * Constants.TileWidth + 120;
            graphics.ApplyChanges();

            currentGameState = GameState.Play;
            board = new Board();
            board.FreshBoard();
        }

        public static void GameOver(int onePoints, int twoPoints, Tile[,] tiles)
        {
            currentGameState = GameState.GameOver;
            gameOver = new GameOver(onePoints, twoPoints, tiles);
        }

        public static void UpdateWindowTitle(string text)
        {
            Window.Title = text;
        }

        public static void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    Menu.Update(gameTime);
                    break;
                case GameState.Play:
                    board.Update(gameTime);
                    break;
                case GameState.GameOver:
                    gameOver.Update(gameTime);
                    break;
            }
        }

        public static Point GetBoardSize()
        {
            return new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public static void Draw(SpriteBatch sb)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    Menu.Draw(sb);
                    break;
                case GameState.Play:
                    board.Draw(sb);
                    break;
                case GameState.GameOver:
                    gameOver.Draw(sb);
                    break;
            }
        }
    }
}
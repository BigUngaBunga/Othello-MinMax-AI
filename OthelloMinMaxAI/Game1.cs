using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace OthelloMinMaxAI
{
    public class Game1 : Game
    {
        public const int MAX_TREE_DEPTH = 5;
        public const int GAMEBOARD_SIZE = 8;
        public const int TILE_SIZE = 64;
        public const int LINE_WIDTH = 4;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        
        public static Random random = new Random();
        public static Texture2D circleTexture;
        public static Texture2D rectangleTexture;

        private GameBoard game;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = TILE_SIZE * GAMEBOARD_SIZE + 256;
            _graphics.PreferredBackBufferWidth = TILE_SIZE * GAMEBOARD_SIZE;
            //_graphics.ApplyChanges();
        }
        private Texture2D CreateRectangleTexture(int width, int height, int lineWidth, Color lineColor)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    if (x == 0 || x >= width - lineWidth || y == 0 || y >= height - lineWidth)
                    {
                        colors[index] = lineColor;
                    }
                    else
                    {
                        colors[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colors);


            return texture;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            game = new GameBoard(GAMEBOARD_SIZE, GAMEBOARD_SIZE);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            circleTexture = Content.Load<Texture2D>(@"Textures\WhiteCircle");
            rectangleTexture = CreateRectangleTexture(TILE_SIZE, TILE_SIZE, LINE_WIDTH, Color.Black);

            // TODO: use this.Content to load your game content here
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();

            game.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

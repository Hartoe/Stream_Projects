using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Conways_Game_of_Life
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        GameStateManager gameStateManager;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = "Conway's Game of Life";
            IsMouseVisible = true;
            Point GridSize = new Point(150, 100);
            int cell_width = 32;
            GameWorld world = new GameWorld();

            Console.WriteLine(GraphicsDevice.Viewport.Width);

            GraphicsHandler.Init(graphics, GraphicsDevice, Content);
            InputHandler.Init();
            gameStateManager = new GameStateManager();
            gameStateManager.Add_State("classic", new ClassicState(world, gameStateManager, GridSize, cell_width));
            gameStateManager.Add_State("dual", new DualState(world, gameStateManager, GridSize, cell_width));
            gameStateManager.Add_State("menu", new MenuState(world, gameStateManager));
            gameStateManager.Set_State("menu");
            Camera.Init(GraphicsDevice.Viewport, GridSize, cell_width, gameStateManager);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GraphicsHandler.Set_Sprite_Batch();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();
            Camera.Update();
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            GraphicsHandler.Begin();
            gameStateManager.Draw();
            GraphicsHandler.End();

            base.Draw(gameTime);
        }
    }
}

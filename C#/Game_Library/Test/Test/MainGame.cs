using Game_Library.Handlers;
using Game_Library.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        GameStateManager gsm;
        GraphicsHandler graphicsHandler;
        InputHandler inputHandler;
        Camera camera;
        GameWorld world;

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
            gsm = new GameStateManager();
            camera = new Camera(Vector2.Zero);
            world = new GameWorld();
            graphicsHandler = new GraphicsHandler(GraphicsDevice);
            inputHandler = new InputHandler();

            graphicsHandler.Add_Group("main", new Game_Library.Helpers.ShaderGroup(graphicsHandler.Foreground, null));

            gsm.Add_State("main", new State(world, camera, graphicsHandler, Content, inputHandler));
            gsm.Set_State("main");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
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
            inputHandler.Update(gameTime);
            camera.Update(gameTime);
            gsm.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            graphicsHandler.Draw(camera);
            graphicsHandler.Begin(camera);
            gsm.Draw();
            graphicsHandler.End();

            base.Draw(gameTime);
        }
    }
}

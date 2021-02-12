using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Evolutionary_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        public static Random Random;
        public static Point WindowSize;

        GraphicsDeviceManager graphics;
        GraphicsHandler graphicsHandler;
        GameWorld world;
        float time;
        int count;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Random = new Random();
            WindowSize = new Point(960, 960);
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = WindowSize.X;
            graphics.PreferredBackBufferHeight = WindowSize.Y;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphicsHandler = new GraphicsHandler(GraphicsDevice, Content);
            world = new GameWorld();
            world.Add_Object(new Grid(Vector2.Zero, graphicsHandler, world, new Point(30,30)));
            
            for (int i = 0; i < 50; i++)
                world.Add_Object(new Creature(new Vector2(Random.Next(1,WindowSize.X-1),
                    Random.Next(1,WindowSize.Y-1)), graphicsHandler, world, Random.Next(100,200),
                    Random.Next(50,80), Random.Next(100,180), Random.Next(20,70),
                    new Color(Random.Next(255), Random.Next(255), Random.Next(255))));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > 0.1)
            {
                world.Update(gameTime);
                time = 0;
                count++;
            }
            if (count == 10)
            {
                count = 0;
                world.Calculate_Average_Values();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            graphicsHandler.Begin();
            world.Draw();
            graphicsHandler.End();

            base.Draw(gameTime);
        }
    }
}

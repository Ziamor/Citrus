#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Project_Citrus;
using Project_Citrus.Engine.Components;
using Project_Citrus.lua;
using Project_Citrus.Engine;
#endregion

namespace Project_Citrus
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Citrus : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static EntityManager entityManager;
        public static KeyboardManager keyboardManager;
        public Citrus()
            : base()
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
            // TODO: Add your initialization logic here                     
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ContentLoader.content_manager = Content;

            entityManager = new EntityManager(spriteBatch);
            Program.registerLuaFunctions(entityManager);  

            keyboardManager = new KeyboardManager();
            Program.registerLuaFunctions(keyboardManager);           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            keyboardManager.UpdateState();
            if (keyboardManager.IsKeyPressed(Keys.R))
            {               
                try
                {
                    Program.pLuaVM.DoFile(@"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\scripts\" + "world_test.lua");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            entityManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            entityManager.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

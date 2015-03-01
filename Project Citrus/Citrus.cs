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
using Project_Citrus.Engine.lua;
using Project_Citrus.Engine;
using Project_Citrus.Engine.ContentLoading;
using Project_Citrus.Engine.Menus;
using Project_Citrus.Engine.Managers;
using Project_Citrus.Engine.Menus.Widgets;
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
        public static MenuManager menuManager;
        private Menu main_menu;
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
            this.IsMouseVisible = true;
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

            menuManager = new MenuManager();
            Program.registerLuaFunctions(menuManager);

            //JSON_Loader.Write_Menu(new Menu("main_menu", false, true, true, new Button("play")));
            //main_menu = JSON_Loader.Get_Menu("main_menu");
            Vector2 screen_size = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            Panel pnl_buttons = new Panel("pnl_buttons", new Vector2(20, 20), new Vector2(100, 100), Anchor.TOP_LEFT);
            Button btn_play = new Button("btn_play", "Play", new Vector2(10, 10), Anchor.TOP_LEFT, Anchor.TOP_LEFT);
            pnl_buttons.Add_Widget(btn_play);

            btn_play.Image_Name = "button_green";
            pnl_buttons.Image_Name = "panel";

            main_menu = new Menu("main_menu", screen_size, false, true, true, pnl_buttons);
            menuManager.Add_Menu(main_menu);
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
                    // Program.pLuaVM.DoFile(@"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\scripts\" + "world_test.lua");
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
            menuManager.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

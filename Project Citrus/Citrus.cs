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
using Engine;
using Engine.Entities.Types;
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
        Entity worker = null;
        Entity worker2 = null;
        Entity wall = null;
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

            //Entity_Factory.Write(new Entity("worker3", new Type_Worker(), "worker", new String[] { "drawable", "solid", "unit" }));
            worker = JSON_Loader.Get_Entity("worker");
            worker2 = JSON_Loader.Get_Entity("worker");
            wall = JSON_Loader.Get_Entity("wall");
           // worker = entity_loader.
            /*Entity_Factory.read2("worker2");
            Sprite sprite = new Sprite("images\\worker","worker");
            JSON_Helper<Sprite> spriteJSON = new JSON_Helper<Sprite>();
            spriteJSON.Write(sprite,"worker", Type.Image);
            worker = Entity_Factory.get("worker");
            wall = Entity_Factory.get("wall");

            Entity.Add_Entity(worker);
            Entity.Add_Entity(wall);
            Entity.Data_Dump();*/
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
            spriteBatch.Draw(worker.Sprite.Tex, Vector2.Zero, Color.White);
            spriteBatch.Draw(worker2.Sprite.Tex, new Vector2(150, 0), Color.White);
            spriteBatch.Draw(wall.Sprite.Tex, new Vector2(50,0), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

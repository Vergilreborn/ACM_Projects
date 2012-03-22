using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Media;

namespace ACM2D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player redMan;
        Player blueMan;
        SpriteFont font;
        KeyboardState current, previous;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            
            graphics.ApplyChanges();
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

            //Initalize the keyBoardStates
            current = previous = Keyboard.GetState();

            //initialize the player by Texture,position,width,height,speed
            redMan = new Player(Content.Load<Texture2D>("RedMan"), Content.Load<Texture2D>("Sprites/ShipSprite1"), new Vector2(50, 50),
                                new Rectangle(0,0,60,60), new Rectangle(0,0,32,32), 4.0f,0);
            blueMan = new Player(Content.Load<Texture2D>("BlueMan"),Content.Load<Texture2D>("Sprites/ShipSprite2"), new Vector2(graphics.PreferredBackBufferWidth - 120, 50),
                                new Rectangle(0,0,60,60), new Rectangle(0,0,32,32), 4.0f,1);


            //initialize font
            font = Content.Load<SpriteFont>("SpriteFont1");
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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //get the keyboard state
            current = Keyboard.GetState();

            if (current.IsKeyDown(Keys.Tab) && previous.IsKeyDown(Keys.Tab))
                graphics.ToggleFullScreen();
            


            //update the redMan
            redMan.update(gameTime, current,previous);
            blueMan.update(gameTime, current,previous);

            base.Update(gameTime);

            //set the previous state to current
            previous = current;

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
           
            

            //begin drawing 
            spriteBatch.Begin();

            //Draws the player
            redMan.DrawShip(spriteBatch);
            blueMan.DrawShip(spriteBatch);
            redMan.DrawAura(spriteBatch,font);
            blueMan.DrawAura(spriteBatch,font);
           
            //Stops drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

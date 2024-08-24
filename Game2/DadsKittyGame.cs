using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace DadGames
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DadsKittyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D cat1, cat2;
        Vector2 catPos = new(100, 100);
        SpriteEffects catFx = SpriteEffects.None;
        int moveFactor = 2;
        int animationRate = 5;
        bool moveFrame;

        public DadsKittyGame()
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
            Window.Title = "Dad's Kitty Game";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Use this.Content to load your game content here
            cat1 = Content.Load<Texture2D>("black_kitty_1t");
            cat2 = Content.Load<Texture2D>("black_kitty_2t");
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
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = default;
            try { gamePadState = GamePad.GetState(PlayerIndex.One); }
            catch (NotImplementedException) { /* ignore gamePadState */ }

            if (keyboardState.IsKeyDown(Keys.Escape) ||
                keyboardState.IsKeyDown(Keys.Back) ||
                gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                try { Exit(); }
                catch (PlatformNotSupportedException) { /* ignore */ }
            }

            // TODO: Add your update logic here
            moveFrame = false;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                catPos.X -= moveFactor;
                catFx = SpriteEffects.FlipHorizontally;
                if (catPos.X < 0)
                    catPos.X = 0;
                else
                    moveFrame = ((int)catPos.X / (moveFactor * animationRate)) % 2 == 1;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                catPos.X += moveFactor;
                catFx = SpriteEffects.None;
                moveFrame = ((int)catPos.X / (moveFactor * animationRate)) % 2 == 1;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                catPos.Y -= moveFactor;
                if (catPos.Y < 0)
                    catPos.Y = 0;
                else
                    moveFrame = ((int)catPos.Y / (moveFactor * animationRate)) % 2 == 1;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                catPos.Y += moveFactor;
                moveFrame = ((int)catPos.Y / (moveFactor * animationRate)) % 2 == 1;
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
            spriteBatch.Begin();
            spriteBatch.Draw(moveFrame ? cat2 : cat1, catPos, null, Color.White, 0, Vector2.Zero, Vector2.One, catFx, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xamarin.Essentials;

namespace AccelerometerSample
{
    /// <summary>
    /// A simple example showing how to use the accelerometer to move
    /// a sprite around the screen
    /// </summary>
    public class AccelerometerGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _asteroidTexture;
        private Texture2D _backgroundTexture;

        private Vector2 _logoPosition;
        private Vector2 _logoVelocity;

        public AccelerometerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.SupportedOrientations = Microsoft.Xna.Framework.DisplayOrientation.Portrait;
            _graphics.ApplyChanges();
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
            //Initialize the Accelerometer
            Accelerometer.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load the sprite's texture
            _asteroidTexture = Content.Load<Texture2D>("asteroid");

            // load the background texture
            _backgroundTexture = Content.Load<Texture2D>("space");

            // center the sprite on screen
            Viewport viewport = _graphics.GraphicsDevice.Viewport;
            _logoPosition = new Vector2(
                (viewport.Width - _asteroidTexture.Width) / 2,
                (viewport.Height - _asteroidTexture.Height) / 2);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //poll the acceleration value
            Vector3 acceleration = Accelerometer.GetState().Acceleration;

            _logoVelocity.X += acceleration.X;
            _logoVelocity.Y += -acceleration.Y;

            _logoPosition += _logoVelocity;

            // keep the sprite on the screen - clamp X
            Viewport viewport = _graphics.GraphicsDevice.Viewport;

            if (_logoPosition.X < 0)
            {
                _logoPosition.X = 0;
                _logoVelocity.X = 0;
            }
            else if (_logoPosition.X > viewport.Width - _asteroidTexture.Width)
            {
                _logoPosition.X = viewport.Width - _asteroidTexture.Width;
                _logoVelocity.X = 0;
            }

            // keep the sprite on the screen - clamp Y
            if (_logoPosition.Y < 0)
            {
                _logoPosition.Y = 0;
                _logoVelocity.Y = 0;
            }
            else if (_logoPosition.Y > viewport.Height - _asteroidTexture.Height)
            {
                _logoPosition.Y = viewport.Height - _asteroidTexture.Height;
                _logoVelocity.Y = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.  In this case, render
        /// the space background and asteroid at the desired coordinates
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            _spriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, null, Color.White);

            _spriteBatch.Draw(_asteroidTexture, _logoPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

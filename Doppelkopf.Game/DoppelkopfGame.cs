using System;
using Doppelkopf.Client.GameRunner;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Doppelkopf.Client.Input;
using Doppelkopf.Client.GUI;

namespace Doppelkopf.Client
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DoppelkopfGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        Screen currentScreen;
        ActionController Actions;

        private static DoppelkopfGame instance;
        public static DoppelkopfGame Instance {
            get { return instance; }
        }

        public Screen CurrentScreen {
            get { return currentScreen; }
        }

        public InputManager Input {
            get { return this.input; }
        }

        public TextRenderer TextRenderer {
            get;
        }

        public Runner Runner {
            get; set;
        }

        public DoppelkopfGame() {
            instance = this;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferHeight = 800;

            input = new InputManager(this);
            this.TextRenderer = new TextRenderer(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            this.Runner = new Runner();
            this.Runner.Start();
            this.IsMouseVisible = true;

            this.Actions = new ActionController(this);

            this.currentScreen = new MainScreen(this);
            this.currentScreen.Activate();

            base.Initialize();
        }

        protected override void OnExiting(object sender, EventArgs args) {
            this.currentScreen.Deactivate();
            this.Runner.Stop();
            base.OnExiting(sender, args);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            CardRenderer.Initialize(Content);
            this.TextRenderer.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            currentScreen?.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

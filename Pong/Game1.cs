using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player pLeft;
        Player pRight;
        Ball ball;
        int hits = 0;
        SpriteFont font;
        int winner = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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
            font = Content.Load<SpriteFont>("font");
            pLeft = new Player();
            pRight = new Player();
            ball = new Ball();
            Player.Sprite = Content.Load<Texture2D>(@"Sprites\player");
            ball.Sprite = Content.Load<Texture2D>(@"Sprites\ball");
            pLeft.Position = new Vector2(40, 20);
            pRight.Position = new Vector2(760 - Player.Sprite.Width, 20);
            ball.Position = new Vector2(400, 300);
            Random r = new Random();
            ball.Velocity = new Vector2(r.Next(-2, 2), r.Next(-2, 2));

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

            ball.Position += ball.Velocity;
            if (ball.getHitbox().Intersects(pLeft.getHitbox()) || ball.getHitbox().Intersects(pRight.getHitbox()))
            {
                ball.Velocity = new Vector2(-Math.Sign(ball.Velocity.X) * (Math.Min(Math.Abs(ball.Velocity.X) + 0.5f, 30)), ball.Velocity.Y);
                hits++;
            }

            if (ball.getHitbox().Top < 2)
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            else if(ball.getHitbox().Bottom > graphics.PreferredBackBufferHeight - 2)
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);



            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.R))
            {
                Random r = new Random();
                ball.Velocity = new Vector2(r.Next(-2, 2), r.Next(-2, 2));
                ball.Position = new Vector2(400, 300);
                hits = 0;
            }
            if (keys.IsKeyDown(Keys.W))
                pLeft.Position -= new Vector2(0, 5);
            if (keys.IsKeyDown(Keys.S))
                pLeft.Position += new Vector2(0, 5);
            if (keys.IsKeyDown(Keys.Up))
                pRight.Position -= new Vector2(0, 5);
            if (keys.IsKeyDown(Keys.Down))
                pRight.Position += new Vector2(0, 5);

            if (pLeft.Position.Y > graphics.PreferredBackBufferHeight - Player.Sprite.Bounds.Height)
                pLeft.Position = new Vector2(pLeft.Position.X, graphics.PreferredBackBufferHeight - Player.Sprite.Bounds.Height);
            else if(pLeft.Position.Y < 0)
                pLeft.Position = new Vector2(pLeft.Position.X, 0);

            if (pRight.Position.Y > graphics.PreferredBackBufferHeight - Player.Sprite.Bounds.Height)
                pRight.Position = new Vector2(pRight.Position.X, graphics.PreferredBackBufferHeight - Player.Sprite.Bounds.Height);
            else if (pRight.Position.Y < 0)
                pRight.Position = new Vector2(pRight.Position.X, 0);

            if (ball.getHitbox().Right > graphics.PreferredBackBufferWidth)
                winner = 1;
            else if (ball.getHitbox().Left < 0)
                winner = 2;
            else winner = 0;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(Player.Sprite, pLeft.getHitbox(), Color.White);
            spriteBatch.Draw(Player.Sprite, pRight.getHitbox(), Color.White);
            spriteBatch.Draw(ball.Sprite, ball.getHitbox(), Color.White);
            spriteBatch.DrawString(font, "Hits: " + hits, new Vector2(300, 10), Color.White);
            switch (winner)
            {
                case 1: spriteBatch.DrawString(font, "Left wins!", new Vector2(300, 300), Color.White); break;
                case 2: spriteBatch.DrawString(font, "Right wins!", new Vector2(300, 300), Color.White); break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

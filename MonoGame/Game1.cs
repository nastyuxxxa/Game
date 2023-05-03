using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Player player;
        private Evil evil;
        Texture2D spriteTexture;
        Texture2D evilTexture;
        Texture2D blockTexture1;
        Texture2D blockTexture2;
        Vector2 spritePosition = Vector2.Zero; // текущая позиция спрайта
        Vector2 evilSpritePosition; // текущая позиция злого спрайта
        float spriteSpeed = 3f; // шаг перемещения
        float evilSpriteSpeed = 3f/2;
        Point spriteSize;
        Point evilSpriteSize;
        Color color = Color.CornflowerBlue;
        int numberLevel = 1; //номер уровня
        List<Block> blocks = new List<Block>();
        KeyboardState Oldkeys;

        private List<Rectangle> collisionRects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            spritePosition = Vector2.Zero;
            graphics.PreferredBackBufferHeight = 334; // высота карты
            graphics.PreferredBackBufferWidth = 606; // ширина карты
            evilSpritePosition = new Vector2(graphics.PreferredBackBufferWidth/2, 59);
            
        }

        protected override void Initialize() // главные настройки, касающиеся всей игры
        {
            
            base.Initialize();
        }

        protected override void LoadContent() // ресурсы игры, такие как изображения, звуки, шрифты
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteTexture = Content.Load<Texture2D>("pink"); // загружаем изображение
            evilTexture = Content.Load<Texture2D>("Fall");

            blockTexture1 = Content.Load<Texture2D>("block");
            blockTexture2 = Content.Load<Texture2D>("block2");
            collisionRects = new List<Rectangle>();
            Level.CreateMaps(blocks, numberLevel, blockTexture1, blockTexture2);
            foreach (var b in blocks)
            {
                collisionRects.Add(new Rectangle(b.rectangle.X, b.rectangle.Y, b.rectangle.Width, b.rectangle.Height));
            }

            player = new Player(Content.Load<Texture2D>("pink_run"),  Content.Load<Texture2D>("pink"));
            spriteSize = new Point(spriteTexture.Width, spriteTexture.Height);
            evilSpriteSize = new Point(evilTexture.Width, evilTexture.Height);
        }

        protected override void Update(GameTime gameTime) // обновление состояния
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            evilSpritePosition.X += evilSpriteSpeed;
            if (evilSpritePosition.X > Window.ClientBounds.Width - 4*evilTexture.Width || evilSpritePosition.X < (Window.ClientBounds.Width / 2))
                evilSpriteSpeed *= -1;

            if (keyboardState.IsKeyDown(Keys.F) && Oldkeys.IsKeyDown(Keys.F))
            {
                numberLevel++;
            }
            Oldkeys = keyboardState;

            var initPos = player.position;
            player.Update();
            // столкновения по y
            foreach (var rect in collisionRects)
            {
                player.isFalling = true;
                if (rect.Intersects(player.playerFallRect))
                {
                    player.isFalling = false;
                    break;
                }
            }

            // столкновения по x
            foreach(var rect in collisionRects)
            {
                if (rect.Intersects(player.hitbox))
                {
                    player.position.X = initPos.X;
                    player.velocity.X = initPos.X;
                    break;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) // прорисовка
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // отрисовка спрайта
            spriteBatch.Begin();
            player.Draw(spriteBatch, gameTime);
            spriteBatch.Draw(evilTexture, evilSpritePosition, Color.White);

            foreach (var block in blocks)
                block.Draw(spriteBatch);

            //evil.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool Collide() // обработка столкновений
        {
            Rectangle spriteRect = new Rectangle((int)spritePosition.X,
                (int)spritePosition.Y, spriteSize.X, spriteSize.Y);
            Rectangle evilSpriteRect = new Rectangle((int)evilSpritePosition.X,
                (int)evilSpritePosition.Y, evilSpriteSize.X, evilSpriteSize.Y);

            return spriteRect.Intersects(evilSpriteRect);
        }
    }
}
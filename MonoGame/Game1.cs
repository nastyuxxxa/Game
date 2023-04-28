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
        Texture2D texture;
        Texture2D evilTexture;
        Vector2 spritePosition = Vector2.Zero; // текущая позиция спрайта
        Vector2 evilSpritePosition; // текущая позиция злого спрайта
        float spriteSpeed = 5f; // шаг перемещения
        //float evilSpriteSpeed = 2f;
        //int frameWidth = 43;
        //int frameHeight = 60;
        //Point currentFrame = new Point(0, 0);
        //Point spriteSize = new Point(14, 1);
        //Point evilSpriteSize;
        //int currentTime = 0; // сколько времени прошло
        //int period = 55; // период обновления в миллисекундах

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() // главные настройки, касающиеся всей игры
        {
            
            base.Initialize();
        }

        protected override void LoadContent() // ресурсы игры, такие как изображения, звуки, шрифты
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("run"); // загружаем изображение
        }

        protected override void Update(GameTime gameTime) // обновление состояния
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //// добавляем к текущему времени прошедшее время
            //currentTime += gameTime.ElapsedGameTime.Milliseconds;
            //// если текущее время превышает период обновления спрайта
            //if (currentTime > period)
            //{
            //    currentTime -= period; // вычитаем из текущего времени период обновления

            //    if (spritePosition.X > Window.ClientBounds.Width - frameWidth || spritePosition.X < 0)
            //    {
            //        spriteSpeed *= -1;
            //        ++currentFrame.Y;
            //        if (currentFrame.Y >= spriteSize.Y)
            //            currentFrame.Y = 0;
            //    }

            //    ++currentFrame.X; // переходим к следующему фрейму в спрайте
            //    if (currentFrame.X >= spriteSize.X)
            //    {
            //        currentFrame.X = 0;
            //    }
            //}

            if (keyboardState.IsKeyDown(Keys.Left))
                spritePosition.X -= spriteSpeed;
            if (keyboardState.IsKeyDown(Keys.Right))
                spritePosition.X += spriteSpeed;
            if (keyboardState.IsKeyDown(Keys.Up))

            if (keyboardState.IsKeyDown(Keys.Down))


            //// проверяем, не убежал ли наш спрайт с игрового поля
            //if (spritePosition.X < 0)
            //    spritePosition.X = 0;
            //if (spritePosition.Y < 0)
            //    spritePosition.Y = 0;
            //if (spritePosition.X > Window.ClientBounds.Width - spriteSize.X)
            //    spritePosition.X = Window.ClientBounds.Width - spriteSize.X;
            //if (spritePosition.Y > Window.ClientBounds.Height - spriteSize.Y)
            //    spritePosition.Y = Window.ClientBounds.Height - spriteSize.Y;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) // прорисовка
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            spriteBatch.Draw(texture, spritePosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool Collide() // обработка столкновений
        {
            return true;
        }
    }
}
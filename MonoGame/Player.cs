using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Player : Entity
    {
        public Vector2 velocity;
        public Rectangle playerFallRect;

        public float playerSpeed = 2;
        public float fallSpeed = 4; // скорость падения
        public float jumpSpeed = -8;
        public float startY;
        public bool isFalling = true;
        public bool isJumping;

        public Animation[] playerAnimation;
        public currentAnimation playerAnimationController;

        public Player(Texture2D spriteRun, Texture2D spriteIdle)
        {
            //spriteList = sprite
            position = new Vector2();
            velocity = new Vector2();
            
            playerAnimation = new Animation[2];
            playerAnimation[0] = new Animation(spriteIdle);
            playerAnimation[1] = new Animation(spriteRun);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 25);
            playerFallRect = new Rectangle((int)position.X + 3, (int)position.Y + 32, 25, 1);
        }
        public override void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();


            playerAnimationController = currentAnimation.Idle;

            if (isFalling)
                velocity.Y += fallSpeed;

            Move(keyboardState);
            //Jump(keyboardState);

            position = velocity;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            playerFallRect.X = (int)position.X;
            playerFallRect.Y = (int)position.Y + 32;
        }

        private void Move(KeyboardState keyboardState)
        {
            // перемещение клавиатурой
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X -= playerSpeed;
                playerAnimationController = currentAnimation.Run;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X += playerSpeed;
                playerAnimationController = currentAnimation.Run;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y += jumpSpeed;
                playerAnimationController = currentAnimation.Run;
            }
        }

        private void Jump(KeyboardState keyboardState)
        {
            startY = position.Y;
            if (isJumping)
            {
                position.Y += jumpSpeed;
                jumpSpeed += 1;
                if (position.Y >= startY)
                {
                    position.Y = startY;
                    isJumping = false;
                }
            }
            else
            { 
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    isJumping = true;
                    jumpSpeed = -8;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (playerAnimationController)
            {
                case currentAnimation.Idle:
                    playerAnimation[0].Draw(spriteBatch, position, gameTime, 100);
                    break;
                case currentAnimation.Run:
                    playerAnimation[1].Draw(spriteBatch, position, gameTime, 100);
                    break;
            }

            //spriteBatch.Draw(spriteList, position, Color.White);

        }
    }
}

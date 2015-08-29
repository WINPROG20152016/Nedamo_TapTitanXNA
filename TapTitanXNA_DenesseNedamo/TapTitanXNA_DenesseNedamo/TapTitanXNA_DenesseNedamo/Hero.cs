using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_DenesseNedamo
{
    public class Hero
    {
        public Vector2 playerPosition;

        Texture2D idle;
        Texture2D run;
        //Texture2D enemy;

        ContentManager content;
        public static float windowWidth = 400;
        public static float windowHeight = 500;
        Level level;
        float posX;
        float posY;
        //int counter = 0;

        Animation idleAnimation;
        Animation ramonaRun;
        Animation scottRun;
        Animation kimIdle;
        Animation slime;
        public AnimationPlayer spritePlayer;
        public Rectangle textureRec;
        
        public int characterChooser;
        //int winner = 0;

        public bool isWinner = false;

        public Hero(ContentManager content, Level level, int characterChooser)
        {
            this.content = content;
            this.level = level;
            this.characterChooser = characterChooser;
        }

        public void LoadContent()
        {
            if (characterChooser == 1)
            {
                idle = content.Load<Texture2D>("Sprite/Idle");
                run = content.Load<Texture2D>("Sprite/Run");
                ramonaRun = new Animation(run, 0.1f, true, 8);
                posX = 10.0f;
                posY = 3.0f;

            }
            else if (characterChooser == 2)
            {
                idle = content.Load<Texture2D>("Sprite/RunScott");
                run = content.Load<Texture2D>("Sprite/REALRunScott");
                scottRun = new Animation(run, 0.1f, true, 8);
                posX = 10.0f;
                posY = 2.0f;
            }
            else if (characterChooser == 3)
            {
                idle = content.Load<Texture2D>("Sprite/IdleKim");
                kimIdle = new Animation(idle, 0.1f, true, 6);
                posX = 10.0f;
                posY = 4.0f;
            }
            else if (characterChooser == 0)
            {
                idle = content.Load<Texture2D>("Sprite/slime");
                slime = new Animation(idle, 0.1f, true, 2);
                posX = 0.5f;
                posY = 2.5f;
            }

            if (characterChooser == 0)
            {
                idleAnimation = new Animation(idle, 0.3f, true, 2);
            } else {
                idleAnimation = new Animation(idle, 0.1f, true, 7);
            }


            float positionX = (windowWidth / posX) - (idleAnimation.FrameWidth / posX);
            float positionY = (windowHeight / posY) - (idleAnimation.FrameHeight / posY);
            playerPosition = new Vector2((float)positionX, (float)positionY);

            spritePlayer.PlayAnimation(idleAnimation);
            textureRec = new Rectangle((int)positionX, (int)positionY, idleAnimation.FrameWidth, idleAnimation.FrameHeight);
            
        }

        public void Update(GameTime gameTime)
        {
            //do
            //{
            if (characterChooser == 1)
            {
                if (level.mouseState.LeftButton == ButtonState.Pressed && level.mouseState.RightButton == ButtonState.Pressed && playerPosition.X <= windowWidth + 330)
                {
                    playerPosition.X += 10;
                    spritePlayer.PlayAnimation(ramonaRun);
                }
            }
            else if (characterChooser == 2)
            {
                if (level.keyState.IsKeyDown(Keys.Right) && level.keyState.IsKeyDown(Keys.Left) && playerPosition.X <= windowWidth + 330)
                {
                    playerPosition.X += 10;
                    spritePlayer.PlayAnimation(scottRun);
                }
            }
            else if (characterChooser == 0)
            {
                    playerPosition.X -= 0.5f;
            }

            if (playerPosition.X >= windowWidth + 300)
            {
                isWinner = true;
                spritePlayer.PlayAnimation(idleAnimation);
            }

            //} while (isWinner == false);
            textureRec.X = (int)playerPosition.X;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
                /*spriteBatch.Draw(player,
                playerPosition,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f);*/
            spritePlayer.Draw(gameTime, spriteBatch, playerPosition, SpriteEffects.None);

        }
    }
}

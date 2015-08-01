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
    public class Support1
    {
        Vector2 playerPosition;

        Texture2D support1;
        ContentManager content;
        public static int windowWidth = 400;
        public static int windowHeight = 500;
        Level level;

        Animation scottIdle;
        AnimationPlayer spritePlayer;

        public Support1(ContentManager content, Level level)
        {
            this.content = content;
            this.level = level;
        }

        public void LoadContent()
        {

            support1 = content.Load<Texture2D>("Sprite/RunScott");
            
            scottIdle = new Animation(support1, 0.1f, true, 7);

            int positionX = (windowWidth / 4) - (support1.Width / 4);
            int positionY = (windowHeight / 2) - (support1.Height / 2);
            playerPosition = new Vector2((float)positionX, (float)positionY);

            spritePlayer.PlayAnimation(scottIdle);
        }

        public void Update(GameTime gameTime)
        {
            /*if (level.mouseState.LeftButton == ButtonState.Pressed && level.oldMouseState.LeftButton == ButtonState.Released)
            {
                playerPosition.X += 10;
                spritePlayer.PlayAnimation(ramonaRun);
            }*/

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TapTitanXNA_DenesseNedamo
{
    enum BState
    {
        UP, HOVER, DOWN, RELEASED,
    }
    
    public class Button
    {
        ContentManager content;
        string buttonName;
        Vector2 buttonPosition;

        Texture2D buttonTexture;
        Rectangle buttonRectangle;
        Color buttonColor;
        BState bstate;
        double timer;
        double frameTime;

        public Button(ContentManager content, string buttonName, Vector2 buttonPosition)
        {
            this.content = content;
            this.buttonName = buttonName;
            this.buttonPosition = buttonPosition;

            LoadContent();
        }

        public void LoadContent()
        {
            buttonTexture = content.Load<Texture2D>(buttonName);
            buttonRectangle = new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, buttonTexture.Width, buttonTexture.Height);
            buttonColor = Color.White;
            bstate = BState.UP;
            timer = 0.0f;
        }

        public bool Update(GameTime gameTime, int mx, int my, bool mpressed, bool prev_mpressed)
        {
            frameTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            if (hitImageAlpha(buttonRectangle, buttonTexture, mx, my))
            {
                timer = 0.0;
                if (mpressed)
                {
                    // mouse is currently down
                    bstate = BState.DOWN;
                    buttonColor = Color.LightSkyBlue;
                }
                else if (!mpressed && prev_mpressed)
                {
                    // mouse was just released
                    if (bstate == BState.DOWN)
                    {
                        // button i was just down
                        bstate = BState.RELEASED;
                    }
                }
                else
                {
                    bstate = BState.HOVER;
                    buttonColor = Color.LightBlue;
                }
            }
            else
            {
                bstate = BState.UP;
                if (timer > 0)
                {
                    timer = timer - frameTime;
                }
                else
                {
                    buttonColor = Color.White;
                }
            }

            if (bstate == BState.RELEASED)
            {
                return true;
            }

            return false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, buttonRectangle, buttonColor);
        }

        public bool hitImage(float tx, float ty, Texture2D texture, int x, int y)
        {
            return (x >= tx && x <= tx + texture.Width && y >= ty && y <= ty + texture.Height);
        }

        public bool hitImageAlpha(Rectangle rect, Texture2D texture, int x, int y)
        {
            return hitImageAlpha(0, 0, texture, texture.Width * (x - rect.X) / rect.Width, texture.Height * (y - rect.Y) / rect.Height);
        }

        public bool hitImageAlpha(float tx, float ty, Texture2D texture, int x, int y)
        {
            if (hitImage(tx, ty, texture, x, y))
            {
                int[] data = new int[texture.Width * texture.Height];
                texture.GetData<int>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    texture.Width < texture.Width * texture.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * texture.Width
                        ] &
                                0xFF000000) >> 24) > 20;
                }
            }
            return false;
        }

    }
}

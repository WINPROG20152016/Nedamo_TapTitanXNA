﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_DenesseNedamo
{
    public class Level
    {
        public static int windowWidth = 800;
        public static int windowHeight = 285;
        public int HP = 100;

        #region Properties
        ContentManager content;
        Texture2D background;

        public MouseState oldMouseState;
        public MouseState mouseState;

        public KeyboardState oldKeyState;
        public KeyboardState keyState;

        bool mpressed, prev_mpressed = false;
        bool kpressed, prev_kpressed = false;

        int mouseX, mouseY;

        public Hero hero;
        Hero support1;
        Hero support2;
        public Hero enemy1;

        //SpriteFont damageStringfont;
        SpriteFont winnerStringfont;
        SpriteFont instructionsStringfont;
        SpriteFont ramonaLife, scottLife;
        string winner;

        //int damageNumber = 0;

        //Button playButton;
        //Button atkButton;

        bool isFirstWinner = false;

        ///List<Hero> heroes = new List<Hero>();

        #endregion

        public Level(ContentManager content)
        {
            this.content = content;

            hero        = new Hero(content, this, 1);
            support1    = new Hero(content, this, 2);
            support2    = new Hero(content, this, 3);
            enemy1      = new Hero(content, this, 0);
            
            //heroes.Add(hero);
        }

        public void LoadContent()
        {
            background = content.Load<Texture2D>("BG/bg");

            //damageStringfont = content.Load<SpriteFont>("SpriteFont1");
            instructionsStringfont = content.Load<SpriteFont>("SpriteFont1");
            winnerStringfont = content.Load<SpriteFont>("SpriteFont1");
            ramonaLife = content.Load<SpriteFont>("SpriteFont2");
            scottLife = content.Load<SpriteFont>("SpriteFont2");
            //playButton = new Button(content, "Sprite/buttonGo", Vector2.Zero);
            //atkButton = new Button(content, "Sprite/atkButton", new Vector2(100,150));

            //foreach (Hero hero in heroes)
            //{
                hero.LoadContent();
                support1.LoadContent();
                support2.LoadContent();
                enemy1.LoadContent();
            //}
        }

        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;
            prev_mpressed = mpressed;
            mpressed = mouseState.LeftButton == ButtonState.Pressed;

            keyState = Keyboard.GetState();
            prev_kpressed = kpressed;
            kpressed = keyState.IsKeyDown(Keys.Right);

            hero.Update(gameTime);
            support1.Update(gameTime);
            support2.Update(gameTime);
            enemy1.Update(gameTime);

            oldMouseState = mouseState;
            oldKeyState = keyState;
            //atkButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed);

            //if (atkButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed))
            //{
            //    damageNumber += 1;
            //}

            if (hero.isWinner && !isFirstWinner)
            {
                winner = "Ramona";
                isFirstWinner = true;
            }

            if (support1.isWinner && !isFirstWinner)
            {
                winner = "Scott";
                isFirstWinner = true;
            }


            if (hero.textureRec.Intersects(enemy1.textureRec))
            {
                HP -= 1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            support2.Draw(gameTime, spriteBatch);
            hero.Draw(gameTime, spriteBatch);
            support1.Draw(gameTime, spriteBatch);
            enemy1.Draw(gameTime, spriteBatch);

            

            //spriteBatch.DrawString(damageStringfont, damageNumber + " Damage!", Vector2.Zero, Color.White);
            spriteBatch.DrawString(instructionsStringfont, "       Alternate mouse buttons to control Ramona and the left & right keypad to control Scott!", Vector2.Zero, Color.White);
            if (isFirstWinner)
            {
                spriteBatch.DrawString(winnerStringfont, winner + " wins!", new Vector2(340, 250), Color.White);
            }

            if (hero.characterChooser == 1)
            {
                spriteBatch.DrawString(ramonaLife, "HP: " + HP, new Vector2(hero.playerPosition.X + 10, hero.playerPosition.Y - 5), Color.White);
            }

            if (support1.characterChooser == 2)
            {
                spriteBatch.DrawString(scottLife, "HP: " + HP, new Vector2(support1.playerPosition.X + 10, support1.playerPosition.Y - 5), Color.White);
            }

            
            
            //atkButton.Draw(gameTime, spriteBatch);
            

        }
    }
}

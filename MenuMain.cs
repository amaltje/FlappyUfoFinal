using FlappyBird.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace FlappyBird.Menu
{
    class MenuMain : MenuBase
    {
        // fields aangemaakt
        Vector2 logoPosition;
        Vector2 logoSize;
        Rectangle logoSource;
        double counter = 0;
        Button startButton;
        Button quitButton;

        Rectangle highScoreRectangle;
        Rectangle highScoreSource;
        Score score;

        // constructor aangeroepen
        // foto's worden afgebeeld
        // 558 as the X coordinate of the rectangle's upper-left corner, 157 as the Y coordinate of the rectangle's upper-left corner. 96 as the width of the rectangle. 22 as the height of the rectangle.

        // The X coordinate is set to (Game1.screenWidth / 2 - logoSource.Width * 3 / 2) which places the logo horizontally centered on the screen
        // The Y coordinate is set to (Game1.screenHeight / 2 - logoSource.Height * 3 / 2 - 128) which places the logo vertically centered on the screen and shifted up by 128 pixels

        // The X-coordinate is set to highScoreRectangle.X + highScoreRectangle.Width + 48, which places the score to the right of the highScoreRectangle
        // Y-coordinate is set to highScoreRectangle.Y, which aligns the score with the top of the highScoreRectangle
        // boolean is false = score not centered 


        public MenuMain()
        {
            logoSource = new Rectangle(558, 157, 96, 22);
            logoPosition = new Vector2(Game1.screenWidth / 2 - logoSource.Width * 3 / 2, Game1.screenHeight / 2 - logoSource.Height * 3 / 2 - 128);
            logoSize = new Vector2(logoSource.Width * 3, logoSource.Height * 3);

            highScoreSource = new Rectangle(558, 282, 69, 7);
            highScoreRectangle = new Rectangle(8, 8, highScoreSource.Width * 3, highScoreSource.Height * 3);
            score = new Score(highScoreRectangle.X + highScoreRectangle.Width + 48, highScoreRectangle.Y, false);

            startButton = new Button(sprite, new Point(Game1.screenWidth / 2, Game1.screenHeight / 2), new Rectangle(558, 198, 40, 14));
            quitButton = new Button(sprite, new Point(Game1.screenWidth / 2, startButton.ButtonY + startButton.ButtonHeight * 2), new Rectangle(558, 268, 40, 14));
        }

        // Update en tekenen
        // startbutton gametime = check of button is ingeklikt
        // start button clicked = gamemain naar game
        // update quit button
        // quit button clicked = gamemain true = game exiteed 

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            startButton.Update(gameTime);
            if (startButton.Clicked)
                GameMain.ChangeMenu = "game";
            quitButton.Update(gameTime);
            if (quitButton.Clicked)
                GameMain.Quit = true;
        }

        // drawing menu screen
        // sritebatch draws the sprite stored in the sprite variable
        // at a position specified by the logoPosition vector and with a size specified by the logoSize vector
        //logoSource rectangle is used to specify which part of the sprite should be drawn
        //color.White argument specifies the color tint to apply to the sprite
        // 0.1f argument specifies the depth layer at which to draw the sprite

        // line 4 calls the Draw method of a score object, passing in a string representation of the highScore variable as an argument --> draws the current high score on the menu screen
        // button = start button en quit button

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(sprite, new Rectangle((int)logoPosition.X, (int)logoPosition.Y, (int)logoSize.X, (int)logoSize.Y), logoSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(sprite, highScoreRectangle, highScoreSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            score.Draw(spriteBatch, highScore.ToString());
            startButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
        }
    }
}

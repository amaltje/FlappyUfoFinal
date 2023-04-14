using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FlappyBird.GUI;
using Microsoft.Xna.Framework.Audio;
using static System.Formats.Asn1.AsnWriter;

namespace FlappyBird.Menu
{
    //al onze dingen worden aangeroepen
    class MenuGameOver
    {
        // FIELDS
        Texture2D texture;
        bool loaded;

        Rectangle title;
        Rectangle titleSource;

        Rectangle box;
        Rectangle boxSource;

        Rectangle newScore;
        Rectangle newScoreSource;

        Button retryButton;
        Button menuButton;

        int score;
        int highScore;

        int medalLevel;
        Medal medal;
        SoundEffect highSound;

        Score writeScore;
        Score writeBest;

        bool soundPlayed;

        // constructor class MenuGameOver --> displaying game over menu
        // "texture" variable is set to a texture stored in a "RessourcesManager" object, which is a class responsible for managing game assets
        // loaded false = menu not fully loaded
        // "titleSource" and "title" variables --> positioned at the top center of the screen + size is  three times larger than the original source rectangle
        // the "boxSource" and "box" variables --> specify source rectangle and position rectangle and contains the retry and menu buttons. The box is positioned at the bottom center of the screen, with a size that is three times larger than the original source rectangle.
        // 2 "Button" objects are created using the "Button" constructor. Each button is initialized with a texture, position, and source rectangle. One button represents the retry option and the other represents the menu option.

        public MenuGameOver()
        {
            texture = RessourcesManager.sprite;
            loaded = false;

            titleSource = new Rectangle(558, 179, 94, 19);
            title = new Rectangle(Game1.screenWidth / 2 - titleSource.Width * 3 / 2, 0 - titleSource.Height * 3, titleSource.Width * 3, titleSource.Height * 3);

            boxSource = new Rectangle(558, 0, 113, 58);
            box = new Rectangle(Game1.screenWidth / 2 - boxSource.Width * 3 / 2, Game1.screenHeight, boxSource.Width * 3, boxSource.Height * 3);

            retryButton = new Button(texture, new Point(Game1.screenWidth / 2, Game1.screenHeight / 2 + box.Height / 2 + 32), new Rectangle(558, 226, 40, 14));
            menuButton = new Button(texture, new Point(Game1.screenWidth / 2, retryButton.ButtonY + 80), new Rectangle(558, 212, 40, 14));

            highScore = MenuBase.HighScore;
            score = MenuBase.TotalScore;

            //medal level + the sound effect for a player's score
            // code begins by checking whether player has new high score. Yes = medal 0 is used (gold medal)
            // score between 10 and 14 = medal 1 = silver
            // score between 5 and 9 = medal 2 = bronze
            // not new high score = medal 3 = no medal

            if (score > 14)
            {
                medalLevel = 0;
                highSound = RessourcesManager.gold;
            }
            else if (score > 9)
            {
                medalLevel = 1;
                highSound = RessourcesManager.gold;
            }
            else if (score > 4)
            {
                medalLevel = 2;
                highSound = RessourcesManager.gold;
            }
            else
            {
                medalLevel = 3;
                highSound = null;
            }

            // input medal by using  spritebatch
            //point = location
            // writescore = created with the x and y coordinates as arguments for the location where the player's score will be displayed
            // write best + score =  created with the x and y coordinates as arguments for the location where the player's best score will be displayed
            // newscoresource + rectangle = object is created with the place of the area where the "New!" text will be displayed
            // newscore + rectangle = object is created with the dimensions of the area where the "New!" text will be displayed
            // no sound

            medal = new Medal(medalLevel, new Point((Game1.screenWidth / 2 - boxSource.Width * 3 / 2) + 13 * 3, (Game1.screenHeight / 2 - boxSource.Height * 3 / 2) + 21 * 3));

            writeScore = new Score((Game1.screenWidth / 2 - boxSource.Width * 3 / 2) + 92 * 3, (Game1.screenHeight / 2 - boxSource.Height * 3 / 2) + 17 * 3, false);
            writeBest = new Score((Game1.screenWidth / 2 - boxSource.Width * 3 / 2) + 92 * 3, (Game1.screenHeight / 2 - boxSource.Height * 3 / 2) + 38 * 3, false);

            newScoreSource = new Rectangle(617, 58, 16, 7);
            newScore = new Rectangle((Game1.screenWidth / 2 - boxSource.Width * 3 / 2) + 60 * 3, (Game1.screenHeight / 2 - boxSource.Height * 3 / 2) + 8 * 3, newScoreSource.Width * 3, newScoreSource.Height * 3);
            soundPlayed = false;
        }

        // METHODS

        // UPDATE & DRAW
        // Update method of the MenuGameOver class, which updates the state of the game over menu
        // checks if menu is not loaded --> determined by loaded
        // not loaded = sound effect = dood
        // the title of the menu has moved down to a certain position ( y > 32 pixel)
        // audio is played
        // (if (title.Y < 64)) checks if the title sprite has not yet reached the target y-coordinate, which is 64. If it hasn't, the code increments the y-coordinate of the title sprite by 8
        // (box.Y -= 8;) moves the box sprite upwards by decreasing its y-coordinate by 8
        // (if (box.Y + box.Height / 2 <= Game1.screenHeight / 2)) checks if the box sprite has reached the center of the screen
        // yes = code sets the y-coordinate of the box sprite to the center of the screen
        // (Game1.screenHeight / 2 - box.Height / 2), sets the loaded flag to true, and sets the soundPlayed flag to false
        public void Update(GameTime gameTime)
        {
            if (!loaded)
            {
                if (!soundPlayed && title.Y > 32)
                {
                    soundPlayed = true;
                    RessourcesManager.over.Play();
                }
                if (title.Y < 64)
                    title.Y += 8;
                box.Y -= 8;

                if (box.Y + box.Height / 2 <= Game1.screenHeight / 2)
                {
                    box.Y = Game1.screenHeight / 2 - box.Height / 2;
                    loaded = true;
                    soundPlayed = false;
                }
            }
            else
            {
                retryButton.Update(gameTime);
                if (retryButton.Clicked)
                    GameMain.ChangeMenu = "game";
                menuButton.Update(gameTime);
                if (menuButton.Clicked)
                    GameMain.ChangeMenu = "main";
                medal.Update(gameTime);
                if (!soundPlayed && highSound != null)
                {
                    highSound.Play();
                    soundPlayed = true;
                }
            }
        }

        // SpriteBatch.Draw method to draw the title and box textures, using their respective positions and source rectangles
        // The SpriteEffects and layer depth values are also specified
        //  if the box has moved up enough to reveal the buttons, it calls the Draw method of the retryButton and menuButton objects = visible
        //  if the menu has finished loading (i.e., loaded is true), it calls the Draw method of the medal, writeScore, and writeBest objects to display the user's score, high score, and medal
        //  player achieved a new high score, it also draws the "new score" graphic using the newScore and newScoreSource values

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, title, titleSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(texture, box, boxSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            if (box.Y < retryButton.ButtonY - retryButton.ButtonHeight / 2)
            {
                retryButton.Draw(spriteBatch);
                menuButton.Draw(spriteBatch);
            }
            if (loaded)
            {
                medal.Draw(spriteBatch);
                writeScore.Draw(spriteBatch, score.ToString());
                writeBest.Draw(spriteBatch, highScore.ToString());
                if (MenuBase.NewScore)
                    spriteBatch.Draw(texture, newScore, newScoreSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            }
        }
    }
}
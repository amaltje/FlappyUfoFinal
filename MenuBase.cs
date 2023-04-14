using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Menu
{
    //base class 
    public abstract class MenuBase
    {
        // onze sprite wordt opgeroepen en de foto's daarin
        protected Texture2D sprite;

        Texture2D Bird;
        Rectangle black;
        float opacity;

        Rectangle background;
        Rectangle backgroundSource;

        //Rectangle = foto
        //The type or member can be accessed only by code in the same class, dus alleen in MenuBase

        protected Rectangle ground;
        Rectangle groundSource;

        protected bool paused;
        protected bool pauseBackground;

        protected static int totalScore;
        protected static bool newScore;
        protected static int highScore;
        string[] txtFile;

        //er worden variabelen toegevoegd! scores
        //txt.file is content die geshowed wordt in output :)

        // Constructor
        public MenuBase()
        {
            totalScore = 0;

            sprite = RessourcesManager.sprite;
            Bird = RessourcesManager.Bird;
            black = new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight);
            opacity = 1f;

            // oké dus, onze sprite wordt opgeroepen uit rm.cs! 
            //1f = floating point value met de opacity = niks doorzichtig

            backgroundSource = new Rectangle(0, 0, 552, 256);
            background = new Rectangle(0, 0, backgroundSource.Width * 2, backgroundSource.Height * 2);
            groundSource = new Rectangle(0, 256, 553, 56);
            ground = new Rectangle(0, Game1.screenHeight - groundSource.Height * 2, groundSource.Width * 2, groundSource.Height * 2);
            paused = false;
            pauseBackground = false;

            //de foto's in onze sprite worden nu ingevoegd en gekeken
            // x 2 om de ideale lengte te hebben
            // niet gepauzeerd en achtergrond ook niet --> daarom false

            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Flappy.txt"))
            {
                txtFile = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Flappy.txt");
                if (txtFile[1].Contains("high score="))
                {
                    if (int.TryParse(txtFile[1].Substring(11, 3), out highScore)) { }
                }
            }
        }
        // code checks if a file named "Flappy.txt" exists in the "MyDocuments" folder. Exists = reads contents of the file, extracts the value of the high score if it exists, and assigns it to the variable "highScore".
        // uses the System.IO namespace to check if the file exists using the File.Exists method
        // Uses the Environment.GetFolderPath method to get the path of the "MyDocuments" folder --> paths with "Flappy.txt"
        // file exists = uses File.ReadAllLines
        // checks if the second line of the file contains the string "high score=". Yes = code uses the Substring method to extract the substring that starts at index 11 and has a length of 3 characters  and tries to parse it as an integer using the int.
        // TryParse method = parsing succeeds, the parsed value is assigned to the variable "highScore".
        // parsed value = read value to convert to another

        // methodes!
        // methode is privé en kan alleen be accessed from within the same class/struct
        // achtergrond is niet gepauzeerd en er wordt er ook niet naar gekeken, dus kan het bewegen!
        // achtergrond hebben we al ingesteld met onze sprite
        // background movement logic involves verlagen van de X coordinate van de achtergrond met 1 --> 'beweging'
        // methode checks of de achtergrond z'n x position over een limiet is, -276. Als dit kleiner of gelijk is, gaat het terug naar 0 --> loop
        private void BackgroundMovement()
        {
            if (!paused && !pauseBackground)
            {
                background.X--;
                if (background.X <= -276)
                    background.X = 0;
            }
        }

        //zelfde verhaal voor de grond
        // verlaagt  x-coordinate met 2
        private void GroundMovement()
        {
            if (!paused && !pauseBackground)
            {
                ground.X -= 3;
                if (ground.X <= -14)
                    ground.X = 0;
            }
        }

        public static int HighScore { get { return highScore; } }
        public static int TotalScore { get { return totalScore; } }
        public static bool NewScore { get { return newScore; } }

        // je krijgt score 

        // Update & Draw
        // 3e line checks if a variable called "opacity" is greater than 0. Yes= code subtracts 0.03 from the opacity value (fade out element in game)
        public virtual void Update(GameTime gameTime)
        {
            BackgroundMovement();
            GroundMovement();
            if (opacity > 0)
                opacity -= 0.03f;
        }

        //De foto's worden opgeroepen door spritebatch object
        // draws the background, ground, and a fading rectangle on the screen using the provided "SpriteBatch" object
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, background, backgroundSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.Draw(sprite, ground, groundSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6f);
            spriteBatch.Draw(Bird, black, new Rectangle(0, 0, 1, 1), new Color(Color.Black, opacity), 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
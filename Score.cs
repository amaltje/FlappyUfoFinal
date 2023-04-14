using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.GUI
{
    class Score
    {
        // FIELDS
        Texture2D texture;
        Rectangle score;
        Rectangle scoreSource;
        Point position;
        bool big;
        int characterPosition;

        // Constructor
        // texture is set to the sprite texture loaded from RessourcesManager
        // position is set to a new Point object with the x-coordinate _x and the y-coordinate _y
        // big is set to the boolean value _big, which determines whether the score should be displayed in a larger font
        // characterPosition is set to 0, which is the index of the first character in the score string
        public Score(int _x, int _y, bool _big)
        {
            texture = RessourcesManager.sprite;
            position = new Point(_x, _y);
            big = _big;
            characterPosition = 0;
        }

        // methode 
        // responsible for drawing a single digit in the score display
        // It takes in a SpriteBatch object to draw the digit, an integer representing the position of the character a character representing the digit to be drawn, and a string representing the entire score
        // method first checks whether the score display should use the large or small font, based on the big boolean field.
        // --> sets the scoreSource rectangle to the correct position in the texture based on the character to be drawn
        // after setting the scoreSource rectangle, the method uses the spriteBatch object to draw the digit at the correct position on the screen!
        private void DrawNumber(SpriteBatch spriteBatch, int characterPosition, char number, string scoreString)
        {
            if (big)
            {
                scoreSource = new Rectangle(617, 65, 8, 10);
                switch (number)
                {
                    case '0':
                        scoreSource.Location = new Point(617, 65);
                        break;
                    case '1':
                        scoreSource.Location = new Point(625, 65);
                        break;
                    case '2':
                        scoreSource.Location = new Point(633, 65);
                        break;
                    case '3':
                        scoreSource.Location = new Point(641, 65);
                        break;
                    case '4':
                        scoreSource.Location = new Point(617, 76);
                        break;
                    case '5':
                        scoreSource.Location = new Point(625, 76);
                        break;
                    case '6':
                        scoreSource.Location = new Point(633, 76);
                        break;
                    case '7':
                        scoreSource.Location = new Point(641, 76);
                        break;
                    case '8':
                        scoreSource.Location = new Point(617, 87);
                        break;
                    case '9':
                        scoreSource.Location = new Point(625, 87);
                        break;
                    default:
                        scoreSource.Location = new Point(617, 65);
                        break;
                }
            }
            // defines the behavior of drawing a character of the score in the case where big is false
            ////1e is big numbers en coordinaten
            // the scoreSource rectangle is set to a smaller size to accommodate the smaller character size
            // switch statement then sets the location of the scoreSource rectangle based on the character to be drawn, similar to the case where big is true
            else
            {
                scoreSource = new Rectangle(617, 98, 7, 7);
                switch (number)
                {
                    case '0':
                        scoreSource.Location = new Point(617, 98);
                        break;
                    case '1':
                        scoreSource.Location = new Point(624, 98);
                        break;
                    case '2':
                        scoreSource.Location = new Point(631, 98);
                        break;
                    case '3':
                        scoreSource.Location = new Point(638, 98);
                        break;
                    case '4':
                        scoreSource.Location = new Point(645, 98);
                        break;
                    case '5':
                        scoreSource.Location = new Point(617, 106);
                        break;
                    case '6':
                        scoreSource.Location = new Point(624, 106);
                        break;
                    case '7':
                        scoreSource.Location = new Point(631, 106);
                        break;
                    case '8':
                        scoreSource.Location = new Point(638, 106);
                        break;
                    case '9':
                        scoreSource.Location = new Point(645, 106);
                        break;
                    default:
                        scoreSource.Location = new Point(617, 98);
                        break;
                }
            }
            //2e is small numbers coordinaten
            // If the score is "big" it uses a larger font to draw the number, otherwise it uses a smaller font
            // position and size of the number to be drawn are defined by the scoreSource and score variables
            // switch statement sets the scoreSource based on the value of number
            // the score rectangle is then calculated based on the position, character position, and length of the scoreString, and the spriteBatch.Draw method is called to draw the digit at the appropriate location
            score = new Rectangle((position.X + characterPosition * (scoreSource.Width * 3)) - (scoreString.Length * scoreSource.Width * 3) / 2, position.Y, scoreSource.Width * 3, scoreSource.Height * 3);
            spriteBatch.Draw(texture, score, scoreSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        // tekenen
        // foreach (char c in score) loops through each character in the score string
        // DrawNumber(spriteBatch, characterPosition, c, score) is called with the current character c, characterPosition which represents the position of the current character to be drawn, and the score string
        // characterPosition is incremented to prepare for the next character to be drawn.
        // after everything is drawn, characterPosition is reset to zero to prepare for the next time Draw is called
        public void Draw(SpriteBatch spriteBatch, string score)
        {
            foreach (char c in score)
            {
                DrawNumber(spriteBatch, characterPosition, c, score);
                characterPosition++;
            }
            characterPosition = 0;
        }
    }
}
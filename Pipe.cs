using FlappyBird.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.GUI
{
    class Pipe
    {
        // fields
        Texture2D texture;
        Rectangle pipeUp;
        Rectangle pipeDown;
        Rectangle pipeSource;
        Rectangle pipeHeadSource;
        int holeSize;
        int holePosition;
        Random rand;

        // constructors
        // texture is set to a sprite that is loaded from a resource manager
        // pipeSource is set to a rectangle that defines the position and size of the pipe sprite within the texture
        // pipeHeadSource is set to a rectangle that defines the position and size of the pipe head sprite within the texture
        // rand = new instance of the Random class --> used later to generate a random position for the gap in the pipes
        // "holePosition" is set to a random integer between 0 and 200, which determines the vertical position of the gap in the pipes
        //"pipeDown" is set to a rectangle that defines the position and size of the lower pipe on the screen.
        // The X position is set to the screen width plus the start position passed to the constructor = pipe offscreen to the right when it is first created
        // The Y position is set to the hole position plus the hole size = gap 

        //pipeUp" is set to a rectangle that defines the position and size of the upper pipe on the screen
        //X position and width are the same as the lower pipe
        //Y position is set to the hole position minus the height of the pipe sprite, which positions the pipe above the gap

        public Pipe(int _startPosition)
        {
            holeSize = 78;
            texture = RessourcesManager.sprite;
            pipeSource = new Rectangle(654, 157, 24, 155);
            pipeHeadSource = new Rectangle(553, 300, 26, 12);
            rand = new Random();
            holePosition = rand.Next(0, 200);
            pipeDown = new Rectangle(Game1.screenWidth + _startPosition, holePosition + holeSize, pipeSource.Width * 2, pipeSource.Height * 2);
            pipeUp = new Rectangle(Game1.screenWidth + _startPosition, holePosition - pipeSource.Height * 2, pipeSource.Width * 2, pipeSource.Height * 2);
        }

        // Methode
        // moves the pair of pipes to the left by subtracting 2 from their X positions
        // right edge of the lower pipe goes off the left edge of the screen = pipes are reset to the right edge of the screen
        // new random value is generated for the hole position of the pipes --> determine where the gap between them will be located vertically
        // the Y positions of the pipes are set based on the new hole position and the size of the gap
        // the Y position of the lower pipe is set to the sum of the hole position and the hole size = creates a gap between the pipes
        // the Y position of the upper pipe is set to the difference between the hole position and twice the height of the pipe sprite, which positions the pipe above the gap
        private void MovePipe()
        {
            pipeDown.X -= 3;
            pipeUp.X -= 3;

            if (pipeDown.X <= -1 - pipeDown.Width)
            {
                pipeDown.X = Game1.screenWidth;
                pipeUp.X = Game1.screenWidth;
                rand = new Random();
                holePosition = rand.Next(0, 200);
                pipeDown.Y = holePosition + holeSize;
                pipeUp.Y = holePosition - pipeSource.Height * 2;
            }
        }

        //calculates and returns a rectangle that represents the "hole" in the pipes that the bird can fly through
        // X position of the rectangle is set to the X position of the lower pipe minus half the width of the bird sprite. This centers the rectangle horizontally around the lower pipe, so that the bird only needs to avoid colliding with the sides of the pipe
        // Y position of the rectangle is set to the sum of the hole position (the randomly generated vertical position of the gap between the pipes) and one and a half times the height of the bird sprite.
        // This positions the top of the rectangle at the bottom of the gap, leaving enough room for the bird to fly through
        // width of the rectangle is set to the sum of the width of the pipe sprite and the width of the bird sprite. This makes the rectangle wide enough to fully encompass the gap between the pipes and the bird sprite
        // height of the rectangle is set to the size of the hole, minus twice the height of the bird sprite. This ensures that the rectangle is tall enough to represent the entire gap, but doesn't include any parts of the pipes that the bird can't fly through

        public Rectangle Hole { get { return new Rectangle(pipeDown.X - Bird.Width / 2, holePosition + Bird.Height * 3 / 2, pipeDown.Width + Bird.Width, holeSize - Bird.Height * 2); } }

        // properties are getters that return the X and Y positions, width, and height of the pipe's upper and lower parts
        // allow access to these properties from outside of the Pipe class
        //  Pipe called pipe and you want to get the X position of the lower part of the pipe, ypu use pipe.XpositionDown)
        public int XpositionUp { get { return pipeUp.X; } }
        public int XpositionDown { get { return pipeDown.X; } }
        public int YpositionUp { get { return pipeUp.Y; } }
        public int YpositionDown { get { return pipeDown.Y; } }
        public int WidthUp { get { return pipeUp.Width; } }
        public int WidthDown { get { return pipeDown.Width; } }
        public int HeightUp { get { return pipeUp.Height; } }
        public int HeightDown { get { return pipeDown.Height; } }

        // update en tekenen 
        // used to update the positions and states of all game objects in the scene
        // the MovePipe() method updates the position of the pipes to create the illusion of the bird flying through a series of pipes
        public void Update(GameTime gameTime)
        {
            MovePipe();
        }

        //  takes a SpriteBatch object as input, which is used to draw the pipes and their heads
        //  --> calls the Draw() method on the SpriteBatch object four times: twice for each pipe, once for the pipe body and once for the pipe head
        // each pipe the method draws the pipe body using the texture, pipeDown or pipeUp, and the pipeSource rectangle
        // the rectangle specifies the portion of the texture to draw
        // the method draws the pipe head using the texture, pipeHeadSource rectangle and a new rectangle that specifies the position and size of the pipe head
        // The position of the pipe head is calculated based on the position of the pipe body and the size of the pipe head
        // the last parameter in the spriteBatch.Draw() method is the layer depth
        // The layer depth determines the order in which the objects are drawn
        // A smaller value means the object is drawn on top of other objects with higher layer depths
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pipeDown, pipeSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(texture, new Rectangle((pipeDown.X + pipeDown.Width / 2) - pipeHeadSource.Width, pipeDown.Y, pipeHeadSource.Width * 2, pipeHeadSource.Height * 2), pipeHeadSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);

            spriteBatch.Draw(texture, pipeUp, pipeSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(texture, new Rectangle((pipeUp.X + pipeUp.Width / 2) - pipeHeadSource.Width, pipeUp.Y + pipeUp.Height - pipeHeadSource.Height * 2, pipeHeadSource.Width * 2, pipeHeadSource.Height * 2), pipeHeadSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
        }
    }
}
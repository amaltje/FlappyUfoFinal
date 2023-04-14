using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.GUI
{
    class Bird

    {
        // fields 
        Texture2D texture;
        static Rectangle rectangle;
        Rectangle sourceRectangle;
        float rotation;
        float rotationSpeed;

        float fallSpeed;
        float fallTime;
        public int displacement;
        int frameCounter;

        KeyboardState presentKey;
        KeyboardState pastKey;

        // Constructor 
        // bird geladen (foto)

        // line 3 sets the rectangle variable to a new Rectangle object that represents the position and size of the sprite on the screen
        // X coordinate is set to half the width of the screen
        // Y coordinate is set to half the height of the screen minus two times the height of the source rectangle
        // width is set to two times the width of the source rectangle
        // the height is set to two times the height of the source rectangle

        // not rotated
        // rotationspeed = 0.3, which likely represents how quickly the sprite should rotate (kantelen)
        // hoe snel ufo valt
        // ufo nog niet gevallen
        // frame = 1
        // not displaced from original position
        public Bird()
        {
            texture = RessourcesManager.sprite;
            sourceRectangle = new Rectangle(558, 107, 17, 12);
            rectangle = new Rectangle(Game1.screenWidth / 2, Game1.screenHeight / 2 - sourceRectangle.Height * 2, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
            rotation = 0f;
            rotationSpeed = 0.3f;

            fallSpeed = 3.5f;
            fallTime = 0f;
            frameCounter = 1;
            displacement = 0;
        }

        // Methode 

        //property that gets and sets the Y position of ufo. It gets the value of the Y coordinate of the rectangle field, and sets it to the given value
        public int YPosition
        {
            get { return rectangle.Y; }
            set { rectangle.Y = value; }
        }

        //gets the value of the X coordinate of the rectangle field
        public int XPosition
        {
            get { return rectangle.X; }
        }

        //  It gets the value of the Width property of the rectangle field
        public static int Width
        {
            get { return rectangle.Width; }
        }

        // It gets the value of the Height property of the rectangle field
        public static int Height
        {
            get { return rectangle.Height; }
        }

        // herhaling: checks if the ufo's rectangle field intersects with another Rectangle. It returns a boolean value indicating whether or not the rectangles intersect
        public bool Intersect(Rectangle _rectangle)
        {
            return rectangle.Intersects(_rectangle);
        }

        // used to make the ufo up and down
        // It sets the fallTime field to -1.4f, which likely causes the bird to jump up slightly
        // If _playSound is true, it also plays a sound effect
        public void Flap(bool _playSound)
        {
            fallTime = -1.4f;
            if (_playSound)
                RessourcesManager.flap.Play();
        }

        //animate the ufo
        //It sets the frameCounter field based on the displacement field
        //If displacement is less than -1, it sets frameCounter to 2
        //If displacement is between -1 and 1, it sets frameCounter to 1
        //If displacement is greater than 1, it sets frameCounter to 0
       
        // Uupdate + draw
        // two parameters: a GameTime object and a boolean flag indicating whether the character is still alive or not
        // checks if the space key is pressed and if the character is alive. Yes = calls the Flap method with the argument true to make the character go up and down and play a sound effect
        // calculates the character's vertical displacement based on its fall speed and the current time, using the Math.Sinh method. The displacement is clamped to a maximum value of 12 pixels  --> prevent the character from falling too fast
        // method also calculates the character's rotation angle based on its fall time, using the Math.Sinh method --> rotation angle is also clamped to a maximum value of 0.9 radians to prevent the character from tilting too much
        // character's rectangle is updated to reflect its new position
        // animation method is called to determine which frame of the character's sprite sheet to use based on its displacement
        // displacement zorgt ervoor dat ufo beweegt
        //method sets the sourceRectangle field to the appropriate portion of the sprite sheet based on the frameCounter + updates the pastKey field to store the current keyboard state for the next frame

        public void Update(GameTime gameTime, bool _alive)
        {
            presentKey = Keyboard.GetState();
            if (presentKey.IsKeyDown(Keys.Space) && pastKey != presentKey && _alive)
                Flap(true);
            displacement = (int)(Math.Sinh(fallTime) * fallSpeed);
            if (displacement >= 12)
                displacement = 12;
            rectangle.Y += displacement;
            fallTime += 0.1f;
            pastKey = presentKey;
        }

        // parameters used are:
        // "texture": The texture that represents the sprite to be drawn
        // A Rectangle object that specifies where on the screen the sprite will be drawn and how large it will be.
        // rectangle object that specifies which portion of the texture will be used to draw the sprite.If null, the entire texture will be used.
        // The color tint to apply to the sprite. In this case, no tinting is applied.
        // The amount to rotate the sprite, in radians.
        // "new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2)": The origin point for the rotation and scaling of the sprite.In this case, it is set to the center of the source rectangle.
        // "SpriteEffects.None": Specifies whether to flip the sprite horizontally, vertically, or not at all.
        // "0.5f": The depth at which to draw the sprite, as a value between 0.0 and 1.0, where 0.0 is frontmost and 1.0 is rearmost

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White, rotation, new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2), SpriteEffects.None, 0.5f);
        }
    }
}
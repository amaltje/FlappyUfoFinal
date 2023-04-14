using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.GUI
{
    class Button
    {
        // FIELDS
        Texture2D texture;
        Point position;
        Rectangle button;
        Rectangle buttonSource;
        Color color;
        bool clicked;
        bool inClick;

        // CONSTRUCTOR
        public Button(Texture2D _texture, Point _position, Rectangle _buttonSource)
        {
            color = Color.White;
            texture = _texture;
            buttonSource = _buttonSource;
            position = _position;
            button = new Rectangle(position.X - buttonSource.Width * 3 / 2, position.Y - buttonSource.Height * 3 / 2, buttonSource.Width * 3, buttonSource.Height * 3);
            clicked = false;
            inClick = false;
        }

        // Methodes
        //  five properties: "Clicked", "ButtonWidth", "ButtonHeight", "ButtonX", and "ButtonY"
        // Clicked is a boolean property that gets or sets whether the button has been clicked
        // ButtonWidth" is an integer property that gets the width of the button
        // ButtonHeight" is an integer property that gets the height of the button
        // ButtonX" is an integer property that gets or sets the X-coordinate of the button's position on the screen. It also updates the position of the button's "position" field
        // ButtonY" is an integer property that gets or sets the Y-coordinate of the button's position on the screen. It also updates the position of the button's "position" field


        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }
        public int ButtonWidth
        {
            get { return button.Width; }
        }
        public int ButtonHeight
        {
            get { return button.Height; }
        }
        public int ButtonX
        {
            get { return button.X; }
            set { button.X = value; position.X = value; }
        }
        public int ButtonY
        {
            get { return button.Y; }
            set { button.Y = value; position.Y = value; }
        }

        // Update & tekenen
        // state of the button is checked based on the position of the mouse cursor
        //mouse cursor is within the boundaries of the button, the color of the button is changed to "Color.LightGray"
        // left mouse button is pressed while the mouse cursor is within the boundaries of the button -->  button is scaled up to three times its original size, the "inClick" boolean is set to true, and the button is not considered "clicked" yet
        // left mouse button is released while the mouse cursor is within the boundaries of the button and the "inClick" boolean is true, the button is considered "clicked", the "inClick" boolean is set to false, and a sound effect is played. Otherwisenot considered "clicked
        // size is reset to three times its original size when the mouse cursor is not within its boundaries
        // mouse cursor is not within the boundaries of the button, its color is set back to "Color.White"
        // position of the button is updated based on the position of the "position" field and the size of the button
        // button.Width / 2" divides the width of the button by 2 to adjust for its center position
        // position.X - button.Width / 2" subtracts half the width of the button from the X-coordinate of the other object's position to center the button horizontally

        public void Update(GameTime gameTime)
        {
            if (Mouse.GetState().X >= button.X && Mouse.GetState().X <= button.X + button.Width && Mouse.GetState().Y >= button.Y && Mouse.GetState().Y <= button.Y + button.Height)
            {
                color = Color.LightGray;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    button.Width = buttonSource.Width * 3 - 10;
                    button.Height = buttonSource.Height * 3 - 4;
                    inClick = true;
                }
                else
                {
                    if (inClick)
                    {
                        inClick = false;
                        clicked = true;
                        RessourcesManager.buttonClick.Play();
                    }
                    else if (inClick == false)
                        clicked = false;
                    button.Width = buttonSource.Width * 3;
                    button.Height = buttonSource.Height * 3;
                }
            }
            else if (color != Color.White)
                color = Color.White;

            button.X = position.X - button.Width / 2;
            button.Y = position.Y - button.Height / 2;
        }

        //button drawn from SpriteBatch
        // texture refers to the image file that contains the button's appearance
        // button refers to the rectangle object that defines the position and size of the button on the screen
        // buttonSource refers to the rectangle object that defines the portion of the image file to be used for the button's appearance
        // color refers to the color that the button should be tinted with. This can be used to create visual effects such as highlighting or fading
        // 0f" sets the rotation of the button to zero degrees
        // Vector2.Zero" sets the origin of the button to the top-left corner of the rectangle
        // 0.3f sets the depth layer of the button to 0.3, which determines the order in which sprites are drawn. Lower values are drawn first and higher values are drawn last
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, button, buttonSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0.3f);
        }
    }
}
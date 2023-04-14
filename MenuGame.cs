using FlappyBird.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace FlappyBird.Menu
{
    class MenuGame : MenuBase
    {
        // FIELDS
        MenuGameOver gameOver;
        bool over;

        bool started;
        bool died;
        Button menuButton;
        Button pauseButton;
        Button resumeButton;

        Bird player;
        Pipe[] pipes;
        Score actualScore;
        bool addScore;

        bool writed;

        Rectangle getReady;
        Rectangle getReadySource;
        Rectangle startTip;
        Rectangle startTipSource;
        Rectangle spaceTip;
        Rectangle spaceTipSource;

        string[] txtScore;

        KeyboardState presentKey;
        KeyboardState pastKey;

        // CONSTRUCTOR
        // started/died/over are not present
        // menuButton, pauseButton, and resumeButton are initialized as new instances of a Button class, using sprite (presumably a texture) and specific Rectangle objects for their image and position
        // position is calculated using the screen width and height  and some arithmetic to center the button on the top right corner of the screen
        //  13 * 3 / 2 expression evaluates to 19.5, so the button is positioned 19.5 pixels to the left of the right edge of the screen and 21 pixels down from the top edge (14 * 3 / 2 = 21).
        // actualScore object is created, which appears to be an instance of the Score class. It is centered horizontally and placed towards the top of the screen
        // totalScore variable is initialized to 0, and newScore and writed are initialized to false
        // 
        public MenuGame()
        {
            started = false;
            died = false;
            over = false;

            menuButton = new Button(sprite, new Point(60, 21), new Rectangle(558, 212, 40, 14));
            pauseButton = new Button(sprite, new Point(Game1.screenWidth - 13 * 3 / 2, 14 * 3 / 2), new Rectangle(558, 143, 13, 14));
            resumeButton = new Button(sprite, new Point(Game1.screenWidth - 13 * 3 / 2, 14 * 3 / 2), new Rectangle(571, 143, 13, 14));

            actualScore = new Score(Game1.screenWidth / 2, Game1.screenHeight / 2 - 192, true);
            totalScore = 0;
            newScore = false;
            writed = false;

            // getReadySource rectangle appears to be defining the area in the sprite sheet where the "Get Ready!" message is located
            // getReady rectangle is then used to position this message in the center of the screen
            // StartTipSource and spaceTipSource define the areas in the sprite sheet where the "Tap to jump" and "Press space to fly" messages are located, respectively. startTip and spaceTip then position these messages on the screen
            // txtScore = used to display the game's title

            getReadySource = new Rectangle(584, 135, 87, 22);
            getReady = new Rectangle(Game1.screenWidth / 2 - getReadySource.Width * 3 / 2, Game1.screenHeight / 2 - getReadySource.Height * 3 / 2 - 128, getReadySource.Width * 3, getReadySource.Height * 3);
            startTipSource = new Rectangle(558, 58, 19, 37);
            startTip = new Rectangle(Game1.screenWidth / 2 - startTipSource.Width, Game1.screenHeight / 2 - startTipSource.Height, startTipSource.Width * 2, startTipSource.Height * 2);
            spaceTipSource = new Rectangle(577, 58, 31, 15);
            spaceTip = new Rectangle(startTip.X + startTip.Width + 16, startTip.Y + 47, spaceTipSource.Width * 3, spaceTipSource.Height * 3);

            txtScore = new string[2];
            txtScore[0] = "|Flappy Bird Save|";
        }

        // methode

        // update en tekenen
        // reads current state of the keyboard input
        // if the "Escape" key is pressed and the game is started, toggles the paused boolean value = gepauzeerd

        //game begint:
        // updates the pause button and handles pause button click
        // updates the player's position and animation
        // updates the position and animation of each pipe
        // checks collision with each pipe and sets died boolean value to true if there is a collision +  plays a sound effect and sets the player's animation to a "hurt" state
        // checks if the player has passed through a pipe and updates the score accordingly. It also plays a sound effect
        // checks if the player has touched the ground or a pipe, and sets the died boolean value to true if it did. It also plays a sound effect and sets the player's animation to a "hurt" state
        // checks if the player has gone above the screen limit and if so, sets the game to the "died" state
        // dood = Updates the player's position and animation.
        // + if  score is higher than the high score, updates the high score and writes it to a file.
        // = displays the "game over" menu and handles menu button click
        // niet begonnen = Updates the menu button and handles menu button click
        // if the "Space" key is pressed, starts the game and initializes the player, pipes, and score
        // if the game is paused : updates the menu and resume buttons and handles button clicks

        public override void Update(GameTime gameTime)
        {
            presentKey = Keyboard.GetState();
            base.Update(gameTime);
            if (presentKey.IsKeyDown(Keys.Escape) && pastKey != presentKey && started)
                paused = !paused;
            if (!paused)
            {
                if (started)
                {
                    pauseButton.Update(gameTime);
                    if (pauseButton.Clicked)
                        paused = true;
                    player.Update(gameTime, true);
                    foreach (Pipe pipe in pipes)
                    {
                        pipe.Update(gameTime);
                    }
                    // for is a keyword that indicates the start of a loop
                    // int i = 0 initializes a variable i to 0 = used to keep track of the loop iteration count
                    // i < 3 is a condition that is checked before each iteration of the loop.If it evaluates to true, the loop continues and if false, the loop ends
                    // i++ is an increment statement that is executed after each iteration of the loop.It increments the value of i by 1

                    for (int i = 0; i < 3; i++)
                    {
                        //knalt tegen pipe aan
                        if ((!player.Intersect(pipes[i].Hole) && player.XPosition + Bird.Width / 2 > pipes[i].XpositionUp && player.XPosition - Bird.Width / 2 < pipes[i].XpositionUp + pipes[i].WidthUp))
                        {
                            died = true;
                            RessourcesManager.hurtt.Play();
                            player.Flap(false);
                        }
                        //als hij erover heen gaat
                        else if (player.XPosition >= pipes[i].Hole.X + pipes[i].Hole.Width && player.XPosition <= pipes[i].Hole.X + pipes[i].Hole.Width + 1 && addScore)
                        {
                            totalScore++;
                            addScore = false;
                            RessourcesManager.pipePassedd.Play();
                        }
                        else
                            addScore = true;
                    }
                    if (player.YPosition + Bird.Height / 2 >= Game1.screenHeight - ground.Height || died)
                    {
                        died = true;
                        started = false;
                        pauseBackground = true;
                        RessourcesManager.hurtt.Play();
                        player.Flap(false);
                    }
                    else if (player.YPosition - Bird.Height / 2 < 0)
                    {
                        player.YPosition = 0 + Bird.Height / 2;
                    }
                }
                else if (died)
                {
                    // dood
                    if (player.YPosition - Bird.Height / 2 > Game1.screenHeight + Bird.Height)
                    {
                        if (!over)
                        {
                            gameOver = new MenuGameOver();
                            over = true;

                        }
                        gameOver.Update(gameTime);
                    }
                    else
                    {
                        player.Update(gameTime, false);
                        if (!writed)
                        {
                            if (totalScore > highScore)
                            {
                                if (totalScore < 10)
                                {
                                    txtScore[1] = "high score=" + "00" + totalScore;
                                }
                                else if (totalScore < 100)
                                {
                                    txtScore[1] = "high score=" + "0" + totalScore;
                                }
                                else
                                {
                                    txtScore[1] = "high score=" + totalScore;
                                }
                                System.IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Flappy.txt", txtScore);
                                highScore = totalScore;
                                newScore = true;
                            }
                            writed = true;
                        }
                    }
                }
                else
                {
                    menuButton.Update(gameTime);
                    if (menuButton.Clicked)
                    {
                        GameMain.ChangeMenu = "main";
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        started = true;
                    player = new Bird();
                    addScore = true;
                    pipes = new Pipe[3];
                    for (int i = 0; i < 3; i++)
                    {
                        pipes[i] = new Pipe(i * 267);
                    }
                }
            }
            else
            {
                //paused button is pressed
                // updates the menu button and the resume button
                // menu button is clicked = sets the ChangeMenu property of the GameMain class to "main", which will cause the game to switch to the main menu
                // the resume button is clicked = sets the paused variable to false, which will resume the game.
                // the pastKey variable is updated to store the current state of the keyboard
                menuButton.Update(gameTime);
                if (menuButton.Clicked)
                {
                    GameMain.ChangeMenu = "main";
                }
                resumeButton.Update(gameTime);
                if (resumeButton.Clicked)
                {
                    paused = false;
                }
            }
            pastKey = presentKey;
        }

        // method first calls the base class Draw method, then checks if the game has started or if the player has died, and draws different things based on those conditions
        // game has started and the player hasn't died, it checks if the game is paused, and draws either the pause button or a black semi-transparent overlay with menu and resume buttons on top of the game
        //  then draws the player, the pipes, and the score
        //  player dead = draws the player, the pipes, and a "game over" message if the game is over
        // game hasn't started yet = draws the menu button, "get ready" message, and instructions for the player to press the spacebar to start

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (started)
            {
                if (!paused)
                {
                    pauseButton.Draw(spriteBatch);
                }
                else
                {
                    spriteBatch.Draw(RessourcesManager.Bird, new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight), new Rectangle(0, 0, 1, 1), new Color(Color.Black, 0.7f), 0f, Vector2.Zero, SpriteEffects.None, 0.4f);
                    menuButton.Draw(spriteBatch);
                    resumeButton.Draw(spriteBatch);
                    spriteBatch.Draw(sprite, new Rectangle(Game1.screenWidth / 2 - 162 / 2, Game1.screenHeight / 2 - 60 / 2, 162, 60), new Rectangle(598, 198, 54, 20), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
                player.Draw(spriteBatch);
                foreach (Pipe pipe in pipes)
                {
                    pipe.Draw(spriteBatch);
                }
                actualScore.Draw(spriteBatch, totalScore.ToString());
            }
            else if (died)
            {
                player.Draw(spriteBatch);
                foreach (Pipe pipe in pipes)
                {
                    pipe.Draw(spriteBatch);
                }
                if (over)
                {
                    gameOver.Draw(spriteBatch);
                }
            }
            else
            // starts by calling the base class's Draw method and then checks if the game has started or not
            // If the game has started, it checks if it is paused or not
            // If it is not paused, it draws the pause button
            // If it is paused, it draws a black semi-transparent rectangle over the screen, and draws the menu button, resume button, and a "paused" text
            //draws the player and the pipes, and the current score
            //player has died, it draws the player and the pipes again, and if the game is over, it draws the "game over" text. If the game has not started yet, it draws the menu button and some tips on how to start the game
            {
                menuButton.Draw(spriteBatch);
                spriteBatch.Draw(sprite, startTip, startTipSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(sprite, spaceTip, spaceTipSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(sprite, getReady, getReadySource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            }
        }
    }
}
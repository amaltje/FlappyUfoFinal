using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{

    // Dit is onze main document voor onze game!
    // linker scherm is 0,0
 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics; //attached to hardware
        SpriteBatch spriteBatch;
        public static int screenWidth;
        public static int screenHeight;
        // SpriteBatch = meerdere sprites met zelfde texture
        // public static int = variabele maken voor scherm lenght en width

        GameMain main;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 768;
            graphics.PreferredBackBufferHeight = 512;

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            // is letterlijk length en width van onze game en dat gelijk stellen aan onze variabele!
        }

  
        // perform initialization needed before starting to run.
        // --> can query for any required services and load any non-graphic related content duh
        //  calling base.Initialize will enumerate through any components + initialize
        // dus: assigns a value before it is used in the program

       
        protected override void Initialize()
        {
            base.Initialize();
        }

        // LoadContent will be called once per game and is the place to load all contents
   
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            RessourcesManager.LoadContent(Content);
            main = new GameMain();

            // Onze spritebatch wordt nu geladen!
        }

        // UnloadContent will be called once per game and is the place to unload game-specific content 
        // disposes all content!
        protected override void UnloadContent()
        {
        }

        // allows game to run logic such as updating it, audio etc. 
        // checking for collisions, gathering input, and playing audio.
        // gameTime houdt informatie vast 
        protected override void Update(GameTime gameTime)
        {
            main.Update(gameTime);

            if (GameMain.Quit)
                Exit();

            base.Update(gameTime);
        }

        // game should draw itself 
        // the base van ons spel! mooie achtergrond wel 
        // spriteBatch.Begin = beginning process of sprites to draw! 
        // BlendState = color in rendertarget (backbuffer = displayed on screen) en alphablending = (semi) transparant
        // SamplerState.PointClamp zorgt voor scaling texture ??? (null, null, null is coordinaten uit)
        //base.Draw = draw is amount passed tijd
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            main.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
using FlappyBird.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird
{
    class GameMain
    {
        // Dt zijn onze fields! We gaan hier dus mee werken in GameMain
        // static string is 1 locatie
        // static boolean zodat er geen herhaling is (zometeen false of true)
        //string = characters (menu aangegeven)
        MenuMain mainMenu;
        MenuGame gameMenu;

        string[] menuList = { "main", "game" };
        static string activeMenu;
        static bool reset;
        static bool quit;

        // de constructors --> initialize objecten
        // activeMenu is nu dus menuList[0]. De 0 zorgt ervoor dat we value nog kunnen toevoegen? 
        public GameMain()
        {
            activeMenu = menuList[0];
            reset = true;
            quit = false;
        }
        //gamemain kan je resetten, maar niet stoppen

        // methodes!
        // in de game zelf kan je terug gaan naar menu 
        // je kan dus resetten en menu wordt actief
        public static string ChangeMenu { set { activeMenu = value; reset = true; } }
        public static bool Quit
        {
            get { return quit; }
            set { quit = value; }
        }

        // updates en tekenen
        //gametime wordt geupdate
        // mainMenu staat nu gelijk aan een new MenuMain (andere scherm)
        // als je gaat resetten, kan je niet meer resetten
        // als je in mainMenu zit, dan verandert gameTime en anders veranderd gameTime in gameMenu (menu scherm en spel zelf)
        public void Update(GameTime gameTime)
        {
            if (reset)
            {
                mainMenu = new MenuMain();
                gameMenu = new MenuGame();
                reset = false;
            }
            if (activeMenu == "main")
                mainMenu.Update(gameTime);
            else if (activeMenu == "game")
                gameMenu.Update(gameTime);
        }

        // de menu en game menu worden toegevoegd // shape
        public void Draw(SpriteBatch spriteBatch)
        {
            if (activeMenu == "main")
                mainMenu.Draw(spriteBatch);
            else if (activeMenu == "game")
                gameMenu.Draw(spriteBatch);
        }
    }
}
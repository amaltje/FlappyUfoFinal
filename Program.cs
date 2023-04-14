using FlappyBird;
using System;

namespace MG_FlappyBird
{
public static class Program
    {
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }

}
 // game is nu aan variabele! Onze main programma is dus game1. Het spel kan nu dus opstarten.
 // static void zorgt ervoor dat Main nu 'space' heeft en er een bestand in gezet kan worden
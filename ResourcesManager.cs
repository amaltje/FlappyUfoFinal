using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird
{
    class RessourcesManager
    {
        // Static fields! hier worden onze foto's en geluiden toegevoegd 
        public static Texture2D sprite;
        public static Texture2D Bird;

        public static SoundEffect buttonClick;
        public static SoundEffect flap;
        public static SoundEffect hurtt;
        public static SoundEffect pipePassedd;
        public static SoundEffect over;
        public static SoundEffect bronze;
        public static SoundEffect silver;
        public static SoundEffect gold;

        // hierin worden de elemente geladen en moeten we het een naam geven, zodat we ze makkelijk kunnen gebruiken!
        public static void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("sprite");
            Bird = content.Load<Texture2D>("Bird");
            buttonClick = content.Load<SoundEffect>("buttonClick");
            flap = content.Load<SoundEffect>("flap");
            hurtt = content.Load<SoundEffect>("hurtt");
            pipePassedd = content.Load<SoundEffect>("pipePassedd");
            over = content.Load<SoundEffect>("overHighNo");
            bronze = content.Load<SoundEffect>("overHighBronze");
            silver = content.Load<SoundEffect>("overHighSilver");
            gold = content.Load<SoundEffect>("overHighGold");
        }
    }
}
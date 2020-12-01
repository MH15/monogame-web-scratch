﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game_project.Fonts
{
    class Font
    {
        private static readonly Fonts.Font instance = new Fonts.Font();
        public static Fonts.Font Instance
        {
            get
            {
                return instance;
            }
        }

        static private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private Font() { }

        // Helps with mapping sound effects to their files. 
        // Makes it so you can search for effects without needing to memorize the names. You can intellisense the values of this

        public enum FontFiles
        {
            LoZ
        }

        public async Task LoadAllFonts(ContentManager content)
        {
            //foreach (string effect in Enum.GetNames(typeof(FontFiles)))
            //{
            //    fonts.Add(effect, content.Load<SpriteFont>(effect));
            //}
            var f = await content.LoadAsync<SpriteFont>("LoZ");
            fonts.Add("LoZ", f);

        }

        public static SpriteFont GetFont(FontFiles font)
        {
            return fonts[font.ToString()];
        }
    }
}

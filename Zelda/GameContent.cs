using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using game_project.Content.Sprites.SpriteFactories;
using game_project.Levels;

namespace game_project
{
    static class GameContent
    {
        public static bool IsBaseLoaded { get; set; }
        public static bool IsLoaded { get; set; }
        public static int Counter;
        public static int Max = 9;

        public static async Task Init(ContentManager content, GraphicsDevice graphics)
        {
            Counter = 0;
            //Texture.Pixel  = await content.LoadAsync<Texture2D>("Pixel");
            Font.hudFont = await content.LoadAsync<SpriteFont>("LoZ");
            IsBaseLoaded = true;


            await LinkSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await ItemSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await HUDSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await LevelMapSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await BossSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await NPCSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await EnemySpriteFactory.Instance.LoadAllTextures(content); Counter++;
            await LinkItemSpriteFactory.Instance.LoadAllTextures(content); Counter++;
            //await Sound.Instance.LoadAllSounds(content);

            LevelManager.Init();
            IsLoaded = true;
        }
        public static class Font
        {
            public static SpriteFont hudFont;
        }

    }
}




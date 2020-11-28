using game_project.Content.Sprites.SpriteFactories;
using game_project.Controllers;
using game_project.ECS;
using game_project.ECS.Systems;
using game_project.Fonts;
using game_project.Levels;
using game_project.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bridge.Utils;
using game_project.ECS.Components;
using System.Threading.Tasks;

namespace game_project
{

    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static Viewport viewport;
        SpriteBatch spriteBatch;

        KeyboardController keyboard;

        // Required to exit from other folders
        public static Game1 self;

        private Entity link;

        public bool IsLoaded = false;
        public int LoadCounter = 0;


        public Game1()
        {
            Console.Log("constructor");
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Constants.SCREEN_PREFERRED_WIDTH_PX,
                PreferredBackBufferHeight = Constants.SCREEN_PREFERRED_HEIGHT_PX
            };
            graphics.ApplyChanges();
            //Window.Position = Constants.SCREEN_WINDOW_ORIGIN;
            viewport = graphics.GraphicsDevice.Viewport;
            Content.RootDirectory = "Content";
            self = this;
        }

        protected override void Initialize()
        {
            Console.Log("initialize");
            keyboard = new KeyboardController();
            //string initialPath = Constants.STARTING_LEVEL;
            //LevelManager.Load(initialPath);

            //ColliderSystem.DrawDebug = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Console.Log("load");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadStuff();
            //link = new SpriteTest(new Vector2(200, 200));
            //link = new GameObjects.Link.Link(new Vector2(200, 200));
            //Scene.Add(link);
            string initialPath = Constants.STARTING_LEVEL;
            LevelManager.Load(initialPath);
        }

        private async void LoadStuff()
        {
            ////load all spritesheets

            //await ItemSpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 1");
            //await HUDSpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 2");
            //await LevelMapSpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 3");
            //await BossSpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 4");
            //await NPCSpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 5");
            //await EnemySpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 6");
            //await LinkItemSpriteFactory.Instance.LoadAllTextures(this.Content);
            ////Console.Log("load 7");
            //Sound.Instance.LoadAllSounds(this.Content);
            ////Console.Log("load 8");
            ////Font.Instance.LoadAllFonts(this.Content);
            ////Console.Log("done load");

            var taskList = new[]
            {
                LinkSpriteFactory.Instance.LoadAllTextures(this.Content),
                ItemSpriteFactory.Instance.LoadAllTextures(this.Content),
                HUDSpriteFactory.Instance.LoadAllTextures(this.Content),
                LevelMapSpriteFactory.Instance.LoadAllTextures(this.Content),
                BossSpriteFactory.Instance.LoadAllTextures(this.Content),
                NPCSpriteFactory.Instance.LoadAllTextures(this.Content),
                EnemySpriteFactory.Instance.LoadAllTextures(this.Content),
                LinkItemSpriteFactory.Instance.LoadAllTextures(this.Content),
            };

            Task t = Task.WhenAll(taskList);

            await t;



            if (t.Status == TaskStatus.RanToCompletion)
            {
                IsLoaded = true;
            }


        }

        protected override void UnloadContent()
        {
            Content.Unload();

            // We need to clear all our components to reset state; If the program is reset they will be created fresh by the new Link() instance
            TransformSystem.Clear();
            ColliderSystem.Clear();
            BehaviorScriptSystem.Clear();
            SpriteSystem.Clear();
            //TextSystem.Clear();

            Scene.Clear();
        }

        private int i = 0;

        protected override void Update(GameTime gameTime)
        {
            //Console.Log(IsLoaded);

            if (!IsLoaded)
            {
                Console.Log("loading");

            }

            i++;
            if (i % 100 == 0)
            {
                //Console.Log("frame");
            }
            // Update KeyboardController for Commands
            keyboard.Update();

            Input.GetState();

            // Game implemented using Entity-Component-System,
            // Systems store instances of certain types of Components
            TransformSystem.Update(gameTime);
            BehaviorScriptSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            ColliderSystem.Check();
            SpriteSystem.Update(gameTime);
            //TextSystem.Update(gameTime);
            ////Sound.Update(gameTime);

            base.Update(gameTime);
        }


        int frame = 0;
        int frameCounter = 0;
        int _lastTime = 0;

        protected override void Draw(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds > _lastTime)
            {
                _lastTime = gameTime.TotalGameTime.Seconds;
                frame = frameCounter;
                frameCounter = 0;
            }
            frameCounter++;
            //i++;
            //GraphicsDevice.Clear(Color.IndianRed);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp);
            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.Identity);
            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp);
            SpriteSystem.Draw(spriteBatch); // Draw all Sprite components
            ColliderSystem.Draw(spriteBatch); // Draw all Collider debug boxes
            //TextSystem.Draw(spriteBatch); // Draw all Text components

            //DrawShadowedString(hudFont, "FPS: " + frame, new Vector2(0.0f, 4f), Color.Yellow);

            spriteBatch.End();

            ///*
            //if (i % 30 == 0)
            //{
            //    //Debug.WriteLine(GameStateManager.State);
            //    //    float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            //    //    Debug.WriteLine("frame rate: " + frameRate);
            //}
            //*/

            base.Draw(gameTime);
        }

        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            spriteBatch.DrawString(font, value, position, color);
        }
    }



    public class SpriteTest : Entity
    {
        public SpriteTest(Vector2 pos)
        {
            name = "SpriteTest";
            var s = LinkSpriteFactory.Instance.CreateLinkUseItemDown();
            AddComponent(new Sprite(s));

            GetComponent<Transform>().position = pos;
        }
    }
}

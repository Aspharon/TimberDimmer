using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TimberDimmer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private InputHelper inputHelper;
        private GraphicsHelper graphicsHelper;
        public static ContentManager contentManager;
        public static Random rand;
        public static bool[][] treeGrid;
        public static bool[][] fireGrid;
        int gridWidth = 15, gridHeight = 10, gamestate = 0, treesSaved;

        public Game1()
        {
            contentManager = Content;
            contentManager.RootDirectory = "Content";
            graphicsHelper = new GraphicsHelper(this);
            inputHelper = new InputHelper();
            IsMouseVisible = true;
            rand = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            StartScreen startScreen = new StartScreen();
            Objects.List.Add(startScreen);

            base.Initialize();
        }

        void Start()
        {
            Background BG = new Background();
            Objects.List.Add(BG);

            treeGrid = new bool[gridWidth][];
            for (int z = 0; z < gridWidth; z++)
                treeGrid[z] = new bool[gridHeight];

            fireGrid = new bool[gridWidth][];
            for (int z = 0; z < gridWidth; z++)
                fireGrid[z] = new bool[gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    treeGrid[x][y] = true;
                    Tree tree = new Tree(realPosition(new Vector2(x, y)));
                    Objects.List.Add(tree);
                }
            }

            Fire fire = new Fire(new Vector2(gridWidth / 2 + rand.Next(-3, 3), gridHeight / 2 + rand.Next(-3, 3)));
            Objects.List.Add(fire);

            Player player = new Player();
            Objects.List.Add(player);
        }

        Vector2 realPosition(Vector2 pos)
        {
            pos.X = pos.X * 32;
            pos.Y = pos.Y * 32;
            return pos;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // TODO: Add your update logic here

            base.Update(gameTime);
            graphicsHelper.Update(gameTime);
            graphicsHelper.HandleInput(inputHelper);
            foreach (GameObject obj in Objects.List)
                obj.HandleInput(inputHelper);
            foreach (GameObject obj in Objects.List)
                obj.Update(gameTime);
            foreach (GameObject obj in Objects.AddList)
                Objects.List.Add(obj);
            Objects.AddList.Clear();
            foreach (GameObject obj in Objects.RemoveList)
                Objects.List.Remove(obj);
            Objects.RemoveList.Clear();

            switch (gamestate)
            {
                case 0:
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        gamestate++;
                        Objects.List.Clear();
                        Start();
                    }
                    break;

                case 1:
                    bool contained = true;
                    foreach (Fire f in Objects.List.OfType<Fire>())
                    {
                        if (f.neighbourList.Count != 0)
                        {
                            contained = false;
                        }
                    }
                    if (contained)
                    {
                        gamestate++;

                        int saved = 0;
                        foreach (Tree tree in Objects.List.OfType<Tree>()) saved++;
                        foreach (Fire fire in Objects.List.OfType<Fire>()) saved--;
                        Objects.List.Clear();

                        EndScreen endScreen = new EndScreen(saved);
                        Objects.List.Add(endScreen);
                    }
                    break;
                case 2:
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        gamestate--;
                        Objects.List.Clear();
                        Start();
                    }
                    break;
            }

            Objects.List.Sort((o1, o2) => o1.position.Y.CompareTo(o2.position.Y));


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
            graphicsHelper.Draw(gameTime);
        }
    }
}

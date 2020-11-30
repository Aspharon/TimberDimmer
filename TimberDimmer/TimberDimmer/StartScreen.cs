using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TimberDimmer
{
    class StartScreen : GameObject
    {
        Texture2D sprite;

        public StartScreen()
        {
            sprite = Game1.contentManager.Load<Texture2D>("start");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Vector2.Zero, Color.White);
        }
    }
}
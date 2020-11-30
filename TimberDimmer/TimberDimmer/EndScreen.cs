using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TimberDimmer
{
    class EndScreen : GameObject
    {
        Texture2D sprite;

        public EndScreen()
        {
            sprite = Game1.contentManager.Load<Texture2D>("stop");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Vector2.Zero, Color.White);
        }
    }
}
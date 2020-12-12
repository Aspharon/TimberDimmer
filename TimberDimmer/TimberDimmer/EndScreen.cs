using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TimberDimmer
{
    class EndScreen : GameObject
    {
        Texture2D sprite;
        SpriteFont font;
        int saved;

        public EndScreen(int trees)
        {
            sprite = Game1.contentManager.Load<Texture2D>("stop");
            font = Game1.contentManager.Load<SpriteFont>("font");
            saved = trees;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 measurement = font.MeasureString("saved");
            spriteBatch.Draw(sprite, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, saved.ToString(), new Vector2(240 - measurement.X / 4 - 5, 210 - measurement.Y / 2), Color.Gold);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TimberDimmer
{
    class Tree : GameObject
    {
        Texture2D sprite;

        public Tree(Vector2 pos)
        {
            position = pos;
            position.Y += 0.95f;
            sprite = Game1.contentManager.Load<Texture2D>("tree");
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void GetChopped()
        {
            Trunk trunk = new Trunk(position);
            Objects.AddList.Add(trunk);
            Game1.treeGrid[(int)position.X / 32][(int)position.Y / 32] = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}

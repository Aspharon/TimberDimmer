using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TimberDimmer
{
    class Fire : GameObject
    {
        Texture2D sprite;
        Rectangle rect;
        int animationCounter, spriteCounter, animationSpeed = 5, timer, spreadSpeed = 60;
        Vector2 gridPosition;
        public List<Vector2> neighbourList;
        SoundEffect spread;

        public Fire(Vector2 pos)
        {
            gridPosition = pos;
            position = pos * 32;
            position.Y += 1;
            Game1.fireGrid[(int)gridPosition.X][(int)gridPosition.Y] = true;
            sprite = Game1.contentManager.Load<Texture2D>("fire");
            spread = Game1.contentManager.Load<SoundEffect>("spread");
            rect = new Rectangle(0, 0, 16, 24);
            neighbourList = new List<Vector2>();
        }

        public override void Update(GameTime gameTime)
        {

            neighbourList.Clear();
            if (gridPosition.X > 0)
                neighbourList.Add(new Vector2(gridPosition.X - 1, gridPosition.Y));
            if (gridPosition.X < 14)
                neighbourList.Add(new Vector2(gridPosition.X + 1, gridPosition.Y));
            if (gridPosition.Y > 0)
                neighbourList.Add(new Vector2(gridPosition.X, gridPosition.Y - 1));
            if (gridPosition.Y < 9)
                neighbourList.Add(new Vector2(gridPosition.X, gridPosition.Y + 1));

            List<Vector2> removeList = new List<Vector2>();
            foreach (Vector2 pos in neighbourList)
            {
                if (Game1.treeGrid[(int)pos.X][(int)pos.Y] == false || Game1.fireGrid[(int)pos.X][(int)pos.Y] == true)
                {
                    removeList.Add(pos);
                }
            }
            foreach (Vector2 pos in removeList)
            {
                neighbourList.Remove(pos);
            }

            timer++;
            if (timer == spreadSpeed)
            {
                Spread();
                timer = 0;
            }


            Animate();
        }

        void Spread()
        {
            foreach (Vector2 pos in neighbourList)
            {
                if (Game1.rand.Next(10) == 1)
                {
                    Fire fire = new Fire(pos);
                    Objects.AddList.Add(fire);
                    spread.Play();
                }
            }
        }

        void Animate()
        {
            animationCounter++;
            if (animationCounter == animationSpeed)
            {
                animationCounter = 0;
                spriteCounter++;
                if (spriteCounter == 12)
                    spriteCounter = 0;
            }


            rect = new Rectangle(16 * spriteCounter, 0, 16, 24);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, rect, Color.White, 0.0f, Vector2.Zero, 2, SpriteEffects.None, 0.0f);
        }
    }
}

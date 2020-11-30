using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TimberDimmer
{
    class Player : GameObject
    {
        Texture2D sprite;
        Rectangle rect;
        SpriteEffects effects;
        int animationCounter, spriteCounter, animationSpeed = 5;
        bool swinging;

        public Player()
        {
            position = new Vector2(48, 48);
            sprite = Game1.contentManager.Load<Texture2D>("player");
            rect = new Rectangle(0, 0, 16, 32);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyDown(Keys.E))
            {
                swinging = true;
            }

            if (!swinging)
            {
                float moveSpeed = 1.7f;
                Vector2 moveVector = new Vector2(0, 0);
                if (inputHelper.KeyDown(Keys.W) && position.Y > 0)
                    moveVector += new Vector2(0, -moveSpeed);
                if (inputHelper.KeyDown(Keys.S) && position.Y < 320 - 32)
                    moveVector += new Vector2(0, moveSpeed);
                if (inputHelper.KeyDown(Keys.D) && position.X < 480 - 10)
                    moveVector += new Vector2(moveSpeed, 0);
                if (inputHelper.KeyDown(Keys.A) && position.X > 0)
                    moveVector += new Vector2(-moveSpeed, 0);
                position += moveVector;
                if (moveVector.X > 0)
                {
                    effects = SpriteEffects.None;
                }
                if (moveVector.X < 0)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (swinging)
            {
                if (animationCounter == 0 && spriteCounter == 0) //janky way to check if its the first swinging frame. Don't do this.
                {
                    Tree closest = null;
                    float smallestDist = float.MaxValue;
                    foreach (Tree h in Objects.List.OfType<Tree>())
                    {
                        float dist = Vector2.Distance(position, h.position);
                        if (dist < smallestDist)
                        {
                            smallestDist = dist;
                            closest = h;
                        }

                    }
                    if (closest != null && smallestDist < 32 && Game1.fireGrid[(int)closest.position.X / 32][(int)closest.position.Y / 32] == false)
                    {
                        closest.GetChopped();
                        Objects.RemoveList.Add(closest);
                    }
                }
                Animate();
            }
        }

        void Animate()
        {
            animationCounter++;
            if (animationCounter == animationSpeed)
            {
                animationCounter = 0;

                spriteCounter++;
                if (spriteCounter == 8)
                {
                    swinging = false;
                    spriteCounter = 0;
                }

                rect = new Rectangle(16 * spriteCounter, 0, 16, 32);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, rect, Color.White, 0.0f, Vector2.Zero, 1, effects, 0.0f);
        }
    }
}

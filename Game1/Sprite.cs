using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Game1
{
    abstract class Sprite
    {
        public Texture2D textureImage;
        public Vector2 position, speed;
        public Point frameSize, currentFrame;

        public Rectangle colisionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)
                    (int)frameSize.X, (int)frameSize.Y);
            }
        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed, Point frameSize, Point currentFrame)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.speed = speed;
            this.frameSize = frameSize;
            this.currentFrame = currentFrame;
        }

        public virtual void Update(GameTime gametime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position,
                new Rectangle((currentFrame.X * frameSize.X), (currentFrame.Y * frameSize.Y), frameSize.X, frameSize.Y), Color.White);
                

        }

    }
}

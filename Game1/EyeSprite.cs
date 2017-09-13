using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class EyeSprite : Sprite
    {
        public int damage = 1;

        public EyeSprite(Texture2D textureImage, Vector2 position, Vector2 speed, Point frameSize, Point currentFrame) 
            :base(textureImage, position, speed, frameSize, currentFrame)
        {

        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }


    }
}

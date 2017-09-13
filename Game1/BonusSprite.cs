using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class BonusSprite : Sprite
    {


        public BonusSprite(Texture2D textureImage, Vector2 position, Vector2 speed, Point frameSize, Point currentFrame) :
            base(textureImage, position, speed, frameSize, currentFrame)
        {

        }

        public override void Update(GameTime gametime)
        {
            position += speed;
            base.Update(gametime);
        }

    }
}

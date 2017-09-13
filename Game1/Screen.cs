using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public abstract class  Screen
    {

        public virtual void LoadContent(ContentManager Content)
        {
            
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime, ContentManager Content)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            
        }
    }
}

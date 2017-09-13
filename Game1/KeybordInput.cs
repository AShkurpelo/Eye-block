using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace Game1
{
    public class KeybordInput
    {
        private static KeybordInput instance;

        public static KeybordInput Instance
        {
            get
            {
                if (instance == null)
                    instance = new KeybordInput();
                return instance;
            }
        }
        

        public KeyboardState oldstate = new KeyboardState();
        public KeyboardState state = new KeyboardState();

        public bool work = false;

        //public enum state { up, down, left, rigth }
        //public state direction = new state(); 

        public void Update()
        {
            oldstate = state;
            state = Keyboard.GetState();
            bool a = Down, b = Up, c = Left, d = Rigth;
        }
        
        public bool Down
        {
            get
            {
                if ((oldstate.IsKeyUp(Keys.Down)) && state.IsKeyDown(Keys.Down) &&
                    !(state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Right)))
                {
                    work = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool Up
        {
            get
            {
                if ((oldstate.IsKeyUp(Keys.Up)) && state.IsKeyDown(Keys.Up) &&
                    !(state.IsKeyDown(Keys.Down) && state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Right)))
                {
                    work = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool Left
        {
            get
            {
                if ((oldstate.IsKeyUp(Keys.Left)) && state.IsKeyDown(Keys.Left) &&
                    !(state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Down) && state.IsKeyDown(Keys.Right)))
                {
                    work = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool Rigth
        {
            get
            {
                if ((oldstate.IsKeyUp(Keys.Right)) && state.IsKeyDown(Keys.Right) &&
                    !(state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Down)))
                {
                    work = true;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool Enter
        {
            get
            {
                if (state.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                    return true;
                else
                    return false;
            }
        }

        public bool Esc
        {
            get
            {
                if (state.IsKeyDown(Keys.Escape) && oldstate.IsKeyUp(Keys.Escape))
                    return true;
                else
                    return false;
            }
        }

    }
}

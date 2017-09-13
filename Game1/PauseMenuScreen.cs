using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class PauseMenuScreen : Screen
    {
        SpriteFont optionsFont;

        int r = 255, g = 255, b = 255;
        bool colorDown;
        //Color activeOptionColor = new Color(); //r,g,b,

        public enum State { cont, restart, exit };
        public State state = new State();

        public KeybordInput input;

        public void MenuScreen()
        {
            screenManagerState = ScreenManager.ScreenState.pauseMenu;
        }

        public override void LoadContent(ContentManager Content)
        {
            optionsFont = Content.Load<SpriteFont>(@"ScoreFont");

            screenManagerState = ScreenManager.ScreenState.pauseMenu;
        }

        void ColorUpdate()
        {
            if (r == 255)
                colorDown = true;
            if (r == 0)
                colorDown = false;
            if (colorDown)
                r -= 5;
            else
                r += 5;
            g = r;
            b = r;
        }

        void changeState(bool down)
        {
            if (down)
            {
                if (state == State.cont)
                    state = State.restart;
                else
                {
                    if (state == State.exit)
                        state = State.cont;
                    else
                        state = State.exit;
                }
                //gameName = "Work";
            }
            else
            {
                if (state == State.cont)
                    state = State.exit;
                else
                {
                    if (state == State.exit)
                        state = State.restart;
                    else
                        state = State.cont;
                }
            }
        }

        public ScreenManager.ScreenState screenManagerState
        {
            get;
             set;
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {
            ColorUpdate();
            screenManagerState = ScreenManager.ScreenState.pauseMenu;
            input = KeybordInput.Instance;

            if (input.Down)
                changeState(true);
            if (input.Up)
                changeState(false);

            if (input.Enter)
                switch (state)
                {
                    case State.exit:
                        screenManagerState = ScreenManager.ScreenState.exit;
                        break;
                    case State.cont:
                        screenManagerState = ScreenManager.ScreenState.game;
                        break;
                    case State.restart:
                        screenManagerState = ScreenManager.ScreenState.game;
                        break;
                }
            if (input.Esc)
                screenManagerState = ScreenManager.ScreenState.game;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case State.cont:
                    spriteBatch.DrawString(optionsFont, "CONTINUE", new Vector2(270, 100), new Color(r, g, b));
                    spriteBatch.DrawString(optionsFont, "RESTSART", new Vector2(270, 200), Color.Black);
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(350, 300), Color.Black);
                    break;
                case State.restart:
                    spriteBatch.DrawString(optionsFont, "CONTINUE", new Vector2(270, 100), Color.Black);
                    spriteBatch.DrawString(optionsFont, "RESTSART", new Vector2(270, 200), new Color(r, g, b));
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(350, 300), Color.Black);
                    break;
                case State.exit:
                    spriteBatch.DrawString(optionsFont, "CONTINUE", new Vector2(270, 100), Color.Black);
                    spriteBatch.DrawString(optionsFont, "RESTSART", new Vector2(270, 200), Color.Black);
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(350, 300), new Color(r, g, b));
                    break;
            }
        }

    }
}

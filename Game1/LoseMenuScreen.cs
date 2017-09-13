using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class LoseMenuScreen : Screen
    {
        SpriteFont optionsFont, resultFont;

        int r = 255, g = 255, b = 255;
        bool colorDown;

        public enum State { restart, mainMenu, exit };
        public State state = new State();

        public KeybordInput input;

        public int points;

        public override void LoadContent(ContentManager Content)
        {
            optionsFont = Content.Load<SpriteFont>(@"ScoreFont");
            resultFont = Content.Load<SpriteFont>(@"GameNameFont");

            screenManagerState = ScreenManager.ScreenState.loseMenu;
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
                switch (state)
                {
                    case State.restart:
                        state = State.mainMenu;
                        break;
                    case State.mainMenu:
                        state = State.exit;
                        break;
                    case State.exit:
                        state = State.restart;
                        break;
                }
                //gameName = "Work";
            }
            else
            {
                switch (state)
                {
                    case State.restart:
                        state = State.exit;
                        break;
                    case State.mainMenu:
                        state = State.restart;
                        break;
                    case State.exit:
                        state = State.mainMenu;
                        break;
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
            screenManagerState = ScreenManager.ScreenState.loseMenu;
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
                    case State.restart:
                        screenManagerState = ScreenManager.ScreenState.game;
                        break;
                    case State.mainMenu:
                        screenManagerState = ScreenManager.ScreenState.mainMenu;
                        break;
                }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(resultFont, "your score:" + (Math.Round((double)points/100)).ToString(), new Vector2(50, 20), Color.Black);

            switch (state)
            {
                case State.restart:
                    spriteBatch.DrawString(optionsFont, "RESTART", new Vector2(270, 200), new Color(r, g, b));
                    spriteBatch.DrawString(optionsFont, "MAIN MENU", new Vector2(250, 280), Color.Black);
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(340, 360), Color.Black);
                    break;
                case State.mainMenu:
                    spriteBatch.DrawString(optionsFont, "RESTART", new Vector2(270, 200), Color.Black);
                    spriteBatch.DrawString(optionsFont, "MAIN MENU", new Vector2(250, 280), new Color(r, g, b));
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(340, 360), Color.Black);
                    break;
                case State.exit:
                    spriteBatch.DrawString(optionsFont, "RESTART", new Vector2(270, 200), Color.Black);
                    spriteBatch.DrawString(optionsFont, "MAIN MENU", new Vector2(250, 280), Color.Black);
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(340, 360), new Color(r, g, b));
                    break;
            }
        }
    }
}

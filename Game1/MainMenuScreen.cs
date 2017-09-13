using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    class MainMenuScreen : Screen
    {
        SpriteFont gameNameFont, optionsFont;

        int r = 255, g = 255, b = 255;
        bool colorDown;
        //Color activeOptionColor = new Color(); //r,g,b,

        string gameName = "EYE-BLOCK";

        enum State { newGame, exit};
        State state = new State();
        bool stateBool = true;
        KeybordInput input = new KeybordInput();

        public void MenuScreen()
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            gameNameFont = Content.Load<SpriteFont>(@"GameNameFont");
            optionsFont = Content.Load<SpriteFont>(@"ScoreFont");

            screenManagerState = ScreenManager.ScreenState.mainMenu;
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
            //stateBool = !stateBool;
            //if (stateBool) state = State.newGame;
            //else state = State.exit;
            if (down)
            {
                if (state == State.newGame)
                    state = State.exit;
                else if (state == State.exit)
                    state = State.newGame;
                //gameName = "Work";
            }
            else
            {
                if (state == State.newGame)
                    state = State.exit;
                else if (state == State.exit)
                    state = State.newGame;
            }
        }

        public ScreenManager.ScreenState screenManagerState
        {
            get;
            private set;
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {
            ColorUpdate();
            screenManagerState = ScreenManager.ScreenState.mainMenu;
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
                    case State.newGame:
                        screenManagerState = ScreenManager.ScreenState.game;
                        break;
                }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameNameFont, gameName, new Vector2(165, 20), Color.Black);

            switch (state)
            {
                case State.newGame:
                    spriteBatch.DrawString(optionsFont, "NEW GAME", new Vector2(270, 200), new Color(r, g, b));
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(350, 300), Color.Black);
                    break;
                case State.exit:
                    spriteBatch.DrawString(optionsFont, "NEW GAME", new Vector2(270, 200), Color.Black);
                    spriteBatch.DrawString(optionsFont, "EXIT", new Vector2(350, 300), new Color(r, g, b));
                    break;
            }
        }
    }
}

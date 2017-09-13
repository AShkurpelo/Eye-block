using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class ScreenManager : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        GameScreen gameScreen = new GameScreen();
        PauseMenuScreen pauseMenuScreen = new PauseMenuScreen();
        MainMenuScreen mainMenuScreen = new MainMenuScreen();
        LoseMenuScreen loseMenuScreen = new LoseMenuScreen();

        public static ScreenState _screenState
        {
            get { return screenState; }
        }

        public enum ScreenState { game, mainMenu, pauseMenu, loseMenu, exit };
        static ScreenState screenState = ScreenState.mainMenu; //new ScreenState();

        //enum Action { mainMenuToGame, Exit}

        public ScreenManager(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            gameScreen.LoadContent(Game.Content);
            mainMenuScreen.LoadContent(Game.Content);
            pauseMenuScreen.LoadContent(Game.Content);
            loseMenuScreen.LoadContent(Game.Content);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeybordInput.Instance.Update();

            switch (screenState)
            {
                case ScreenState.game:
                    gameScreen.Update(gameTime, Game.Content);
                    screenState = gameScreen.screenManagerState;
                    break;
                case ScreenState.mainMenu:
                    mainMenuScreen.Update(gameTime, Game.Content);
                    screenState = mainMenuScreen.screenManagerState;
                    if (screenState == ScreenState.game)
                        gameScreen.NewGame(Game.Content, gameTime);
                    break;
                case ScreenState.pauseMenu:
                    pauseMenuScreen.Update(gameTime, Game.Content);
                    screenState = pauseMenuScreen.screenManagerState;
                    if (pauseMenuScreen.state == PauseMenuScreen.State.restart)
                        gameScreen.NewGame(Game.Content, gameTime);
                    break;
                case ScreenState.loseMenu:
                    loseMenuScreen.points = gameScreen.points;
                    loseMenuScreen.Update(gameTime, Game.Content);
                    screenState = loseMenuScreen.screenManagerState;
                    if (screenState == ScreenState.mainMenu || screenState == ScreenState.game)
                        loseMenuScreen.state = LoseMenuScreen.State.restart;
                    if (screenState == ScreenState.game)
                        gameScreen.NewGame(Game.Content, gameTime);
                    break;
                case ScreenState.exit:
                    Game.Exit();
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (screenState)
            {
                case ScreenState.game:
                    gameScreen.Draw(gameTime, spriteBatch);
                    break;
                case ScreenState.mainMenu:
                    mainMenuScreen.Draw(gameTime, spriteBatch);
                    break;
                case ScreenState.pauseMenu:
                    pauseMenuScreen.Draw(gameTime, spriteBatch);
                    break;
                case ScreenState.loseMenu:
                    loseMenuScreen.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}

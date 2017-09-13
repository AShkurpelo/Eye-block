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
    public class GameScreen : Screen
    {
        //SpriteBatch spriteBatch;
        KeybordInput input = new KeybordInput();
        EyeSprite eye;
        List<DustSprite> dust = new List<DustSprite>(), deletetedElements = new List<DustSprite>();
        List<BonusSprite> bonus = new List<BonusSprite>();
        Vector2 speed, position;
        const double speedUp = 1.0005; //1.003
        double startingSpeed = 2.5, maxSpeed = 10;
        double interval = 2000, minInterval = 700;
        double intervalUp { get { return Math.Pow(minInterval / interval, 1/(Math.Log(maxSpeed / startingSpeed) / Math.Log(speedUp))); } }
        int lastTime = 0, time = 0;
        int bonusLastTime = 0, bonusStartTime = 20000, bonusMinInterval = 5000;
        double oldSpeed, oldInterval;
        enum bonusStateEnum
        {
            health, time
        }
        bonusStateEnum bonusState;
        bool slow = false;
        int slowStarted, slowInterval = 4000, sInterval=1500, sSpeed=4;
        SpriteFont scoreFont;
        public int points = 0;

        public ScreenManager.ScreenState screenManagerState
        {
            get;
            private set;
        }


        public GameScreen()
        {

        }

        public void NewGame(ContentManager Content, GameTime gameTime)
        {
            eye.damage = 1;
            eye.textureImage = Content.Load<Texture2D>(@"eye_1");
            dust = new List<DustSprite>();
            deletetedElements = new List<DustSprite>();
            bonus = new List<BonusSprite>();
            startingSpeed = 2.5;
            interval = 2000;
            lastTime = 0;
            time = 0;
            bonusLastTime = 0;
            slow = false;
            points = 0;
            bonusStartTime = gameTime.TotalGameTime.Milliseconds + gameTime.TotalGameTime.Seconds * 1000
                + gameTime.TotalGameTime.Minutes * 60000 + bonusStartTime;
        }

        /*public override void Initialize()
        {
            base.Initialize();
        }*/

        public override void LoadContent(ContentManager Content) // (*new)
        {
            /*spriteBatch = new SpriteBatch(Game.GraphicsDevice);*/

            eye = new EyeSprite(Content.Load<Texture2D>(@"eye_1.png"),
                new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth/2 - 75,
                GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 75), 
                Vector2.Zero, new Point(150, 150), new Point(0, 0));

            scoreFont = Content.Load<SpriteFont>(@"ScoreFont");

            screenManagerState = ScreenManager.ScreenState.game;

            //base.LoadContent();
        }

        int Randomize
        {
            get
            {
                Random rnd = new Random();
                int direction = rnd.Next(1, 5);
                    

                //bool bonus = rnd.Next(1, 10) < 2 ? true : false;
                //if (bonus)
                //    direction += 4;
                return direction;
            }
        }

        int CreateChose
        {
            get
            {
                if (time < bonusStartTime)
                    return 1;
                else
                {
                    if (time - bonusLastTime > bonusMinInterval)
                    {
                        Random rnd = new Random();
                        bonusLastTime = time;
                        return rnd.Next(1, 4);
                    }
                    else
                        return 1;

                }
            }
        }

        bool collisionDetected(Sprite object1, Rectangle object2)
        {
            if (object1.colisionRectangle.Intersects(object2))
                return true;
            else
                return false;
        }

        void DefenceWork()
        {

            Sprite enemy = null;
            bool ifDust = false;
            int minDistance = GraphicsDeviceManager.DefaultBackBufferWidth+10;

            if (input.work)
            {
                foreach (DustSprite d in dust)
                {
                    if (input.Down)
                    {
                        if ((d.position.X == GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 35) &&
                            (d.position.Y > GraphicsDeviceManager.DefaultBackBufferHeight / 2) &&
                            (minDistance >= (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferHeight / 2))
                        {
                            minDistance = (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferHeight / 2;
                            enemy = d;
                            ifDust = true;
                        }
                    }

                    if (input.Up)
                    {
                        if ((d.position.X == GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 35) &&
                            (d.position.Y < GraphicsDeviceManager.DefaultBackBufferHeight / 2) &&
                            (minDistance >= GraphicsDeviceManager.DefaultBackBufferHeight / 2 - (int)d.position.Y))
                        {
                            minDistance = GraphicsDeviceManager.DefaultBackBufferHeight / 2 - (int)d.position.Y;
                            enemy = d;
                            ifDust = true;
                        }
                    }
                    if (input.Left)
                    {
                        if ((d.position.X < GraphicsDeviceManager.DefaultBackBufferWidth / 2) &&
                            (d.position.Y == GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 35) &&
                            (minDistance >= GraphicsDeviceManager.DefaultBackBufferWidth / 2 - (int)d.position.Y))
                        {
                            minDistance = GraphicsDeviceManager.DefaultBackBufferWidth / 2 - (int)d.position.Y;
                            enemy = d;
                            ifDust = true;
                        }
                    }
                    if (input.Rigth)
                    {
                        if ((d.position.X > GraphicsDeviceManager.DefaultBackBufferWidth / 2) &&
                            (d.position.Y == GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 35) &&
                            (minDistance >= (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferWidth / 2))
                        {
                            minDistance = (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferWidth / 2;
                            enemy = d;
                            ifDust = true;
                        }
                    }
                }

                foreach (BonusSprite d in bonus)
                {
                    if (input.Down)
                    {
                        if ((d.position.X == GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 35) &&
                            (d.position.Y > GraphicsDeviceManager.DefaultBackBufferHeight / 2) &&
                            (minDistance >= (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferHeight / 2))
                        {
                            minDistance = (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferHeight / 2;
                            enemy = d;
                            ifDust = false;
                        }
                    }

                    if (input.Up)
                    {
                        if ((d.position.X == GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 35) &&
                            (d.position.Y < GraphicsDeviceManager.DefaultBackBufferHeight / 2) &&
                            (minDistance >= GraphicsDeviceManager.DefaultBackBufferHeight / 2 - (int)d.position.Y))
                        {
                            minDistance = GraphicsDeviceManager.DefaultBackBufferHeight / 2 - (int)d.position.Y;
                            enemy = d;
                            ifDust = false;
                        }
                    }
                    if (input.Left)
                    {
                        if ((d.position.X < GraphicsDeviceManager.DefaultBackBufferWidth / 2) &&
                            (d.position.Y == GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 35) &&
                            (minDistance >= GraphicsDeviceManager.DefaultBackBufferWidth / 2 - (int)d.position.Y))
                        {
                            minDistance = GraphicsDeviceManager.DefaultBackBufferWidth / 2 - (int)d.position.Y;
                            enemy = d;
                            ifDust = false;
                        }
                    }
                    if (input.Rigth)
                    {
                        if ((d.position.X > GraphicsDeviceManager.DefaultBackBufferWidth / 2) &&
                            (d.position.Y == GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 35) &&
                            (minDistance >= (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferWidth / 2))
                        {
                            minDistance = (int)d.position.Y - GraphicsDeviceManager.DefaultBackBufferWidth / 2;
                            enemy = d;
                            ifDust = false;
                        }
                    }
                }
            }

            Random rnd = new Random();
            int k;

            if (enemy != null)
            {
                enemy.speed *= -1;
                k = (int) Math.Pow(-1, rnd.Next(1, 3));
                if (enemy.speed.X != 0)
                    enemy.speed.Y = k*enemy.speed.X;
                else
                    enemy.speed.X = k*enemy.speed.Y;

                if (ifDust)
                {
                    dust.Remove((DustSprite)enemy);
                    dust.Add((DustSprite)enemy);
                }
                else
                {
                    bonus.Remove((BonusSprite)enemy);
                    bonus.Add((BonusSprite)enemy);
                }
            }            
        }

        void UpdateCreate(ContentManager Content)
        {
            if (time - lastTime >= interval)
            {
                switch (Randomize)
                {
                    case 1: // upside
                        speed = new Vector2(0, (float)startingSpeed * GraphicsDeviceManager.DefaultBackBufferHeight /
                            GraphicsDeviceManager.DefaultBackBufferWidth / 3 * 2);
                        position = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 35, 0);
                        break;
                    case 2: // leftside
                        speed = new Vector2((float)startingSpeed, 0);
                        position = new Vector2(0, GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 35);
                        break;
                    case 3: // downside
                        speed = new Vector2(0, (float)-startingSpeed * GraphicsDeviceManager.DefaultBackBufferHeight /
                            GraphicsDeviceManager.DefaultBackBufferWidth / 3 * 2);
                        position = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 35,
                            GraphicsDeviceManager.DefaultBackBufferHeight - 35);
                        break;
                    case 4: //rigthside
                        speed = new Vector2((float)-startingSpeed, 0);
                        position = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth - 35,
                            GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 35);
                        break;
                }
                switch (CreateChose)
                {
                    case 1:
                        dust.Add(new DustSprite(Content.Load<Texture2D>(@"dust"), position, speed,
                            new Point(70, 70), new Point(0, 0)));
                        break;
                    case 2:
                        bonusState = bonusStateEnum.health;
                        bonus.Add(new BonusSprite(Content.Load<Texture2D>(@"health.png"), position, speed,
                            new Point(70, 70), new Point(0, 0)));
                        break;
                    case 3:
                        bonusState = bonusStateEnum.time;
                        bonus.Add(new BonusSprite(Content.Load<Texture2D>(@"time.png"), position, speed,
                            new Point(70, 70), new Point(0, 0)));
                        break;
                }
                //dust.Add(new DustSprite(Game.Content.Load<Texture2D>(@"dust"), position, speed, new Point(70, 70), new Point(0, 0)));
                lastTime = time;
            }
        }  

        public override void Update(GameTime gameTime, ContentManager Content) //+ContentManager
        {
            points++;
            input = KeybordInput.Instance;
            DefenceWork();
            screenManagerState = ScreenManager.ScreenState.game;

            if (input.Esc)
                screenManagerState = ScreenManager.ScreenState.pauseMenu;

            time = gameTime.TotalGameTime.Milliseconds + gameTime.TotalGameTime.Seconds * 1000
                + gameTime.TotalGameTime.Minutes * 60000;

            UpdateCreate(Content);


            foreach (DustSprite d in dust)
            {
                if (collisionDetected(d, eye.colisionRectangle))
                {
                    deletetedElements.Add(d);
                    if (eye.damage < 4)
                    {
                        eye.damage++;
                        eye.textureImage = Content.Load<Texture2D>(@"eye_" + eye.damage.ToString() + ".png");
                    }
                    else
                    {
                        screenManagerState = ScreenManager.ScreenState.loseMenu;
                    }
                }
                d.Update(gameTime);
            }

            if ((slow) && (time - slowStarted  > slowInterval))
            {
                slow = false;
                startingSpeed = oldSpeed;
                interval = oldInterval;
            }

            foreach (BonusSprite b in bonus)
            {
                if (collisionDetected(b, eye.colisionRectangle))
                {
                    if (bonusState == bonusStateEnum.health)
                    {
                        if (eye.damage > 2)
                        {
                            eye.damage = 2;
                            eye.textureImage = Content.Load<Texture2D>(@"eye_" + eye.damage.ToString() + ".png"); //Content
                            bonus.Remove(b);
                            break;
                        }
                        else
                        {
                            eye.damage = 1;
                            eye.textureImage = Content.Load<Texture2D>(@"eye_" + eye.damage.ToString() + ".png"); //Content
                            bonus.Remove(b);
                            break;
                        }            
                    }

                    else
                    {
                        slow = true;
                        slowStarted = time;
                        oldInterval = interval;
                        oldSpeed = startingSpeed;
                        startingSpeed = sSpeed;
                        interval = sInterval;
                        bonus.Remove(b);
                        break;
                    }

                }
                b.Update(gameTime);
            }

            foreach (DustSprite d in deletetedElements)
                dust.Remove(d);

            if (startingSpeed < maxSpeed && slow == false)
            {
                startingSpeed *= speedUp;
                interval *= intervalUp;
            }

            

            //base.Update(gameTime);       
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            eye.Draw(gameTime, spriteBatch);

            if (dust.Count != 0)
                foreach (DustSprite d in dust)
                    d.Draw(gameTime, spriteBatch);
            if (bonus.Count != 0)
                foreach (BonusSprite b in bonus)
                    b.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(scoreFont,"score:" +(Math.Round((double)points/100)).ToString(), new Vector2(25, 25), Color.Black);

            //spriteBatch.End();

            //base.Draw(gameTime);
        }
    }
}

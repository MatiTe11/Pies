using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Pies
{
    enum ActiveScreen
    {
        start, game, over
    };

    class ScreenManager
    {
        //List<Screen> screens;
        GameScreen gameScreen;
        StartScreen startScreen;
        OverScreen overScreen;
        int screenWidth, screenHeight;
        ActiveScreen activeScreen;

        public ScreenManager(int screenWidth, int screenHeight)
        {
            //screens = new List<Screen>();
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            activeScreen = ActiveScreen.start;
            gameScreen = new GameScreen(screenWidth, screenHeight);
            startScreen = new StartScreen(screenWidth, screenHeight);
            overScreen = new OverScreen(screenWidth, screenHeight);
        }

        public void LoadContent(ContentManager content)
        {
            gameScreen.LoadContent(content);
            startScreen.LoadContent(content);
            overScreen.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            //screens[activeScreen].Update(gameTime);

            if(activeScreen == ActiveScreen.game)
            {
                gameScreen.Update(gameTime);
                if (gameScreen.EndGame)
                    activeScreen = ActiveScreen.over;
            }
            else if(activeScreen == ActiveScreen.start)
            {
                startScreen.Update(gameTime);
                if(startScreen.FinishedScreen() == 0)
                {
                    activeScreen = ActiveScreen.game;
                    startScreen.Reset();
                }
            }
            else if (activeScreen == ActiveScreen.over)
            {
                if (overScreen.FinishedScreen() == 0)
                {
                    activeScreen = ActiveScreen.start;
                    overScreen.Reset();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (activeScreen == ActiveScreen.game)
            {
                gameScreen.Draw(spriteBatch);
            }
            else if (activeScreen == ActiveScreen.start)
            {
                startScreen.Draw(spriteBatch);
            }
            else if (activeScreen == ActiveScreen.over)
            {
                overScreen.Draw(spriteBatch);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pies
{
    class OverScreen : Screen
    {
        private float scale;
        Texture2D screenTex;
        bool spacePressed;

        public OverScreen(int screenW, int screenH) : base(screenW, screenH)
        {
            spacePressed = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(screenTex, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale), SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override int FinishedScreen()
        {
            if (spacePressed)
                return 0;
            else
                return -1;
        }

        public override void LoadContent(ContentManager Content)
        {
            screenTex = Content.Load<Texture2D>("overscreen");
            scale = (float)screenWidth / (float)screenTex.Width;
        }

        public override void Reset()
        {

        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                spacePressed = true;
            }
        }
    }
}

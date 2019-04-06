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
    class StartScreen : Screen
    {
        private float scale;
        Texture2D screenTex;

        public StartScreen(int screenW, int screenH) :base(screenW,screenH)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager Content)
        {
            screenTex = Content.Load<Texture2D>("startScreen");
        }

        public override void Reset()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

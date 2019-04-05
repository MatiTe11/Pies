﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pies
{
    interface IScreen
    {
        void Update(GameTime gameTime);
        void Reset();
        void Draw(SpriteBatch spriteBatch);
    }
}

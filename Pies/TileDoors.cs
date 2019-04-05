using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Pies
{
    class TileDoors : Tile
    {
        private bool shit;
        public TileDoors(int X, int Y, int tilesize) : base(X, Y, tilesize)
        {
            shit = false;
        }

        public bool CleanShit
        {
            get { return shit; }
            set { shit = value; }
        }
    }
}

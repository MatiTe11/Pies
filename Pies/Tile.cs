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
    abstract class Tile
    {
        private Rectangle rec = new Rectangle();
        private Point pos = new Point();
        public Tile(int X, int Y, int tilesize)
        {
            pos.X = X;
            pos.Y = Y;
            rec.X = X * tilesize;
            rec.Y = Y * tilesize;
            rec.Height = tilesize;
            rec.Width = tilesize;
        }


        public int PosX
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        public int PosY
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
        public int RecX
        {
            get { return rec.X; }
            set { rec.X = value; }
        }
        public int RecY
        {
            get { return rec.Y; }
            set { rec.Y = value; }
        }
        public int RecSize
        {
            get { return rec.Height; }
            set
            {
                rec.Height = value;
                rec.Width = value;
            }
        }

    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Ball
    {
        public Texture2D Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Rectangle getHitbox()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Sprite.Bounds.Width, Sprite.Bounds.Height);
        }
    }
}

using Hyper_Galaxy.render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy
{
    class Hud
    {
        public Hud()
        {

        }
        public void Update(GameTime t)
        {

        }
        public void Draw(DrawManager d,fontManager f,Planets p)
        {
            int width = 480;
            int height = 130;

            f.DrawText("planet ID: ", new Vector3(-230, height, 0), 1, Vector3.Zero, Color.White, d, false);
            f.DrawText(p.ID.ToString(), new Vector3(-150, height, 0), 1, Vector3.Zero, Color.White, d, false);

            f.DrawText("| Population: ", new Vector3(-140, height, 0), 1, Vector3.Zero, Color.White, d, false);
            f.DrawText(p._currentHab.ToString(), new Vector3(-50, height, 0), 1, Vector3.Zero, Color.White, d, false);

            f.DrawText("Health: ", new Vector3(0, height, 0), 1, Vector3.Zero, Color.White, d, false);
            f.DrawText(p.health.ToString(), new Vector3(50, height, 0), 1, Vector3.Zero, Color.White, d, false);

            f.DrawText("Money: ", new Vector3(100, height, 0), 1, Vector3.Zero, Color.White, d, false);
            f.DrawText(p.CurrentMoney.ToString(), new Vector3(150, height, 0), 1, Vector3.Zero, Color.White, d, false);
        }
    }
}

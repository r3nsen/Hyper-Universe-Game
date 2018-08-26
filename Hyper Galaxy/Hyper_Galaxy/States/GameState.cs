using Hyper_Galaxy.render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy.States
{
    class GameState
    {
        Hud _hud;

        World _world;
       

        public GameState(long seed)
        {
            _world = new World(seed);
             _hud = new Hud();
        }
        public void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
        }
        public void Draw(DrawManager _DM,fontManager f)
        {
            _world.Draw(_DM,f);

            if (getWorld().planetFocus == 0)
            {
                _hud.Draw(_DM, f, _world.center);
            }
            else
            {
                _hud.Draw(_DM, f, _world.getPlanets()[_world.planetFocus - 1]);
            }

            
        }
        public void setWorldTexture(Texture2D tex)
        {
            _world.SetTex(tex);
        }
        public World getWorld()
        {
            return _world;
        }
    }
}

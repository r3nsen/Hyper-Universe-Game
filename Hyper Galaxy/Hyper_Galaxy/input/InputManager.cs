using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy.input
{
    class InputManager
    {
        protected KeyboardState oldK_State;
        protected MouseState oldM_State;
        public struct keys
        {
            public bool W, A, S, D, ESC;
          
            public bool LC, RC, MC;
          
            public int MovX, MovY,MovZ;
            public int PosX, PosY;
        }
        public keys _keys;
        public bool _MovAction;

        public InputManager()
        {
            oldK_State = Keyboard.GetState();
            oldM_State = Mouse.GetState();
        }
        public void updateInput()
        {
            KeyboardUpdate();
            MouseUpdate();
        }

        protected virtual void KeyboardUpdate()
        {
           

        }

        protected virtual void MouseUpdate()
        {
           

        }

    }
}

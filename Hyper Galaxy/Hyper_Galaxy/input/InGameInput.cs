using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy.input
{
    class InGameInput : InputManager
    {
        public InGameInput()
        {

        }

        protected override void KeyboardUpdate()
        {
            KeyboardState newK_State = Keyboard.GetState();

            if (newK_State.IsKeyDown(Keys.Escape))
            {

                if (!oldK_State.IsKeyDown(Keys.Escape))
                {
                    _keys.ESC = true;
                }
            }
            else if (oldK_State.IsKeyDown(Keys.Escape))
            {

            }

            oldK_State = newK_State;
        
        }
        protected override void MouseUpdate()
        {
            MouseState newM_State = Mouse.GetState();

            if (newM_State.LeftButton == ButtonState.Pressed)
            {

                if (!(oldM_State.LeftButton == ButtonState.Pressed))
                {
                    _keys.LC = true;
                }
            }
            else if (oldM_State.LeftButton == ButtonState.Pressed)
            {

            }

            if (newM_State.LeftButton == ButtonState.Released)
            {

                if (!(oldM_State.LeftButton == ButtonState.Released))
                {
                    _keys.LC = false;
                }
            }
            else if (oldM_State.LeftButton == ButtonState.Released)
            {

            }

            if (newM_State.RightButton == ButtonState.Pressed)
            {

                if (!(oldM_State.RightButton == ButtonState.Pressed))
                {
                    _keys.RC = true;
                }
            }
            else if (oldM_State.RightButton == ButtonState.Pressed)
            {

            }

            if (_MovAction)
            {
                if (newM_State.X != oldM_State.X)
                {
                    _keys.MovX = newM_State.X - oldM_State.X;
                }
                if (newM_State.Y != oldM_State.Y)
                {
                    _keys.MovY = newM_State.Y - oldM_State.Y;
                }
               
            }
            if (newM_State.ScrollWheelValue != oldM_State.ScrollWheelValue)
            {
                _keys.MovZ = newM_State.ScrollWheelValue - oldM_State.ScrollWheelValue;
            }

            _keys.PosX = newM_State.X;
            _keys.PosY = newM_State.Y;

            oldM_State = newM_State;
            base.MouseUpdate();
        }
    }
}

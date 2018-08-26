using Hyper_Galaxy.input;
using Hyper_Galaxy.render;
using Hyper_Galaxy.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy
{
    class ScreenManager
    {
        public GameState _gameState;
        public InGameInput _gameInput;
        public fontManager font;
        public Hud _hud;
        bool lastLC;
        long seed;
        public Vector3 focus;

        public enum States
        {
            SPLASH,
            MENU,
            IN_GAME,
            GAME_OVER
        }
        public States CurrentState = States.IN_GAME;
        private bool foc;

        public ScreenManager()
        {
            //  long seed = 734255887109605;
            Random r = new Random();
            seed = (long)Math.Round(r.NextDouble() * 1000000000000000);//
            //seed = 232644205555620;
            _gameState = new GameState(seed);
            _gameInput = new InGameInput();
            font = new fontManager();
            _hud = new Hud();

        }
        public void Update(GameTime gameTime)
        {
            switch (CurrentState)
            {
                case States.IN_GAME:
                    _gameState.Update(gameTime);
                    break;
            }
            _hud.Update(gameTime);
            if (_gameState.getWorld().planetFocus == 0)
            {
                //focus = _gameState.getWorld().center._position;
            }
            else
            {
              //  focus = _gameState.getWorld().getPlanets()[_gameState.getWorld().planetFocus - 1]._position;
            }
            if (foc)
            {
               // _gameState.getWorld().position.X = focus.X;
              //  _gameState.getWorld().position.Y = -focus.Y;
            }
        }
        public void Draw(DrawManager _DM)
        {
            switch (CurrentState)
            {
                case States.IN_GAME:
                    _gameState.Draw(_DM, font);
                    //
                    foreach (Planets p in _gameState.getWorld().getPlanets())
                    {
                        Vector2 b = new Vector2(_gameInput._keys.PosX, _gameInput._keys.PosY);
                        if (b.X > 0 && b.Y > 0 && b.X < 480 && b.Y < 270)
                        {
                            if (_gameState.getWorld().ColisionTest(
                                new Vector2(
                                    (p._position.X + _gameState.getWorld().position.X - 240)*(_gameState.getWorld().position.Z + 1),
                                    (p._position.Y + _gameState.getWorld().position.Y - (270 / 2))) * (_gameState.getWorld().position.Z + 1),
                                    b,
                                    (int)(25* (_gameState.getWorld().position.Z + 1))))
                            {
                                font.DrawText(p.ID.ToString(), new Vector3(-200, -100, 0), 2, Vector3.Zero, Color.White, _DM, false);
                                break;
                            }
                        }
                    }
                    //
                    break;
            }

        }

        internal void Input()
        {
            switch (CurrentState)
            {
                case States.IN_GAME:
                    _gameInput.updateInput();
                    if (_gameInput._keys.LC)
                    {
                        foc = false;
                        _gameInput._MovAction = true;
                    }
                    else
                    {
                        _gameInput._MovAction = false;
                    }

                    if (_gameInput._keys.LC == false && lastLC == true)
                    {
                        Vector2 b = new Vector2(_gameInput._keys.PosX, _gameInput._keys.PosY);

                        foreach (Planets p in _gameState.getWorld().getPlanets())
                        {
                            if (b.X > 0 && b.Y > 0 && b.X < 480 && b.Y < 270)
                            {
                                if (_gameState.getWorld().ColisionTest(
                                    new Vector2(
                                        p._position.X + _gameState.getWorld().position.X + 240,
                                    p._position.Y + _gameState.getWorld().position.Y + (270 / 2)),
                                    b,
                                    (int)(25 * (_gameState.getWorld().position.Z + 1))))
                                {
                                   // _gameState.getWorld().position.X = p._position.X + _gameState.getWorld().position.X + 240;
                                   // _gameState.getWorld().position.Y = p._position.Y + _gameState.getWorld().position.Y + 135;
                                    for (int i = 0; i < _gameState.getWorld().getPlanets().Length; i++)
                                    {
                                        if (_gameState.getWorld().getPlanets()[i].ID == p.ID)
                                        {
                                            _gameState.getWorld().planetFocus = i + 1;
                                            break;
                                        }
                                        // focus = _gameState.getWorld().getPlanets()[i]._position;
                                    }
                                    foc = true;
                                    break;
                                }
                            }
                        }

                    }

                    if (_gameInput._keys.MovX != 0)
                    {
                        foc = false;
                        _gameState.getWorld().position.X -= _gameInput._keys.MovX;
                        _gameInput._keys.MovX = 0;
                    }
                    if (_gameInput._keys.MovY != 0)
                    {
                        foc = false;
                        _gameState.getWorld().position.Y += _gameInput._keys.MovY;
                        _gameInput._keys.MovY = 0;
                    }
                    if (_gameInput._keys.MovZ != 0)
                    {
                        // foc = false;
                        _gameState.getWorld().position.Z -= _gameInput._keys.MovZ / 100;
                        _gameInput._keys.MovZ = 0;
                        if (_gameState.getWorld().position.Z > 7) _gameState.getWorld().position.Z = 7;
                        if (_gameState.getWorld().position.Z < 0.001f) _gameState.getWorld().position.Z = 0.001f;
                    }
                    break;
            }
            lastLC = _gameInput._keys.LC;
        }
    }
}

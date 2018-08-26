using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Hyper_Galaxy.render;
using Hyper_Galaxy.sound;
using Hyper_Galaxy.input;


/*****************************************************************************************************
****************************      Hyper Galaxy                            ****************************
****************************      v 1.0                                   ****************************
****************************      A game by Renato Senhorini (r3nsen)     ****************************
****************************      08/11/2018                              ****************************
*****************************************************************************************************/
namespace Hyper_Galaxy
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        DrawManager _DM;
        ScreenManager _screenManager;
        SoundManager _sm;
      //  InputManager _im;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 270;
          //  graphics.ToggleFullScreen();
            Window.Title = "HyperGalaxy";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;

        }

      
        protected override void Initialize()
        {
          
            base.Initialize();
            graphics.PreferMultiSampling = false;
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        protected override void LoadContent()
        {
            _DM = new DrawManager(GraphicsDevice);
            _screenManager = new ScreenManager();
            _screenManager._gameState.setWorldTexture(Content.Load<Texture2D>("Textures/planets"));
            _screenManager.font.setFontTexture(Content.Load<Texture2D>("Textures/font"));
            _sm = new SoundManager();
            _sm.AddSong(Content.Load<Song>("Sounds/Overworld"), 0);
            _sm.PlaySong(0, true);
        }

       
        protected override void UnloadContent()
        {
          
        }

      
        protected override void Update(GameTime gameTime)
        {

            _screenManager.Input();
            _screenManager.Update(gameTime);
           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (_screenManager.CurrentState == ScreenManager.States.IN_GAME)
            {
                _DM.ResetMatrices(Window.ClientBounds.Width, Window.ClientBounds.Height, 1, _screenManager._gameState.getWorld().position);
            }
            else
            {
                _DM.ResetMatrices(Window.ClientBounds.Width, Window.ClientBounds.Height, 1,_screenManager.focus);
            }
            _screenManager.Draw(_DM);
            _DM.Flush();
         
            base.Draw(gameTime);
        }
    }
}

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
using MGTK.Services;
using MGTK.Theming;

namespace MGTKExample
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _Graphics;
        SpriteBatch _SpriteBatch;
		WindowManager _WindowManager;

		int _Width = 1024, _Height = 768;

		bool _FullScreen = true;

        public Game()
        {
			_Graphics = new GraphicsDeviceManager(this);
			
			_WindowManager = new WindowManager(_Width, _Height);

			IsMouseVisible = false;

            Content.RootDirectory = "Content";
        
			SetResolution();
		}

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
			Theme theme = ThemeIO.Load("./MGTK.THX", GraphicsDevice);

			_SpriteBatch = new SpriteBatch (GraphicsDevice);

			_WindowManager.Theme = theme;

            frmNotes testForm1 = new frmNotes(_WindowManager) { X = 120, Y = 40, Width = 320, Height = 240, BackColor = new Color(170, 170, 170), Text = "Notes", MaximumWidth = 640, MaximumHeight = 480 };
            frmTest testForm2 = new frmTest(_WindowManager) { X = 15, Y = 10, MinimumWidth = 230, Width = 230, MinimumHeight = 240, Height = 240, 
                BackColor = new Color(170, 170, 170), Text = "Window", MaximumWidth = 320, MaximumHeight = 400 };
            frmMixer testForm3 = new frmMixer(_WindowManager) { X = 260, Y = 30, Width = 185, Height = 160, BackColor = new Color(170, 170, 170), Text = "Mixer", Resizable = false };
            frmListBoxes testForm4 = new frmListBoxes(_WindowManager)
            {
                X = 105,
                Y = 260,
                MinimumWidth = 230,
                Width = 340,
                MinimumHeight = 130,
                Height = 130,
                BackColor = new Color(170, 170, 170),
                Text = "ListBoxes"
            };

            _WindowManager.Forms.Add(testForm1);
            _WindowManager.Forms.Add(testForm2);
            _WindowManager.Forms.Add(testForm3);
            _WindowManager.Forms.Add(testForm4);
        }

        protected override void Update (GameTime gameTime)
		{            
			SetResolution ();

			_WindowManager.Update(gameTime);

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			_SpriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null);
			
            GraphicsDevice.Clear(Color.Black);

            _WindowManager.Draw(_SpriteBatch);

            _SpriteBatch.End();

			base.Draw(gameTime);
        }

		void SetResolution()
		{
			_Graphics.PreferredBackBufferWidth = _Width;
			_Graphics.PreferredBackBufferHeight = _Height;

			_Graphics.IsFullScreen = _FullScreen;

			_Graphics.ApplyChanges();

			Window.AllowUserResizing = true;
		}
    }
}

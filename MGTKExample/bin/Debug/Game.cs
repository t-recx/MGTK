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
using GameResolution;

namespace MGTKExample
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		WindowManager windowManager;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
			windowManager = new WindowManager(640, 480);
			
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
			Theme Theme = ThemeIO.Load("./MGTK.THX", GraphicsDevice);

			spriteBatch = new SpriteBatch (GraphicsDevice);

			windowManager.Theme = Theme;

            frmNotes testForm1 = new frmNotes(windowManager) { X = 120, Y = 40, Width = 320, Height = 240, BackColor = new Color(170, 170, 170), Text = "Notes", MaximumWidth = 640, MaximumHeight = 480 };
            frmTest testForm2 = new frmTest(windowManager) { X = 15, Y = 10, MinimumWidth = 230, Width = 230, MinimumHeight = 240, Height = 240, 
                BackColor = new Color(170, 170, 170), Text = "Window", MaximumWidth = 320, MaximumHeight = 400 };
            frmMixer testForm3 = new frmMixer(windowManager) { X = 260, Y = 30, Width = 185, Height = 160, BackColor = new Color(170, 170, 170), Text = "Mixer", Resizable = false };
            frmListBoxes testForm4 = new frmListBoxes(windowManager)
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

            windowManager.Forms.Add(testForm1);
            windowManager.Forms.Add(testForm2);
            windowManager.Forms.Add(testForm3);
            windowManager.Forms.Add(testForm4);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            windowManager.Update(gameTime);

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null);
			
            GraphicsDevice.Clear(Color.Black);

            windowManager.Draw(GraphicsDevice, spriteBatch);

            spriteBatch.End();

			base.Draw(gameTime);
        }
    }
}

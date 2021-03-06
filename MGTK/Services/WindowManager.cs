using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MGTK.Controls;
using MGTK.Messaging;
using MGTK.Theming;
using MGTK.Interfaces.Services;
using MGTK.Factories;

namespace MGTK.Services
{
    public class WindowManager
    {
        public List<Form> Forms;

        public Theme Theme;

        public int MouseX, MouseY;

        public bool MouseLeftPressed = false;
        public bool MouseRightPressed = false;
        public bool PreviousMouseLeftPressed = false;
        public bool PreviousMouseRightPressed = false;

        public Texture2D MouseCursor;

        private Keys[] PreviousKeysPressed = null;

        public SpriteBatch spriteBatch;

        public GameTime gameTime;

        public int MaximizedWindowWidth, MaximizedWindowHeight;
        public int ScreenWidth, ScreenHeight;

        bool Initialized = false;
		
		private IDrawingService _DrawingService = null;

		public IDrawingService DrawingService
		{
			get
			{
				if (_DrawingService == null)
					_DrawingService = DrawingServiceFactory.GetSingletonInstance ();

				return _DrawingService;
			}
			set
			{
				_DrawingService = value;
			}
		}

        public WindowManager(int maximizedwindowwidth, int maximizedwindowheight)
        {
            ScreenWidth = MaximizedWindowWidth = maximizedwindowwidth;
            ScreenHeight = MaximizedWindowHeight = maximizedwindowheight;

            Forms = new List<Form>();        
		}

        public void Draw(SpriteBatch sBatch)
        {
            spriteBatch = sBatch;

            InitializeFormsNotInitialized();

            foreach (Form form in Forms)
                Messages.SendMessage(form, MessageEnum.Draw, null);

            if (MouseCursor != null)
                DrawingService.Draw(spriteBatch, MouseCursor, new Vector2(MouseX, MouseY), Color.White, 0);
        }

        public void Update(GameTime gTime)
        {
            gameTime = gTime;
            MouseCursor = Theme.DefaultCursor;

            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();

            PreviousMouseLeftPressed = MouseLeftPressed;
            PreviousMouseRightPressed = MouseRightPressed;

            MouseX = ms.X;
            MouseY = ms.Y;

            if (!MouseLeftPressed && ms.LeftButton == ButtonState.Pressed)
                MouseLeftPressed = true;
            if (MouseLeftPressed && ms.LeftButton != ButtonState.Pressed)
                MouseLeftPressed = false;

            if (!MouseRightPressed && ms.RightButton == ButtonState.Pressed)
                MouseRightPressed = true;
            if (MouseRightPressed && ms.RightButton != ButtonState.Pressed)
                MouseRightPressed = false;

            if (Forms != null && Forms.Count > 0)
            {
                InitializeFormsNotInitialized();

                if (!PreviousMouseLeftPressed && MouseLeftPressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseLeftPressed);
                if (!PreviousMouseRightPressed && MouseRightPressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseRightPressed); 
                if (ms.LeftButton == ButtonState.Pressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseLeftDown);
                if (ms.RightButton == ButtonState.Pressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseRightDown);
                if (!MouseLeftPressed && PreviousMouseLeftPressed == true)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseLeftClick);
                if (!MouseRightPressed && PreviousMouseRightPressed == true)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseRightClick);
                if (PreviousMouseLeftPressed && !MouseLeftPressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseLeftUp);
                if (PreviousMouseRightPressed && !MouseRightPressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.MouseRightUp);

                Keys[] KeysPressed = ks.GetPressedKeys();

                foreach (Keys keyPressed in KeysPressed)
                    Messages.BroadcastMessage(Forms, MessageEnum.KeyDown, keyPressed);

                if (PreviousKeysPressed != null)
                {
                    foreach (Keys keyPressed in PreviousKeysPressed)
                        if (!KeysPressed.Contains(keyPressed))
                            Messages.BroadcastMessage(Forms, MessageEnum.KeyUp, keyPressed);
                }

                PreviousKeysPressed = KeysPressed;

                foreach (Form form in Forms)
                    Messages.SendMessage(form, MessageEnum.Logic, null);
            }
        }

        private void InitializeFormsNotInitialized()
        {
            if (Forms == null || Forms.Count == 0)
                return;

            foreach (Form form in Forms)
            {
                if (form.Initialized)
                    continue;

                form.InitControl();
                BringToFront(form);

                Messages.BroadcastMessage(Forms, MessageEnum.Init);
            }
        }

        public void BringToFront(Form form)
        {
            float interval = 1 / (float)Forms.Count;

            form.Z = interval;
            form.Focused = true;

            List<Form> FormsFiltered = Forms.FindAll(f => f != form);

            if (FormsFiltered != null)
            {
                float newZ = interval;

                FormsFiltered = FormsFiltered.OrderBy(f => f.Z).ToList();

                foreach (Form f in FormsFiltered)
                {
                    newZ += interval;
                    f.Focused = false;
                    f.Z = newZ;
                }
            }
            
            Forms = Forms.OrderBy(f => f.Z).ToList();
        }
    }
}

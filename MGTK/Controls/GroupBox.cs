using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MGTK.Services;
using MGTK.Theming;

namespace MGTK.Controls
{
    public class GroupBox : Control
    {
        private List<Texture2D> frame = null;

        public List<Texture2D> Frame
        {
            get
            {
                if (frame != null)
                    return frame;

                return Theme.GroupFrame;
            }
            set
            {
                frame = value;
            }
        }

        public GroupBox(Form formowner)
            : base(formowner)
        {
        }

        public override void Draw()
        {
            if (Frame != null)
            {
                DrawingService.DrawFrame(spriteBatch, Frame, OwnerX + X,
                    OwnerY + Y + DrawingService.GetBMFontHeight(Font) / 2, Width, Height - DrawingService.GetBMFontHeight(Font) / 2, Z);

                DrawingService.DrawRectangle(spriteBatch, Frame[(int)FramePart.Background], Color.White,
                    OwnerX + X + Width / 2 - Font.MeasureString(Text) / 2, 
                    OwnerY + Y,
                    Font.MeasureString(Text), DrawingService.GetBMFontHeight(Font), Z - 0.0018f);
            }

            DrawingService.DrawBMCenteredText(spriteBatch, Font, Text, OwnerX + X + Width / 2, OwnerY + Y, ForeColor, null, Z - 0.002f);

            if (Controls != null)
                for (int i = 0; i < Controls.Count; i++)
                    Controls[i].Z = Z - 0.0021f;
        }
    }
}

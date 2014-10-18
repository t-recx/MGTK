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

namespace MGTK.Controls
{
    public class Label : Control
    {
        public Label(Form formowner)
            : base(formowner)
        {
        }

        public override void Draw()
        {
			DrawingService.DrawBMString(spriteBatch, Font, Text, OwnerX + X, OwnerY + Y, ForeColor, Z - 0.00001f);

            base.Draw();
        }
    }
}

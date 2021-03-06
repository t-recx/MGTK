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
using MGTK.Fonts;
using MGTK.Theming;
using MGTK.Interfaces.Services;

namespace MGTK.Services
{
    public class DrawingService : IDrawingService
    {
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color, float layerDepth)
        {
            spriteBatch.Draw(texture, position, null, color, 0, new Vector2(0, 0), 1, SpriteEffects.None, layerDepth);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int x, int y, int width, int height, Color color, bool stretch, float layerDepth)
        {
            Rectangle ?sourceRect = null;

            if (!stretch)
                sourceRect = new Rectangle(0, 0, width, height);

            spriteBatch.Draw(texture, new Rectangle(x, y, width, height), sourceRect, color, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth);
        }

		public void DrawBMString (SpriteBatch spriteBatch, BitmapFont font, string text, int x, int y, Color color, float depth)
		{
			if (text == null) 
				return;

			int dx = x;
			int dy = y;

			foreach(char c in text)
			{
				FontChar fc;

				if (font.CharacterMap.TryGetValue(c, out fc))
				{
					var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
					var position = new Vector2(dx + fc.XOffset, dy + fc.YOffset);

					spriteBatch.Draw(
						font.Texture, 
						position, 
						sourceRectangle, 
						color, 
						0, 
						new Vector2(0, 0), 
						new Vector2(1, 1), 
						SpriteEffects.None, 
						depth);

					dx += fc.XAdvance;
				}
			}
		}

        public void DrawBMCenteredText(SpriteBatch spriteBatch, BitmapFont font, string text, int x, int y, Color drawColor, Color? shadowColor, float layerDepth)
        {
            if (text == null)
                return;

            int centerXPosition = x - (font.MeasureString(text) / 2);

            DrawBMTextShadow(spriteBatch, font, text, centerXPosition, y, drawColor, shadowColor, layerDepth);
        }

        public void DrawBMTextShadow(SpriteBatch spriteBatch, BitmapFont font, string text, int x, int y, Color drawColor, Color? shadowColor, float layerDepth)
        {
            if (shadowColor.HasValue)
				DrawBMString(spriteBatch, font, text, x + 1, y + 1, drawColor, layerDepth);

            DrawBMString(spriteBatch, font, text, x, y, drawColor, layerDepth);
        }

        public void DrawFrame(SpriteBatch spriteBatch, int x, int y, int width, int height, 
            Texture2D UpLeftCorner, Texture2D UpRightCorner, Texture2D DownLeftCorner, Texture2D DownRightCorner,
            Texture2D LeftBar, Texture2D RightBar, Texture2D UpBar, Texture2D DownBar,
            Texture2D Background, float layerDepth)
        {
            if (Background != null)
                spriteBatch.Draw(Background, new Rectangle(x, y, width, height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth - 0.00001f);

            if (LeftBar != null)
                spriteBatch.Draw(LeftBar, new Rectangle(x, y, LeftBar.Width, height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth - 0.000011f);
            if (RightBar != null)
                spriteBatch.Draw(RightBar, new Rectangle(x + width - RightBar.Width, y, RightBar.Width, height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth - 0.000011f);
            if (UpBar != null)
                spriteBatch.Draw(UpBar, new Rectangle(x, y, width, UpBar.Height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth - 0.000011f);
            if (DownBar != null)
                spriteBatch.Draw(DownBar, new Rectangle(x, y + height - DownBar.Height, width, DownBar.Height), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth - 0.000011f);

            if (UpLeftCorner != null)
                Draw(spriteBatch, UpLeftCorner, new Vector2(x, y), Color.White, layerDepth - 0.00012f);
            if (DownLeftCorner != null)
                Draw(spriteBatch, DownLeftCorner, new Vector2(x, y + height - DownLeftCorner.Height), Color.White, layerDepth - 0.000012f);
            if (UpRightCorner != null)
                Draw(spriteBatch, UpRightCorner, new Vector2(x + width - UpRightCorner.Width, y), Color.White, layerDepth - 0.000012f);
            if (DownRightCorner != null)
                Draw(spriteBatch, DownRightCorner, new Vector2(x + width - DownRightCorner.Width, y + height - DownLeftCorner.Height), Color.White, layerDepth - 0.000012f);
        }

        public void DrawFrame(SpriteBatch spriteBatch, List<Texture2D> frame, int x, int y, int width, int height, float layerDepth)
        {
            DrawFrame(spriteBatch, x, y, width, height, frame[(int)FramePart.UpLeft], frame[(int)FramePart.UpRight], frame[(int)FramePart.DownLeft], frame[(int)FramePart.DownRight],
                frame[(int)FramePart.Left], frame[(int)FramePart.Right], frame[(int)FramePart.Up], frame[(int)FramePart.Down], frame[(int)FramePart.Background], layerDepth);
        }

        public void DrawRectangle(SpriteBatch spriteBatch, Texture2D dot, Color color, int x, int y, int width, int height, float layerDepth)
        {
            if (dot == null)
                return;

            spriteBatch.Draw(dot, new Rectangle(x, y, width, height), null, color, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth);
        }

		public int MeasureString(BitmapFont font, string s)
		{
			return font.MeasureString(s);
		}

		public int GetBMFontHeight (BitmapFont font)
		{
			return font.FontHeight;
		}
    }
}

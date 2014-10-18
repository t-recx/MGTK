using System;
using Microsoft.Xna.Framework.Graphics;
using MGTK.Fonts;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MGTK.Interfaces.Services
{
	public interface IDrawingService
	{
		void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color, float layerDepth);
		void Draw(SpriteBatch spriteBatch, Texture2D texture, int x, int y, int width, int height, Color color, bool stretch, float layerDepth);

		void DrawFrame(SpriteBatch spriteBatch, int x, int y, int width, int height, 
            Texture2D UpLeftCorner, Texture2D UpRightCorner, Texture2D DownLeftCorner, Texture2D DownRightCorner,
            Texture2D LeftBar, Texture2D RightBar, Texture2D UpBar, Texture2D DownBar,
           	Texture2D Background, float layerDepth);
		void DrawFrame(SpriteBatch spriteBatch, List<Texture2D> frame, int x, int y, int width, int height, float layerDepth);

		void DrawRectangle(SpriteBatch spriteBatch, Texture2D dot, Color color, int x, int y, int width, int height, float layerDepth);

		void DrawBMString (SpriteBatch spriteBatch, BitmapFont font, string text, int x, int y, Color color, float depth);
		void DrawBMCenteredText(SpriteBatch spriteBatch, BitmapFont font, string text, int x, int y, Color drawColor, Color? shadowColor, float layerDepth);
		void DrawBMTextShadow(SpriteBatch spriteBatch, BitmapFont font, string text, int x, int y, Color drawColor, Color? shadowColor, float layerDepth);

		int MeasureString(BitmapFont font, string s);
		int GetBMFontHeight (BitmapFont font);
	}
}


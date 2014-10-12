using System;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace MGTK.Fonts
{
	public class BitmapFont
	{
		public FontFile File { get; set; }
		public Texture2D Texture { get; set; }
        public Dictionary<char, FontChar> CharacterMap;
		public int FontHeight { get; set; }

		public int MeasureString (string text)
		{
			int numberOfChars = 0;

			foreach (char c in text)
			{
				FontChar fc;
				if (CharacterMap.TryGetValue (c, out fc))
					numberOfChars += fc.XAdvance;
			}

			return numberOfChars;
		}

		public static BitmapFont Load (GraphicsDevice graphicsDevice, string fontFile, string textureFile)
		{
			BitmapFont bitmapFont = new BitmapFont();			

			XmlSerializer deserializer = new XmlSerializer (typeof(FontFile));

			using (TextReader textReader = new StreamReader (fontFile))
			{
				bitmapFont.File = (FontFile)deserializer.Deserialize (textReader);

				textReader.Close ();
			}

			using (FileStream fsSprite = new FileStream(textureFile, FileMode.Open))
			{
				bitmapFont.Texture = Texture2D.FromStream (graphicsDevice, fsSprite);
			}

			bitmapFont.CharacterMap = new Dictionary<char, FontChar>();

			bitmapFont.FontHeight = 0;

            foreach(var fontCharacter in bitmapFont.File.Chars)
            {
				char c = (char)fontCharacter.ID;
				bitmapFont.CharacterMap.Add(c, fontCharacter);

				if (fontCharacter.Height > bitmapFont.FontHeight)
					bitmapFont.FontHeight = fontCharacter.Height;
            }

            return bitmapFont;
        }

		public BitmapFont ()
		{
		}
	}
}


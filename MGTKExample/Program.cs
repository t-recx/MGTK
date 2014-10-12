#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

#endregion

namespace MGTKExample
{
	static class Program
	{
		private static Game game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			game = new Game ();
			game.Run ();
		}
	}
}

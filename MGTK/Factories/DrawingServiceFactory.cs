using System;
using MGTK.Interfaces.Services;
using MGTK.Services;

namespace MGTK.Factories
{
	public static class DrawingServiceFactory
	{
		static IDrawingService _DrawingService = null;

		public static IDrawingService GetSingletonInstance()
		{
			if (_DrawingService == null)
				_DrawingService = new DrawingService();

			return _DrawingService;
		}
	}
}


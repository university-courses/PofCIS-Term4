using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace DrawShape.Utils
{
	public class Drawing
	{
		public static void DrawLine(ref Canvas canvas, Point start, Point end, Brush brush)
		{
			var line = new Line
			{
				X1 = start.X,
				Y1 = start.Y,
				X2 = end.X,
				Y2 = end.Y,
				StrokeThickness = 1,
				Stroke = brush,
				SnapsToDevicePixels = true
			};
			line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
			canvas.Children.Add(line);
		}
	}
}
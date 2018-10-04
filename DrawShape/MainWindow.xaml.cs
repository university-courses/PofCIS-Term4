using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Threading;
using DrawShape.BL;
using DrawShape.Utils;
using DrawShape.Classes;
using Point = DrawShape.Classes.Point;

namespace DrawShape
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly List<Classes.Point> _currentDrawingHexagon;
		
		/// <summary>
		/// Indicates if current picture is saved or not.
		/// </summary>
		private bool _pictureIsSaved;
		
		/// <summary>
		/// Holds current chosen hexagon's id.
		/// </summary>
		private int _currentHexagonId;

		/// <summary>
		/// Holds current chosen color to fill a hexagon's background.
		/// </summary>
		private Brush _currentFillColor;
		
		/// <summary>
		/// Holds current chosen color to fill a hexagon's border.
		/// </summary>
		private Brush _currentBorderColor;

		readonly DispatcherTimer _dhsTimer = new DispatcherTimer();

		private readonly Point _mouseLoc;

		private Polygon _expectedHexagon;

		private Line _expectedLine;
		
		/// <summary>
		/// Constructs the main window of an application.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			_currentHexagonId = -1;
			_currentDrawingHexagon = new List<Classes.Point>();
			_currentFillColor = new SolidColorBrush(Colors.Black);
			ColorPickerFill.Fill = _currentFillColor;
			_currentBorderColor = new SolidColorBrush(Colors.Black);
			ColorPickerBorder.Fill = _currentBorderColor;
			StartDrawingTicker();
			_mouseLoc = new Point();
		}

		private void NewButton_Click(object sender, RoutedEventArgs e)
		{
			if (!_pictureIsSaved)
			{
				SaveButton_Click(sender, e);
			}

			DrawingPanel.Children.Clear();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (DrawingPanel.Children.Count > 0 && !_pictureIsSaved)
			{
				FormBl.SaveHexagons(ref DrawingPanel);
				_pictureIsSaved = true;
			}
		}

		private void OpenButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var hexagons = FormBl.ReadHexagons();
				if (hexagons == null)
				{
					return;
				}

				foreach (var hexagon in hexagons)
				{
					DrawingPanel.Children.Add(hexagon.ToPolygon());
					var newMenuItem = new MenuItem {Header = hexagon.Name};
					newMenuItem.Click += SetCurrentHexagonFromMenu;
					ShapesMenu.Items.Add(newMenuItem);
				}

				_pictureIsSaved = true;
			}
			catch (Exception exc)
			{
				Util.MessageBoxFatal(exc.Message);
			}
		}

		private void MouseSetPoint(object sender, MouseButtonEventArgs e)
		{
			if (_currentDrawingHexagon.Count < 6)
			{
				var mousePos = e.GetPosition(DrawingPanel);
				_currentDrawingHexagon.Add(new Point(mousePos.X, mousePos.Y));
				if (_expectedHexagon == null)
				{
					_expectedHexagon = new Polygon
					{
						Stroke = _currentBorderColor,
						Opacity = 1
					};
					DrawingPanel.Children.Add(_expectedHexagon);
					_expectedLine = FormBl.GetLine(
						new Point(_currentDrawingHexagon[0].X, _currentDrawingHexagon[0].Y),
						new Point(_mouseLoc.X, _mouseLoc.Y), _currentBorderColor
					);
					DrawingPanel.Children.Add(_expectedLine);
				}

				_expectedHexagon.Points.Add(mousePos);
			}
			
			if (_currentDrawingHexagon.Count == 6)
			{
				var hexagon = new Hexagon($"Hexagon{DrawingPanel.Children.Count}", _currentDrawingHexagon, _currentFillColor, _currentBorderColor);
				DrawingPanel.Children.Add(hexagon.ToPolygon());
				var newMenuItem = new MenuItem {Header = hexagon.Name};
				newMenuItem.Click += SetCurrentHexagonFromMenu;
				ShapesMenu.Items.Add(newMenuItem);
				_currentDrawingHexagon.Clear();
				DrawingPanel.Children.Remove(_expectedHexagon);
				DrawingPanel.Children.Remove(_expectedLine);
				_expectedHexagon = null;
				_expectedLine = null;
			}
		}
		
		private void SetCurrentHexagonFromMenu(object sender, RoutedEventArgs e)
		{
			var menuItem = e.OriginalSource as MenuItem;
			try
			{
				if (menuItem != null)
				{
					_currentHexagonId = Util.GetHexagonIdByName(menuItem.Header.ToString(), DrawingPanel.Children);
				}
			}
			catch (InvalidDataException exc)
			{
				Util.MessageBoxFatal(exc.Message);
			}
		}

		private void SetFillColor(object sender, MouseButtonEventArgs e)
		{
			FormBl.SetColor(ref _currentFillColor, ref ColorPickerFill);
		}

		private void SetBorderColor(object sender, MouseButtonEventArgs e)
		{
			FormBl.SetColor(ref _currentBorderColor, ref ColorPickerBorder);
		}
		
		private void StartDrawingTicker()
		{
			_dhsTimer.Interval = TimeSpan.FromMilliseconds(10);
			_dhsTimer.Tick += DrawingHexagonSide;
			_dhsTimer.Start();
		}
		
		private void DrawingHexagonSide(object sender, EventArgs e)
		{
			if (_currentDrawingHexagon.Count > 0)
			{
				var lastPoint = _currentDrawingHexagon[_currentDrawingHexagon.Count - 1];
				_expectedLine.X1 = lastPoint.X;
				_expectedLine.Y1 = lastPoint.Y;
				_expectedLine.X2 = _mouseLoc.X;
				_expectedLine.Y2 = _mouseLoc.Y;
			}
		}
		
		private void MouseLoc(object sender, MouseEventArgs e)
		{
			var point = e.GetPosition(this);
			_mouseLoc.X = point.X + 7;
			_mouseLoc.Y = point.Y - 25;
		}
	}
}

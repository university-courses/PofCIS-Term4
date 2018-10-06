using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;

using DrawShape.BL;
using DrawShape.Utils;
using DrawShape.Classes;

using Point = DrawShape.Classes.Point;
using MenuItem = System.Windows.Controls.MenuItem;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DrawShape
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly List<Point> _currentDrawingHexagon;

		/// <summary>
		/// Indicates if current picture is saved or not.
		/// </summary>
		private bool _pictureIsSaved;
		
		/// <summary>
		/// Holds current chosen hexagon's id.
		/// </summary>
		private int _currentChosenHexagonId;

		/// <summary>
		/// Holds current chosen color to fill a hexagon's background.
		/// </summary>
		private Brush _currentFillColor;
		
		/// <summary>
		/// Holds current chosen color to fill a hexagon's border.
		/// </summary>
		private Brush _currentBorderColor;

		private readonly DispatcherTimer _dhsTimer = new DispatcherTimer();

		private readonly Point _mouseLoc;

		private Polygon _expectedHexagon;

		private Line _expectedLine;

		private Mode _currentMode;

		private System.Windows.Point _mouseDownLoc;

		private bool _mouseIsDown;

		/// <summary>
		/// Shortcuts. TODO: add general description.
		/// </summary>
		public static readonly RoutedCommand SetDrawingModeCommand = new RoutedCommand();
		public static readonly RoutedCommand SetMovingModeCommand = new RoutedCommand();
		public static readonly RoutedCommand SetFillColorCommand = new RoutedCommand();
		public static readonly RoutedCommand SetStrokeColorCommand = new RoutedCommand();
		public static readonly RoutedCommand NewDialogCommand = new RoutedCommand();
		public static readonly RoutedCommand SaveDialogCommand = new RoutedCommand();
		public static readonly RoutedCommand OpenDialogCommand = new RoutedCommand();
		
		/// <summary>
		/// Constructs the main window of an application.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			_currentChosenHexagonId = -1;
			_currentDrawingHexagon = new List<Point>();
			_currentFillColor = new SolidColorBrush(Colors.Black);
			ColorPickerFill.Fill = _currentFillColor;
			_currentBorderColor = new SolidColorBrush(Colors.Black);
			ColorPickerBorder.Fill = _currentBorderColor;
			StartDrawingTicker();
			_mouseLoc = new Point();
			_currentMode = Mode.Drawing;
			SetShortcuts();
			_mouseDownLoc = new System.Windows.Point();
			_mouseIsDown = false;
		}

		private static void SetShortcuts()
		{
			SetDrawingModeCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
			SetMovingModeCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
			SetFillColorCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
			SetStrokeColorCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
			NewDialogCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
			SaveDialogCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
			OpenDialogCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
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

				DrawingPanel.Children.Clear();
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

		private void SetCurrentHexagonFromMenu(object sender, RoutedEventArgs e)
		{
			var menuItem = e.OriginalSource as MenuItem;
			try
			{
				if (menuItem != null)
				{
					_currentChosenHexagonId = Util.GetHexagonIdByName(menuItem.Header.ToString(), DrawingPanel.Children);
				}
			}
			catch (InvalidDataException exc)
			{
				Util.MessageBoxFatal(exc.Message);
			}
		}

		private void SetFillColor(object sender, RoutedEventArgs e)
		{
			FormBl.SetColor(ref _currentFillColor, ref ColorPickerFill);
		}

		private void SetBorderColor(object sender, RoutedEventArgs e)
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

		private void SetDrawingMode(object sender, RoutedEventArgs e)
		{
			_currentMode = Mode.Drawing;
		}

		private void SetMovingMode(object sender, RoutedEventArgs e)
		{
			_currentMode = Mode.Moving;
		}

		public enum Mode
		{
			Drawing, Moving
		}
		
		private void ProcessDrawingOfHexagon(object sender, MouseButtonEventArgs e)
		{
			if (_currentMode == Mode.Drawing)
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
							Opacity = 1,
							StrokeThickness = 2
						};
						DrawingPanel.Children.Add(_expectedHexagon);
						_expectedLine = Util.GetLine(
							new Point(_currentDrawingHexagon[0].X, _currentDrawingHexagon[0].Y),
							new Point(_mouseLoc.X, _mouseLoc.Y), _currentBorderColor
						);
						_expectedLine.StrokeThickness = 2;
						DrawingPanel.Children.Add(_expectedLine);
					}

					_expectedHexagon.Points.Add(mousePos);
				}
			
				if (_currentDrawingHexagon.Count == 6)
				{
					var hexagon = new Hexagon($"Hexagon{DrawingPanel.Children.Count}", _currentDrawingHexagon, _currentFillColor, _currentBorderColor).ToPolygon();
					hexagon.MouseMove += MoveHexagonWithMouse; 
				//	hexagon.KeyDown += MoveHexagonWithKeys;
					DrawingPanel.Children.Add(hexagon);
					_currentChosenHexagonId++;
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
		}
		
		/*
		private void MoveHexagonWithKeys(object sender, KeyEventArgs e)
		{
			try
			{
				if (_currentMode == Mode.Moving && _currentChosenHexagonId > -1 && DrawingPanel.Children.Count > 0)
				{
					var newLoc = new System.Windows.Point(0, 0);
					if (Keyboard.IsKeyDown(Key.Up))
					{
						newLoc.Y = -5;
					}
					else if (Keyboard.IsKeyDown(Key.Right))
					{
						newLoc.X = 5;
					}
					else if (Keyboard.IsKeyDown(Key.Down))
					{
						newLoc.Y = 5;
					}
					else if (Keyboard.IsKeyDown(Key.Left))
					{
						newLoc.X = -5;
					}

					if (!(DrawingPanel.Children[_currentChosenHexagonId] is Polygon hexagon))
					{
						throw new InvalidCastException("can't move hexagon");
					}

					Util.MoveHexagonWithArrows(ref hexagon, newLoc);
				//	DrawingPanel.InvalidateVisual();
				}
			}
			catch (Exception exc)
			{
				Util.MessageBoxFatal(exc.ToString());
			}
		}
		*/
		
		private void MoveHexagonWithMouse(object sender, MouseEventArgs mouseEventArgs)
		{
			try
			{
				if (!_mouseIsDown)
				{
					return;
				}
				
				if (_currentMode == Mode.Moving && _currentChosenHexagonId > -1 && DrawingPanel.Children.Count > 0)
				{
					if (!(DrawingPanel.Children[_currentChosenHexagonId] is Polygon hexagon))
					{
						throw new InvalidCastException("can't move hexagon");
					}
					Util.MoveHexagonWithArrows(ref hexagon, new System.Windows.Point(
						_mouseLoc.X - _mouseDownLoc.X, _mouseLoc.Y - _mouseDownLoc.Y)
					);
				}
			}
			catch (Exception exc)
			{
				Util.MessageBoxFatal(exc.ToString());
			}
		}

		private void GetMouseDownPosition(object sender, MouseButtonEventArgs e)
		{
			if (_currentMode == Mode.Moving)
			{
				_mouseIsDown = true;
				_mouseDownLoc = e.GetPosition(this);	
			}
		}

		private void ResetMouseDownField(object sender, MouseButtonEventArgs e)
		{
			_mouseIsDown = false;
		}
	}
}

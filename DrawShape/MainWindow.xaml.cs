using Microsoft.Win32;

using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;

using DrawShape.Utils;
using DrawShape.Classes;

namespace DrawShape
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private List<Classes.Point> _currentDrawingHexagon;
		
		/// <summary>
		/// Indicates if current picture is saved or not.
		/// </summary>
		private bool _pictureIsSaved;
		
		/// <summary>
		/// Holds current chosen hexagon's id.
		/// </summary>
		private int _currentHexagonId;
		
		/// <summary>
		/// Constructs the main window of an application.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			_currentHexagonId = -1;
			_currentDrawingHexagon = new List<Classes.Point>();
		}

		private void NewButton_Click(object sender, RoutedEventArgs e)
		{
			if (!_pictureIsSaved)
			{
				// TODO: open saving dialog.
			}

			DrawingPanel.Children.Clear();
			_pictureIsSaved = false;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (DrawingPanel.Children.Count > 0 && !_pictureIsSaved)
			{
				var sfd = new SaveFileDialog
				{
					Filter = @"Xml files (*.xml)|*.xml|All files (*.*)|*.*",
					DefaultExt = "xml",
					FileName = "Rectangles",
					AddExtension = true
				};
				if (sfd.ShowDialog() == true)
				{
					var hexagons = new List<Hexagon>();
					foreach (var item in DrawingPanel.Children)
					{
						if (item is Polygon polygon)
						{
							hexagons.Add(Hexagon.FromPolygon(polygon));
						}
					}
					Serialization.SerializeHexagons(sfd.FileName, hexagons);
					_pictureIsSaved = true;
				}
			}
		}

		private void OpenButton_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new OpenFileDialog
			{
				Filter = @"Xml files (*.xml)|*.xml",
				DefaultExt = "xml",
				AddExtension = true
			};
			if (ofd.ShowDialog() == true)
			{
				try
				{
					var hexagons = Serialization.DeserializeHexagons(ofd.FileName);

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
					MessageBox.Show(exc.Message, "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void MouseSetPoint(object sender, MouseButtonEventArgs e)
		{
			if (_currentDrawingHexagon.Count < 6)
			{
				var mousePos = e.GetPosition(DrawingPanel);
				_currentDrawingHexagon.Add(new Classes.Point(mousePos.X, mousePos.Y));	
			}
			if (_currentDrawingHexagon.Count == 6)
			{
				if (Util.GetColorFromColorDilog(out var color))
				{
					var hexagon = new Hexagon($"Hexagon{DrawingPanel.Children.Count}", _currentDrawingHexagon, color);
					DrawingPanel.Children.Add(hexagon.ToPolygon());
					var newMenuItem = new MenuItem {Header = hexagon.Name};
					newMenuItem.Click += SetCurrentHexagonFromMenu;
					ShapesMenu.Items.Add(newMenuItem);
				}
				_currentDrawingHexagon.Clear();
			}
		}
		
		private void DrawingHexagon(object sender, MouseEventArgs e)
		{
			// TODO: display helper lines while drawing a hexagon. 
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
				MessageBox.Show(exc.Message, "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
			} 
		}
	}
}

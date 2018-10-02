using System.Windows;
using System.Collections.Generic;

using Microsoft.Win32;

using DrawShape.Classes;
using DrawShape.Utils;

namespace DrawShape
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public List<Hexagon> Hexagons { get; private set; }
		private bool _isSaved;
		
		/// <summary>
		/// Constructs the main window of an application.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Hexagons = new List<Hexagon>();
		}

		private void NewButton_Click(object sender, RoutedEventArgs e)
		{
			if (!_isSaved)
			{
				// TODO: open saving dialog.
			}
			
			// TODO: refresh drawing panel.
			
			_isSaved = false;
			
			MessageBox.Show("New Button Clicked");
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (Hexagons.Count > 0 && !_isSaved)
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
					Serialization.SerializeHexagons(sfd.FileName, Hexagons);
					_isSaved = true;
				}
			}
			
			MessageBox.Show("Save Button Clicked");
		}

		private void OpenButton_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new OpenFileDialog
			{
				Filter = @"Xml files (*.xml)|*.xml|All files (*.*)|*.*",
				DefaultExt = "xml",
				AddExtension = true
			};

			if (ofd.ShowDialog() == true)
			{
				Hexagons = Serialization.DeserializeHexagons(ofd.FileName);
				
				// TODO: draw deserialized hexagons.
				
				_isSaved = true;
			}
			MessageBox.Show("Open Button Clicked");
		}

		private void ShapesMenu_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Shapes Manu Clicked");
		}
	}
}

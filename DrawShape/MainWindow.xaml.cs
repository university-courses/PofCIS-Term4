using System.Windows;

namespace DrawShape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
        /// <summary>
        /// Constructs the main window of an application.
        /// </summary>
		public MainWindow()
		{
			InitializeComponent();
		}

        private void FileMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New Button Clicked");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save Button Clicked");
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open Button Clicked");
        }

        private void ShapesMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

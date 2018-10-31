using System.Windows;

namespace CargoDelivery
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void CreateOrder_Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Thank you for your order!");
		}
	}
}

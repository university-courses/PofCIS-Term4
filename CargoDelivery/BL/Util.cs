using System.Windows;

namespace CargoDelivery.BL
{
	public static class Util
	{
		public static void Error(string caption, string message)
		{
			MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
		}
		
		public static void Info(string caption, string message)
		{
			MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}

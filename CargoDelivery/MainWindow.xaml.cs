using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CargoDelivery.BL;
using CargoDelivery.Classes;

namespace CargoDelivery
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private uint _nextId;
		private readonly Order _order;
		private readonly Validator _validator;
		private readonly OrdersStorage _storage;
		
		public MainWindow()
		{
			InitializeComponent();
			
			_storage = new OrdersStorage("storage.xml");
			_storage.CreateIfNotExists();

			_validator = new Validator(new List<TextBox>
			{
				FirstName, LastName, Email, PhoneNumber, ClientAddressCity, ClientAddressStreet,
				ClientAddressBuildingNumber, ShopName, ShopAddressCity, ShopAddressStreet,
				ShopAddressBuildingNumber, GoodsCode, GoodsWeight
			});
			
			_nextId = 1;
			_order = new Order();
			DataContext = _order;
		}

		private void DiscoverOrders(object sender, RoutedEventArgs e)
		{
			OrdersExplorer.IsOpen = true;
		}


		private void Ok(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Not implemented!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			OrdersExplorer.IsOpen = false;
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Not implemented!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			OrdersExplorer.IsOpen = false;
		}


		private void CreateOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				_validator.Validate();
				_storage.Add(_order);
				_order.Id = _nextId++;
				MessageBox.Show("An order was created successfully!", "Cargo Delivery", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void InputFocused(object sender, RoutedEventArgs e)
		{
			if (e.OriginalSource is TextBox textBox)
			{
				textBox.SelectAll();
			}
		}
	}
}

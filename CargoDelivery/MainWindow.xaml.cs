using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using CargoDelivery.BL;
using CargoDelivery.Classes;

namespace CargoDelivery
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private long _nextId;
		private Order _order;
		private readonly Validator _validator;
		private readonly OrdersStorage _storage;
		
		public MainWindow()
		{
			InitializeComponent();
			EditOrderButton.IsEnabled = false;
			DeletOrderButton.IsEnabled = false;
			try
			{
				_storage = new OrdersStorage("storage.xml");
				_storage.CreateIfNotExists();
				var ordersList = _storage.RetrieveAllIds();
				_nextId = ordersList.Count != 0 ? ordersList.Keys.Last() + 1 : 0;
			}
			catch (NullReferenceException e)
			{
				Util.Error("Storage fatal error", e.Message);
				Application.Current.Shutdown();
			}
			_validator = new Validator(new List<TextBox>
			{
				FirstName, LastName, Email, PhoneNumber, ClientAddressCity, ClientAddressStreet,
				ClientAddressBuildingNumber, ShopName, ShopAddressCity, ShopAddressStreet,
				ShopAddressBuildingNumber, GoodsCode, GoodsWeight
			});
			ResetOrderInstance();
		}

		private void ExploreOrders(object sender, RoutedEventArgs e)
		{
			var orders = _storage.RetrieveAllIds();
			if (orders.Count > 0)
			{
				OrdersList.ItemsSource = orders; 
				OrdersExplorer.IsOpen = true;
				ResetOrderInstance();
			}
			else
			{
				Util.Info("Explore orders", "Orders database is empty!");
			}
		}
		
		private void SetTargetEditingOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				if (OrdersList.SelectedItems.Count == 1)
				{
					var selectedItem = (dynamic)OrdersList.SelectedItems[0];
					_order = _storage.Retrieve(selectedItem.Key);
					DataContext = _order;
				}
			}
			catch (Exception exc)
			{
				Util.Error("Can't set order for editing", exc.Message);
			}

			OrdersList.SelectedItem = null;
			OrdersExplorer.IsOpen = false;
			EditOrderButton.IsEnabled = false;
			DeletOrderButton.IsEnabled = false;
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			OrdersExplorer.IsOpen = false;
			EditOrderButton.IsEnabled = false;
			DeletOrderButton.IsEnabled = false;
		}

		private void CreateOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				_validator.Validate();
				if (_order.Id == -1)
				{
					_order.Id = _nextId++;
					_storage.Add(_order);
					ResetOrderInstance();
				}
				else
				{
					_storage.Update(_order.Id, _order);
				}
				Util.Info("Cargo Delivery", "An order was saved successfully!");
			}
			catch (Exception exc)
			{
				Util.Error("Order error", exc.Message);
			}
		}

		private void InputFocused(object sender, RoutedEventArgs e)
		{
			if (e.OriginalSource is TextBox textBox)
			{
				textBox.SelectAll();
			}
		}

		private void ItemIsSelected(object sender, RoutedEventArgs e)
		{
			EditOrderButton.IsEnabled = true;
			DeletOrderButton.IsEnabled = true;
		}

		private void NewOrder(object sender, RoutedEventArgs e)
		{
			_order = new Order {Id = -1};
			DataContext = _order;
		}

		private void DeleteOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				if (OrdersList.SelectedItems.Count != 1)
				{
					return;
				}

				var selectedItem = (dynamic) OrdersList.SelectedItems[0];
				_storage.Remove(selectedItem.Key);
				OrdersList.SelectedItem = null;
				EditOrderButton.IsEnabled = false;
				DeletOrderButton.IsEnabled = false;
				var orders = _storage.RetrieveAllIds();
				if (orders.Count < 1)
				{
					OrdersExplorer.IsOpen = false;
				}
				else
				{
					OrdersList.ItemsSource = orders;
				}
			}
			catch (Exception exc)
			{
				Util.Error("Order deleting error", exc.Message);
			}
		}

		private void ResetOrderInstance()
		{
			_order = new Order {Id = -1};
			DataContext = _order;
		}
	}
}

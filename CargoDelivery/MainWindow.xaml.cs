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
		private readonly Order _order;
		private readonly Validator _validator;
		private readonly OrdersStorage _storage;
		private readonly Dictionary<long, string> _ordersList;
		
		public MainWindow()
		{
			InitializeComponent();
			SelectOrderButton.IsEnabled = false;
			try
			{
				_storage = new OrdersStorage("storage.xml");
				_storage.CreateIfNotExists();
				_ordersList = _storage.RetrieveAllIds();
				_nextId = _ordersList.Count != 0 ? _ordersList.Keys.Last() + 1 : 0;
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
			_order = new Order {Id = -1};
			DataContext = _order;
		}

		private void DiscoverOrders(object sender, RoutedEventArgs e)
		{
			OrdersList.ItemsSource = _ordersList;
			OrdersExplorer.IsOpen = true;
		}
		
		private void SetTargetEditingOrder(object sender, RoutedEventArgs e)
		{
			if (OrdersList.SelectedItems.Count < 1)
			{
				return;
			}

			try
			{
				if (OrdersList.SelectedItems.Count == 1)
				{
					var selectedItem = (dynamic)OrdersList.SelectedItems[0];
					DataContext = _storage.Retrieve(selectedItem.Key);	
				}
			}
			catch (Exception exc)
			{
				Util.Error("Can't set order for editing", exc.Message);
			}

			OrdersList.SelectedItem = null;
			OrdersExplorer.IsOpen = false;
			SelectOrderButton.IsEnabled = false;
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			OrdersExplorer.IsOpen = false;
			SelectOrderButton.IsEnabled = false;
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
				}
				else
				{
					_storage.Update(_order.Id, _order);
				}
				_ordersList[_order.Id] = _order.ClientData.FirstName + " " + _order.ClientData.LastName;
				Util.Info("Cargo Delivery", "An order was created successfully!");
			}
			catch (Exception exc)
			{
				Util.Error("Order error", exc.ToString());
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
			SelectOrderButton.IsEnabled = true;
		}
	}
}

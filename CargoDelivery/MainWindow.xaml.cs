using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Effects;

using CargoDelivery.BL;
using CargoDelivery.Classes;
using log4net;

namespace CargoDelivery
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		/// Holds an id number of the next order.
		/// </summary>
		private int _nextId;

		/// <summary>
		/// Contains information of current creating/editing order.
		/// </summary>
		private Order _order;

		/// <summary>
		/// Validator instance.
		/// </summary>
		private readonly Validator _validator;

		/// <summary>
		/// Logger instance.
		/// </summary>
		private static readonly ILog Logger =
			LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Parameterless constructor of application's main window.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			EditOrderButton.IsEnabled = false;
			DeletOrderButton.IsEnabled = false;

			// TODO: retrieve last order's id and set _nextId = last order's id
			_nextId = 0;

			_validator = new Validator(
				new List<TextBox>
				{
					FirstName,
					LastName,
					Email,
					PhoneNumber,
					ClientAddressCity,
					ClientAddressStreet,
					ClientAddressBuildingNumber,
					ShopName,
					ShopAddressCity,
					ShopAddressStreet,
					ShopAddressBuildingNumber,
					GoodsCode,
					GoodsWeight
				},
				Email,
				PhoneNumber);
			ResetOrderInstance();
		}

		/// <summary>
		/// Fires when user opens pop-up window with a list of available oredrs.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void ExploreOrders(object sender, RoutedEventArgs e)
		{
			// TODO: get orders' data as Dictonary <id (long), owner (string)>
			var orders = new Dictionary<long, string>();
			if (orders.Count > 0)
			{
				OrdersList.ItemsSource = orders;
				OrdersExplorer.IsOpen = true;
				ResetOrderInstance();
				WindowMain.IsEnabled = false;
				EditOrderButton.IsEnabled = false;
				DeletOrderButton.IsEnabled = false;
				Opacity = 0.5;
				Effect = new BlurEffect();
			}
			else
			{
				Util.Info("Explore orders", "Orders database is empty!");
			}
		}

		/// <summary>
		/// Fires when user presses 'Edit' button on pop-up window.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void SetTargetEditingOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				if (OrdersList.SelectedItems.Count == 1)
				{
					var selectedItem = (dynamic)OrdersList.SelectedItems[0];
					_order = null;	// TODO: get order by id
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
			WindowMain.IsEnabled = true;
			Opacity = 1;
			Effect = null;
		}

		/// <summary>
		/// Fires when user rejects pop-up window by pressing 'Cancel' button.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void Cancel(object sender, RoutedEventArgs e)
		{
			OrdersExplorer.IsOpen = false;
			EditOrderButton.IsEnabled = false;
			DeletOrderButton.IsEnabled = false;
			WindowMain.IsEnabled = true;
			Opacity = 1;
			Effect = null;
		}

		/// <summary>
		/// Fires when user creates or updates an order.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void SaveOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				_validator.Validate();
				if (_order.Id == -1)
				{
					_order.Id = _nextId++;
					
					// TODO: save order to database
					
					ResetOrderInstance();
				}
				else
				{
					// TODO: update order in database
				}

				Util.Info("Cargo Delivery", "An order was saved successfully!");
			}
			catch (Exception exc)
			{
				Util.Error("Order error", exc.Message);
			}
		}

		/// <summary>
		/// Fires when user selects some input field.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void InputFocused(object sender, RoutedEventArgs e)
		{
			if (e.OriginalSource is TextBox textBox)
			{
				textBox.SelectAll();
			}
		}

		/// <summary>
		/// Fires when user presses on items in pop-up window's list view.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void ItemIsSelected(object sender, RoutedEventArgs e)
		{
			EditOrderButton.IsEnabled = true;
			DeletOrderButton.IsEnabled = true;
		}

		/// <summary>
		/// Fires when user resets input fields for creating new order by pressing 'New' button.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void NewOrder(object sender, RoutedEventArgs e)
		{
			ResetOrderInstance();
		}

		/// <summary>
		/// Fires when user deletes order by pressing 'Delete' button on pop-up window.
		/// </summary>
		/// <param name="sender">The button New that the action is for.</param>
		/// <param name="e">Arguments that the implementor of this event may find useful.</param>
		private void DeleteOrder(object sender, RoutedEventArgs e)
		{
			try
			{
				if (OrdersList.SelectedItems.Count != 1)
				{
					return;
				}

				var selectedItem = (dynamic)OrdersList.SelectedItems[0];
				
				// TODO: delete order by key - 'selectedItem.Key'
				
				OrdersList.SelectedItem = null;
				EditOrderButton.IsEnabled = false;
				DeletOrderButton.IsEnabled = false;

				// TODO: get orders' data as Dictonary <id (long), owner (string)>
				var orders = new Dictionary<long, string>();
				
				if (orders.Count < 1)
				{
					OrdersExplorer.IsOpen = false;
					Opacity = 1;
					Effect = null;
					WindowMain.IsEnabled = true;
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

		/// <summary>
		/// Resets _order field: set _order to new Order with id = -1.
		/// </summary>
		private void ResetOrderInstance()
		{
			_order = new Order { Id = -1 };
			DataContext = _order;
		}
	}
}

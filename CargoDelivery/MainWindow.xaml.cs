using System;
using System.Linq;
using System.Windows;
using System.Data.Common;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Effects;
using System.Data.Entity.Infrastructure;

using log4net;

using CargoDelivery.BL;
using CargoDelivery.DAL;
using CargoDelivery.Classes;

namespace CargoDelivery
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		/// Contains information of current creating/editing order.
		/// </summary>
		private Order _order;

		/// <summary>
		/// Validator instance.
		/// </summary>
		private readonly Validator _validator;

		/// <summary>
		/// Sign if new order is creating or editing an existent.
		/// </summary>
		private bool _isEditing;
		
		/// <summary>
		/// Logger instance.
		/// </summary>
		private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Unit of work instance object to manage database tables.
		/// </summary>
		private static readonly UnitOfWork UnitOfWorkInstance = new UnitOfWork();

		/// <summary>
		/// Parameterless constructor of application's main window.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			EditOrderButton.IsEnabled = false;
			DeletOrderButton.IsEnabled = false;
			_isEditing = false;
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
			try
			{
				var ordersDict = UnitOfWorkInstance.GetOrders().ToDictionary(order => order.Id,
					order => $"{order.ClientData.FirstName} {order.ClientData.LastName}");
				if (ordersDict.Count > 0)
				{
					OrdersList.ItemsSource = ordersDict;
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
					Util.Info("Explore orders", "Orders table is empty!");
				}
			}
			catch (DbException exc)
			{
				Cancel(sender, e);
				Logger.Error($"Error occurred while retrieving orders from the database.\nError: {exc}");
				Util.Error("Exploring orders error", exc.Message);
			}
			catch (Exception exc)
			{
				Cancel(sender, e);
				Logger.Error($"Unknown error occurred while exploring orders.\nError: {exc}");
				Util.Error("Exploring orders error", exc.Message);
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
					_order = UnitOfWorkInstance.Orders.GetById(selectedItem.Key);
					DataContext = _order;
				}
				
				OrdersList.SelectedItem = null;
				OrdersExplorer.IsOpen = false;
				EditOrderButton.IsEnabled = false;
				DeletOrderButton.IsEnabled = false;
				WindowMain.IsEnabled = true;
				Opacity = 1;
				Effect = null;
				_isEditing = true;
			}
			catch (Exception exc)
			{
				Logger.Error($"Error occurred while setting order for editing.\nError: {exc}");
				Util.Error("Can't set order for editing", exc.Message);
			}
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
				if (_isEditing)
				{
					UnitOfWorkInstance.Orders.Update(_order);
				}
				else
				{
					UnitOfWorkInstance.Orders.Insert(_order);	
				}
				UnitOfWorkInstance.Save();
				Util.Info("Cargo Delivery", "An order was saved successfully!");
			}
			catch (InvalidCastException exc)
			{
				Logger.Error($"Error occurred while vaidating inputs.\nError: {exc}");
				Util.Error("Order saving error", exc.Message);
			}
			catch (DbUpdateException exc)
			{
				Logger.Error($"Error occurred while updating the databse.\nError: {exc}");
				Util.Error("Order saving error", exc.Message);
			}
			catch (Exception exc)
			{
				Logger.Error($"Error occurred while order saving or updating.\nError: {exc}");
				Util.Error("Order saving error", exc.Message);
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
			_isEditing = false;
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

				var selectedItem = (dynamic) OrdersList.SelectedItems[0];
				UnitOfWorkInstance.DeleteOrder(selectedItem.Key);
				UnitOfWorkInstance.Save();
				OrdersList.SelectedItem = null;
				EditOrderButton.IsEnabled = false;
				DeletOrderButton.IsEnabled = false;
				var orders = UnitOfWorkInstance.GetOrders().ToDictionary(order => order.Id,
					order => $"{order.ClientData.FirstName} {order.ClientData.LastName}");
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
			catch (DbUpdateException exc)
			{
				Cancel(sender, e);
				Logger.Error($"Error occurred while deleting from the databse.\nError: {exc}");
				Util.Error("Order deleting error", exc.Message);
			}
			catch (Exception exc)
			{
				Cancel(sender, e);
				Logger.Error($"Error occurred while deleting an order.\nError: {exc}");
				Util.Error("Order deleting error", exc.ToString());
			}
		}

		/// <summary>
		/// Resets _order field: set _order to new Order with id = -1.
		/// </summary>
		private void ResetOrderInstance()
		{
			_order = new Order();
			DataContext = _order;
		}
	}
}

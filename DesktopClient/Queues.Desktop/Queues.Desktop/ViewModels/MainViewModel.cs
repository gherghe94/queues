using Prism.Mvvm;
using Queues.Desktop.Services;
using Queues.Desktop.Services.CustomEventArgs;
using Queues.Desktop.Services.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Queues.Desktop.ViewModels
{
    public class MainViewModel : BindableBase, IDisposable
    {
        private bool _disposed;

        private UserViewModel _selectedUser;
        private ObservableCollection<UserViewModel> _users;
        private UserReceiverService _userReceiverService;
        private OrderReceiverService _orderReceiverService;

        public UserViewModel SelectedUser
        {
            get
            {
                return _selectedUser;
            }

            set
            {
                SetProperty(ref _selectedUser, value);
            }
        }

        public ObservableCollection<UserViewModel> Users
        {
            get
            {
                return _users;
            }

            set
            {
                SetProperty(ref _users, value);
            }
        }

        public MainViewModel()
        {
            _userReceiverService = new UserReceiverService();
            _orderReceiverService = new OrderReceiverService();

            Users = new ObservableCollection<UserViewModel>();

            ListenForUsers();
            ListenForOrders();
        }

        private void ListenForOrders()
        {
            _orderReceiverService.StartListening();
            _orderReceiverService.OrderCreated += OnOrderCreated;
        }

        private void OnOrderCreated(object sender, OrderCreatedMessageEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                var foundUser = Users.FirstOrDefault(user => user.Name == e.Order.UserName);
                if (foundUser != null)
                {
                    foundUser.Orders.Add(CreateOrderViewModel(e.Order));
                }
            });
        }

        private OrderViewModel CreateOrderViewModel(OrderCreatedMessage order)
        {
            return new OrderViewModel
            {
                UserName = order.UserName,
                Products = order.Products.Select(p => CreateProductViewModel(p)).ToList()
            };
        }

        private ProductViewModel CreateProductViewModel(ProductMessage p)
        {
            return new ProductViewModel
            {
                Category = p.Category,
                Name = p.Name,
                Price = p.Price
            };
        }

        private void ListenForUsers()
        {
            _userReceiverService.StartListening();
            _userReceiverService.UserCreated += OnUserCreated;
        }

        private void OnUserCreated(object sender, UserCreatedMessageEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Users.Add(CreateUser(e.User));
            });
        }

        private UserViewModel CreateUser(UserCreatedMessage user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Name = user.Name
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _userReceiverService.Dispose();
            }

            _disposed = true;
        }
    }
}

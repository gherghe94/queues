using Prism.Mvvm;
using Queues.Desktop.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Queues.Desktop.ViewModels
{
    public class MainViewModel : BindableBase, IDisposable
    {
        private bool _disposed;

        private UserViewModel _selectedUser;
        private ObservableCollection<UserViewModel> _users;
        private UserReceiverService _userReceiverService;

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
            Users = new ObservableCollection<UserViewModel>();

            ListenForUsers();
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

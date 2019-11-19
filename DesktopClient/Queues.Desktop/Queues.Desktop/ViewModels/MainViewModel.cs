using Prism.Mvvm;
using Queues.Desktop.Services;
using System.Collections.ObjectModel;

namespace Queues.Desktop.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private UserViewModel _selectedUser;
        private ObservableCollection<UserViewModel> _users;
        private NatsService _natsService;

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
            Users = new ObservableCollection<UserViewModel>();

            _natsService = new NatsService();
            _natsService.StartListening();
            _natsService.UserCreated += OnUserCreated;
        }

        private void OnUserCreated(object sender, UserCreatedMessageEventArgs e)
        {
            App.Current.Dispatcher.Invoke(delegate
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
    }
}

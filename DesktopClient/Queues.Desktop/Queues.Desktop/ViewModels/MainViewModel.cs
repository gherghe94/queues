using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Queues.Desktop.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private UserViewModel _selectedUser;
        private ObservableCollection<UserViewModel> _users;

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
            Users = new ObservableCollection<UserViewModel>
            {
                new UserViewModel { Name = " Test ", Email = "test@gmail.com"}
            };
        }
    }
}

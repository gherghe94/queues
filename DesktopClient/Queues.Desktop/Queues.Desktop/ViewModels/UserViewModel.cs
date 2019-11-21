using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Queues.Desktop.ViewModels
{
    public class UserViewModel : BindableBase
    {
        private string _name;
        private string _email;

        private ObservableCollection<OrderViewModel> _orders;

        public UserViewModel()
        {
            Orders = new ObservableCollection<OrderViewModel>();
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                SetProperty(ref _email, value);
            }
        }

        public ObservableCollection<OrderViewModel> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                SetProperty(ref _orders, value);
            }
        }

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}
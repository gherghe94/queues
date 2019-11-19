using Prism.Mvvm;

namespace Queues.Desktop.ViewModels
{
    public class UserViewModel : BindableBase
    {
        private string _name;
        private string _email;

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

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}
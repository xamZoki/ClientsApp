using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApp5.ViewModels
{
    public class ClientViewModel : BaseViewModel, IDataErrorInfo
    {
        private string _name;
        private DateTime _birthday;
        private string _homeAddress;
        private ObservableCollection<Client> _clients;


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public string HomeAddress
        {
            get { return _homeAddress; }
            set
            {
                _homeAddress = value;
                OnPropertyChanged("HomeAddress");
            }
        }

        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public ICommand AddClientCommand { get; private set; }

        public string Error => throw new NotImplementedException();

        public ClientViewModel()
        {
            AddClientCommand = new RelayCommand(AddClient, CanAddClient);
            Clients = new ObservableCollection<Client>();
        }
        private void AddClient()
        {
            Clients.Add(new Client
            {
                Name = Name,
                Birthday = Birthday,
                HomeAddress = HomeAddress
            });

            ClearFields();
        }

        private bool CanAddClient()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                Birthday != DateTime.MinValue &&
                !string.IsNullOrWhiteSpace(HomeAddress);
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Birthday = DateTime.MinValue;
            HomeAddress = string.Empty;
        }
  

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                            error = "Name is required.";
                        break;

                    case "Birthday":
                        if (Birthday == DateTime.MinValue)
                            error = "Birthday is required.";
                        break;

                    case "HomeAddress":
                        if (string.IsNullOrWhiteSpace(HomeAddress))
                            error = "Home Address is required.";
                        break;
                }

                return error;
            }
        }









    }

}

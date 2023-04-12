using ClientsApp.Interfaces;
using Microsoft.Win32;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ClientsApp.ViewModels
{
    public class ClientsViewModel: BaseViewModel, IDataErrorInfo
    {
        // Services
        private IDataImportService _dataImportService;
        private IDataExportService _dataExportService;
        private IClientRepoService _clientRepoService;
        private IDataAddItemService _dataAddItemService;


        private string _name;
        private DateTime? _birthday;
        private string _homeAddress;
        private string _weekendAddress;
        private string _sourceText;
        private ObservableCollection<Models.Client> clients;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string HomeAddress
        {
            get { return _homeAddress; }
            set
            {
                _homeAddress = value;
                OnPropertyChanged();
            }
        }

        public string WeekendAddress
        {
            get { return _weekendAddress; }
            set
            {
                _weekendAddress = value;
                OnPropertyChanged();
            }
        }
        public string SourceText
        {
            get { return _sourceText; }
            set
            {
                _sourceText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Models.Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
        public ICommand AddClientCommand { get; }
        public ICommand OrderClientsCommand => new Command<string>(async (name) =>
        {
            if (name == "Name")
            {
                Clients = new ObservableCollection<Models.Client>(Clients.OrderBy(x => x.Name));
            }
            else if (name == "Birthday")
            {
                Clients = new ObservableCollection<Models.Client>(Clients.OrderBy(x => x.Birthday));
            }
            else if (name == "ID")
            {
                Clients = new ObservableCollection<Models.Client>(Clients.OrderBy(x => x.ID));
            }
        });
        public ICommand ExportToJsonCommand => new Command(async () =>
        {
            var isSuccess =  _dataExportService.ExportToJson(Clients.ToList());
            if (isSuccess)
            {
                MessageBox.Show("Sucessafully saved to local json file");
            }
            else MessageBox.Show("Unsucessafully try to saved to local json file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        });
        public ICommand AddXmlCommand => new Command(
            () => 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    SourceText = File.ReadAllText(openFileDialog.FileName);

                
                var isValidFile = AddlClientsFromXml(openFileDialog.FileName);
                if (!isValidFile)
                {
                    SourceText = "not valid structure";
                }
            });

        public ClientsViewModel(IDataExportService dataExportService, IDataImportService dataImportService, IClientRepoService clientRepoService, IDataAddItemService dataAddItemService)
        {
            _dataExportService = dataExportService;
            _dataImportService = dataImportService;
            _clientRepoService = clientRepoService;
            _dataAddItemService = dataAddItemService;
            Clients = new ObservableRangeCollection<Models.Client>();
            AddClientCommand = new RelayCommand(AddClient, CanAdd);
            GetAllClientsOnStart().GetAwaiter().GetResult();

        }

        public async Task GetAllClientsOnStart()
        {
            foreach (var item in await _clientRepoService.GetAll())
            {
                Clients.Add(item);
            }
        }

        private void AddClient()
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                {

                    Models.Client client = new Models.Client(0, Name, Birthday.Value, HomeAddress, WeekendAddress);
                    _dataAddItemService.AddItem(client);
                    Clients.Clear();
                    foreach (var item in _clientRepoService.GetAll().Result)
                    {
                        Clients.Add(item);
                    }
                    ClearFields();
                });
         
             
        }
        private bool CanAdd()
        {
            return !string.IsNullOrEmpty(Name) && Birthday.HasValue && !string.IsNullOrEmpty(HomeAddress) && IsValidBirthday();
        }
        private bool IsValidBirthday()
        {
            if (Birthday.HasValue)
            {
                DateTime now = DateTime.Now;
                if (Birthday.Value > now.AddYears(-1))
                    return false;
            }
            return true;
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Birthday = null;
            HomeAddress = string.Empty;
            WeekendAddress = string.Empty;
        }
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                            error = "Name is required.";
                        break;
                    case nameof(Birthday):
                        if (!Birthday.HasValue)
                            error = "Birthday is required.";
                        else if (!IsValidBirthday())
                            error = "Birthday must be a past date.";
                        break;
                    case nameof(HomeAddress):
                        if (string.IsNullOrEmpty(HomeAddress))
                            error = "Home Address is required.";
                        break;
                }
                return error;
            }
        }

        public bool AddlClientsFromXml(string xml)
        {
          
            Clients.Clear();
            var clients = _dataImportService.ImportFromXml(xml);
            if (clients.Any())
            {
                foreach (var item in clients)
                {
                    _dataAddItemService.AddItem(item);
                }
                foreach (var item in _clientRepoService.GetAll().Result)
                {
                    Clients.Add(item);
                }
                return true;
            }
            else return false;
        }
    }
}

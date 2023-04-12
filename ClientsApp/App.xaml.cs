using ClientsApp.Interfaces;
using ClientsApp.Services;
using ClientsApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ClientsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IDataExportService, DataExportService>();
                    services.AddSingleton<IDataImportService, DataImportService>();
                    services.AddSingleton<IDataGetAllService, DataGetAllService>();
                    services.AddSingleton<IDataAddItemService, DataAddItemService>();
                    services.AddSingleton<ClientsViewModel>(x => new ClientsViewModel
                    (x.GetRequiredService<IDataExportService>(), 
                     x.GetRequiredService<IDataImportService>(), 
                     x.GetRequiredService<IDataGetAllService>(), 
                     x.GetRequiredService<IDataAddItemService>()));
                    services.AddSingleton<MainWindow>((z) => new MainWindow{ DataContext = z.GetRequiredService<ClientsViewModel>()});

                })
                .Build();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppHost!.Dispose();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startUpForm = AppHost.Services.GetRequiredService<MainWindow>();
            //startUpForm.DataContext = AppHost.Services.GetRequiredService<ClientsViewModel>();
            startUpForm.Show();
            
            base.OnStartup(e);
        }
        
    }
}
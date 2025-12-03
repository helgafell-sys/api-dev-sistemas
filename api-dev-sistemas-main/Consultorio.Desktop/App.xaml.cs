using System.Windows;
using Consultorio.Desktop.ViewModels;
using Consultorio.Desktop.Views;
using Consultorio.Desktop.Services;

namespace Consultorio.Desktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var apiUrl = Environment.GetEnvironmentVariable("CONSULTORIO_API_URL") ?? "http://localhost:5000";
        var apiClient = new ApiClient(apiUrl);
        var viewModel = new PatientsViewModel(apiClient);
        
        var mainWindow = new MainWindow { DataContext = viewModel };
        mainWindow.Show();
    }
}
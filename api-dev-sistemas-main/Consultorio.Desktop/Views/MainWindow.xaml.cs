using System.Windows;
using Consultorio.Desktop.ViewModels;

namespace Consultorio.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is PatientsViewModel vm)
        {
            await vm.LoadPatientsCommand.ExecuteAsync(null);
        }
    }
}
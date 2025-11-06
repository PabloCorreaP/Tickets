using PracticaDIA.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;


namespace PracticaDIA.UI.Views;

//Partial indica que la clase puede estar dividida en varios archivos.
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void GestionPersonal_Click(object? sender, RoutedEventArgs e)
    {
        var personalWindow = new PersonalView();
        if (DataContext is MainWindowViewModel vm)
        {
            personalWindow.DataContext = vm.PersonalViewModel;
        }

        await personalWindow.ShowDialog(this);
    }
}
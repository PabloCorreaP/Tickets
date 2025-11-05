using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace App;

public partial class ResultadoBusqueda : Window
{
    public ResultadoBusqueda(string[] tickets)
    {
        InitializeComponent();
        var btCancel = this.GetControl<Button>("CancelButton");
        var failTicketsTB = this.GetControl<TextBlock>("FailTickets");
        btCancel.Click += (_, _) => this.OnExit();
        if (tickets.Length != 0)
        {
            ResultadoLB.IsVisible = true;
            failTicketsTB.IsVisible = false;
            ResultadoLB.ItemsSource = tickets;
        }
        else
        {
            ResultadoLB.IsVisible = false;
            failTicketsTB.IsVisible = true;
        }
        
        
    }
    private void OnExit()
    {
        this.Close();
    }
}
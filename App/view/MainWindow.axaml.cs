
using Avalonia.Controls;
using Avalonia.Interactivity;


namespace TicketStats.View
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Refrescar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is StatisticsViewModel vm)
                vm.Recalcular(); 
        }
    }


}


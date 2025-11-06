using Avalonia.Controls;
using Avalonia.Interactivity;
using PracticaDIA.UI.ViewModels;
using PracticaDIA.UI.Views;
using System.Threading.Tasks;

namespace PracticaDIA.UI.Views
{
    public partial class PersonalView : Window
    {
        public PersonalView()
        {
            InitializeComponent();
        }

        private async void Nuevo_Click(object? sender, RoutedEventArgs e)
        {
            var dlg = new AddPersonalDialog();
            await dlg.ShowDialog(this);
            if (!dlg.IsCancelled && DataContext is PersonalViewModel vm)
            {
                vm.Agregar(dlg.Trabajador);
            }
        }

        private async void Editar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is PersonalViewModel vm && vm.Seleccionado != null)
            {
                var copia = new PracticaDIA.UI.Core.Personal.Trabajador(vm.Seleccionado.DNI, vm.Seleccionado.Nombre, vm.Seleccionado.Email);
                copia.Tickets = new System.Collections.Generic.List<PracticaDIA.UI.Core.Personal.Ticket>(vm.Seleccionado.Tickets);
                var dlg = new AddPersonalDialog(copia);
                await dlg.ShowDialog(this);
                if (!dlg.IsCancelled)
                {
                    vm.Actualizar(dlg.Trabajador);
                }
            }
        }

        private void Eliminar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is PersonalViewModel vm)
            {
                vm.EliminarSeleccionado();
            }
        }

        private void Guardar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is PersonalViewModel vm)
            {
                vm.Guardar();
            }
        }

        private void Cerrar_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

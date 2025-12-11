using App.Core;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PracticaDIA.UI.Views
{
    public partial class AddPersonalDialog : Window
    {
        public Trabajador Trabajador { get; private set; }
        public bool IsCancelled { get; private set; } = true;

        public AddPersonalDialog()
        {
            InitializeComponent();
            Trabajador = new Trabajador();
        }

        public AddPersonalDialog(Trabajador t) : this()
        {
            Trabajador = t;
            TxtDNI.Text = t.DNI;
            TxtDNI.IsEnabled = false; // no permitimos cambiar DNI en edición
            TxtNombre.Text = t.Nombre;
            TxtEmail.Text = t.Email;
        }

        private void Aceptar_Click(object? sender, RoutedEventArgs e)
        {
            Trabajador.DNI = TxtDNI.Text ?? string.Empty;
            Trabajador.Nombre = TxtNombre.Text ?? string.Empty;
            Trabajador.Email = TxtEmail.Text ?? string.Empty;
            IsCancelled = false;
            Close();
        }

        private void Cancelar_Click(object? sender, RoutedEventArgs e)
        {
            IsCancelled = true;
            Close();
        }
    }
}
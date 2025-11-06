using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PracticaDIA.UI.Core.Personal;
using PracticaDIA.UI.Services;

namespace PracticaDIA.UI.ViewModels
{
    public class PersonalViewModel : INotifyPropertyChanged
    {
        private RegistroPersonal _registro;
        private ObservableCollection<Trabajador> _trabajadores;
        private Trabajador? _seleccionado;

        public PersonalViewModel()
        {
            _registro = new RegistroPersonal();
            _trabajadores = new ObservableCollection<Trabajador>(_registro.ObtenerTrabajadores());
        }

        public ObservableCollection<Trabajador> Trabajadores
        {
            get => _trabajadores;
            set { _trabajadores = value; OnPropertyChanged(); }
        }

        public Trabajador? Seleccionado
        {
            get => _seleccionado;
            set { _seleccionado = value; OnPropertyChanged(); }
        }

        public void Agregar(Trabajador t)
        {
            _registro.AgregarTrabajador(t);
            Refrescar();
        }

        public void EliminarSeleccionado()
        {
            if (Seleccionado != null)
            {
                _registro.EliminarTrabajador(Seleccionado.DNI);
                Refrescar();
                Seleccionado = null;
            }
        }

        public void Actualizar(Trabajador t)
        {
            _registro.ActualizarTrabajador(t);
            Refrescar();
        }

        public void Guardar()
        {
            _registro.Guardar();
        }

        public int ContarTicketsPorEstado(TicketEstado estado)
        {
            if (Seleccionado == null) return 0;
            return _registro.ObtenerTicketsPorEstado(Seleccionado.DNI, estado).Count;
        }

        public void Refrescar()
        {
            Trabajadores.Clear();
            foreach (var t in _registro.ObtenerTrabajadores())
                Trabajadores.Add(t);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

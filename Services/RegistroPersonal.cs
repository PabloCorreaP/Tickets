using System;
using System.Collections.Generic;
using System.Linq;
using PracticaDIA.UI.Core.Personal;
using PracticaDIA.UI.Data;

namespace PracticaDIA.UI.Services
{
    public class RegistroPersonal
    {
        private List<Trabajador> _trabajadores;

        public RegistroPersonal()
        {
            _trabajadores = PersistenciaPersonal.CargarPersonal();
        }

        public List<Trabajador> ObtenerTrabajadores()
        {
            return new List<Trabajador>(_trabajadores);
        }

        public void AgregarTrabajador(Trabajador t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (_trabajadores.Exists(x => x.DNI == t.DNI))
                throw new InvalidOperationException("Ya existe un trabajador con ese DNI.");

            _trabajadores.Add(t);
        }

        public bool EliminarTrabajador(string dni)
        {
            var t = _trabajadores.Find(x => x.DNI == dni);
            if (t == null) return false;
            return _trabajadores.Remove(t);
        }

        public bool ActualizarTrabajador(Trabajador actualizado)
        {
            var idx = _trabajadores.FindIndex(x => x.DNI == actualizado.DNI);
            if (idx < 0) return false;
            _trabajadores[idx].Nombre = actualizado.Nombre;
            _trabajadores[idx].Email = actualizado.Email;
            _trabajadores[idx].Tickets = actualizado.Tickets ?? new List<Ticket>();
            return true;
        }

        public void Guardar()
        {
            PersistenciaPersonal.GuardarPersonal(_trabajadores);
        }

        public List<Ticket> ObtenerTicketsAsignados(string dni)
        {
            var t = _trabajadores.Find(x => x.DNI == dni);
            if (t == null) return new List<Ticket>();
            return new List<Ticket>(t.Tickets);
        }

        public List<Ticket> ObtenerTicketsPorEstado(string dni, TicketEstado estado)
        {
            return ObtenerTicketsAsignados(dni).Where(tt => tt.Estado == estado).ToList();
        }
    }
}

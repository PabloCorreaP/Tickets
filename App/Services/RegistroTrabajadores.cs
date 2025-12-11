using System;
using System.Collections.Generic;
using System.Linq;
using App.Core;
using App.Data;

namespace App.Services
{
    public static class RegistroTrabajadores
    {
        private static List<Trabajador> _trabajadores = new List<Trabajador>();
        public static void Init()
        {
            _trabajadores = PersistenciaTrabajadores.CargarTrabajadores();
        }

        public static List<Trabajador> ObtenerTrabajadores()
        {
            return new List<Trabajador>(_trabajadores);
        }

        public static Trabajador? ObtenerTrabajadorPorDNI(string DNI)
        {
            return _trabajadores.First(t => t.DNI.Equals(DNI));
        }

        public static void AgregarTrabajador(Trabajador t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (_trabajadores.Exists(x => x.DNI == t.DNI))
                throw new InvalidOperationException("Ya existe un trabajador con ese DNI.");

            _trabajadores.Add(t);
        }

        public static bool EliminarTrabajador(string dni)
        {
            var t = _trabajadores.Find(x => x.DNI == dni);
            if (t == null) return false;
            return _trabajadores.Remove(t);
        }

        public static bool ActualizarTrabajador(Trabajador actualizado)
        {
            var idx = _trabajadores.FindIndex(x => x.DNI == actualizado.DNI);
            if (idx < 0) return false;
            _trabajadores[idx].Nombre = actualizado.Nombre;
            _trabajadores[idx].Email = actualizado.Email;
            _trabajadores[idx].Tickets = actualizado.Tickets ?? new List<Ticket>();
            return true;
        }

        public static void Guardar()
        {
            PersistenciaTrabajadores.GuardarTrabajadores(_trabajadores);
        }

        public static List<Ticket> ObtenerTicketsAsignados(string dni)
        {
            var t = _trabajadores.Find(x => x.DNI == dni);
            if (t == null) return new List<Ticket>();
            return new List<Ticket>(t.Tickets);
        }

        public static List<Ticket> ObtenerTicketsPorEstado(string dni, Ticket.Estado estado)
        {
            return ObtenerTicketsAsignados(dni).Where(tt => tt.Resultado == estado).ToList();
        }
    }
}
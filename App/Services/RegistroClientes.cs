using System;
using System.Collections.Generic;
using System.Linq;
using App.core;
using App.Core;
using App.Data;

namespace App.Services
{
    public static class RegistroClientes
    {
        private static List<Cliente> _clientes = new List<Cliente>();
        public static void Init()
        {
            _clientes = PersistenciaClientes.CargarClientes();
        }

        public static List<Cliente> ObtenerClientes()
        {
            return new List<Cliente>(_clientes);
        }

        public static Cliente? ObtenerClientePorDNI(string DNI)
        {
            return _clientes.First(t => t.DNI.Equals(DNI));
        }

        public static void AgregarCliente(Cliente t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (_clientes.Exists(x => x.DNI == t.DNI))
                throw new InvalidOperationException("Ya existe un trabajador con ese DNI.");

            _clientes.Add(t);
        }

        public static bool EliminarCliente(string dni)
        {
            var t = _clientes.Find(x => x.DNI == dni);
            if (t == null) return false;
            return _clientes.Remove(t);
        }

        public static bool ActualizarCliente(Trabajador actualizado)
        {
            var idx = _clientes.FindIndex(x => x.DNI == actualizado.DNI);
            if (idx < 0) return false;
            _clientes[idx].Nombre = actualizado.Nombre;
            _clientes[idx].Email = actualizado.Email;
            return true;
        }

        public static void Guardar()
        {
            PersistenciaClientes.GuardarClientes(_clientes);
        }

        // public static List<Ticket> ObtenerTicketsAsignados(string dni)
        // {
        //     var t = _clientes.Find(x => x.DNI == dni);
        //     if (t == null) return new List<Ticket>();
        //     return new List<Ticket>(t.Tickets);
        // }

        // public static List<Ticket> ObtenerTicketsPorEstado(string dni, Ticket.Estado estado)
        // {
        //     return ObtenerTicketsAsignados(dni).Where(tt => tt.Resultado == estado).ToList();
        // }
    }
}
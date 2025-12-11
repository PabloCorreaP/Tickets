using System;
using System.Collections.Generic;
using System.Linq;
using App.Core;
using App.Data;

namespace App.Services
{
    public static class RegistroTickets
    {
        private static List<Ticket> _tickets = new List<Ticket>();
        public static void Init()
        {
            _tickets = PersistenciaTickets.CargarTickets();
        }

        public static List<Ticket> ObtenerTickets()
        {
            return new List<Ticket>(_tickets);
        }

        public static void AgregarTicket(Ticket t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (_tickets.Exists(x => x.Id == t.Id))
                throw new InvalidOperationException("Ya existe un ticket con esa ID.");

            _tickets.Add(t);
        }

        public static bool EliminarTicket(string id)
        {
            var t = _tickets.Find(x => x.Id.ToString() == id);
            if (t == null) return false;
            return _tickets.Remove(t);
        }

        public static bool ActualizarTicket(Ticket actualizado)
        {
            var idx = _tickets.FindIndex(x => x.Id == actualizado.Id);
            if (idx < 0) return false;
            _tickets[idx].Encargado = actualizado.Encargado;
            _tickets[idx].Notas = actualizado.Notas;
            _tickets[idx].Cerrado = actualizado.Cerrado;
            _tickets[idx].Resultado = actualizado.Resultado;
            return true;
        }

        public static void Guardar()
        {
            PersistenciaTickets.GuardarTickets(_tickets);
        }

        public static List<Ticket> ObtenerTicketsPorEstado(Ticket.Estado estado)
        {
            return _tickets.Where(tt => tt.Resultado == estado).ToList();
        }
    }
}
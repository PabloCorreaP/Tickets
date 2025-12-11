using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using App.Core;

namespace App.Data
{
    public static class PersistenciaTickets
    {
        private const string ARCHIVO_TICKET = "ticket.xml";
        private const string ETIQUETA_TICKETS = "tickets";

        public static void GuardarTickets(List<Ticket> tickets)
        {
            try
            {
                XElement ticketsElement = new(ETIQUETA_TICKETS);
                foreach (Ticket ticket in tickets)
                    ticketsElement.Add(ticket.ToXML());
                ticketsElement.Save(ARCHIVO_TICKET);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar ticket: {ex.Message}");
            }
        }

        public static List<Ticket> CargarTickets()
        {
            var lista = new List<Ticket>();

            if (!File.Exists(ARCHIVO_TICKET))
            {
                Console.WriteLine("No se encontró fichero de tickets.");
                return lista;
            }

            try
            {
                XElement tickets = XDocument.Load(ARCHIVO_TICKET).Descendants(ETIQUETA_TICKETS).FirstOrDefault(new XElement(ETIQUETA_TICKETS));
                foreach (Ticket? p in tickets.Descendants(Ticket.ETIQUETA_TICKET).Select(nodo => Ticket.FromXML(nodo)))
                {
                    if (p != null)
                    {
                        lista.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar ticket: {ex.Message}");
            }

            return lista;
        }
    }
}
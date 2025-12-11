using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ticketsIndividual.Core
{
    class Busquedas
    {
        ReadOnlyCollection<Ticket> tickets;

        public Busquedas(ReadOnlyCollection<Ticket> tickets1)
        {
            this.tickets = tickets1;
        }

        public List<String> SearchCliente(string nombre)
        {
            return (from ticket in this.tickets
                   where ticket.cliente.nombre.Equals(nombre)
                   orderby ticket.asunto
                   select ticket.asunto).ToList();
        }
        public List<String> SearchPersonal(string nombre)
        {
            return (from ticket in this.tickets
                   where ticket.encargado.nombre.Equals(nombre)
                   orderby ticket.asunto
                   select ticket.asunto).ToList();
        }
    }
}

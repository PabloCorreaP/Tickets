using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ticketsIndividual.Core
{
    class Busquedas
    {
        ReadOnlyCollection<Ticket> tickets;

        public Busquedas(ReadOnlyCollection<Ticket> tickets1)
        {
            this.tickets = tickets1;
        }

        public String[] Search(string encargado, string cliente)
        {
            var toret =  from ticket in this.tickets
                   where ticket.cliente.nombre.Equals(cliente)
                   && ticket.encargado.nombre.Equals(encargado)
                   orderby ticket.asunto
                   select ticket.asunto;

            return toret.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketsIndividual.Core
{
    class Tickets
    {
        List<Ticket> tickets;
        public Tickets()
        {
            this.tickets = new List<Ticket>();
        }

        public void Add(Ticket t)
        {
            this.tickets.Add(t);
        }

        public ReadOnlyCollection<Ticket> GetTickets()
        {
            return this.tickets.AsReadOnly();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketsIndividual.Core
{
    class Ticket
    {
        public required Personal encargado { get; init; }
        public required Cliente cliente { get; init; }
        public required string asunto{ get; init; }

        public override string ToString()
        {
            return $"Trabajador encargado: {encargado.nombre} " +
                $"\nCliente origen: {cliente.nombre}";
        }
    }
}

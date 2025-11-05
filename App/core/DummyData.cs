using System.Collections.Generic;
using TicketStats.Core;

namespace TicketStats.Core
{
    public static class DummyData
    {
     

        public static List<Ticket> Tickets()
        {

            return new List<Ticket>
            {
                new Ticket(1, new Personal("111A", "Ana Técnico", "ana@acme.com"), new Cliente("C-01", "Café Sol", "contacto@cafesol.es"), "PC no enciende", "Revisar PSU",
                           Resultado.EnTramite, Estado.Abierto),
                new Ticket(2, new Personal("222B", "Bruno Soporte", "bruno@acme.com"), new Cliente("C-02", "Ferretería Paco", "info@ferrepaco.es"), "Impresora atascada", "Limpiar rodillos",
                           Resultado.Solucionado, Estado.Cerrado),
                new Ticket(3, new Personal("333C", "Carla Field", "carla@acme.com"), new Cliente("C-03", "Librería Nube", "hola@nube.es"), "Monitor parpadea", "Cable suelto",
                           Resultado.Imposible, Estado.Cerrado)
            };
        }
    }
}

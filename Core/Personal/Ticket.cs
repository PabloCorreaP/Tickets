using System;

namespace PracticaDIA.UI.Core.Personal
{
    public enum TicketEstado
    {
        Abierto,
        Cerrado
    }

    public enum ResultadoTicket
    {
        EnTramite,
        Imposible,
        Solucionado
    }

    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Asunto { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public TicketEstado Estado { get; set; } = TicketEstado.Abierto;
        public ResultadoTicket Resultado { get; set; } = ResultadoTicket.EnTramite;
        public string Notas { get; set; } = string.Empty;

        public Ticket() { }

        public Ticket(string asunto, string cliente)
        {
            Asunto = asunto;
            Cliente = cliente;
        }

        public override string ToString()
        {
            return $"[{Estado}] {Asunto} - {Cliente} ({Resultado})";
        }
    }
}

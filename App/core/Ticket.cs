namespace TicketStats.Core
{
    public enum Resultado { EnTramite, Imposible, Solucionado }
    public enum Estado { Abierto, Cerrado }

    public sealed class Ticket
    {
        public int Id { get; init; }
        public Personal Trabajador { get; set; }
        public Cliente Cliente { get; set; }
        public string Asunto { get; set; }
        public string Notas { get; set; }
        public Resultado Resultado { get; set; }
        public Estado Estado { get; set; }

        public Ticket(int id, Personal trabajador, Cliente cliente, string asunto,
                      string notas, Resultado resultado, Estado estado)
        {
            Id = id;
            Trabajador = trabajador;
            Cliente = cliente;
            Asunto = asunto;
            Notas = notas;
            Resultado = resultado;
            Estado = estado;
        }
    }
}

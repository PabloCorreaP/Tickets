namespace TicketStats.Core
{
    public sealed class Cliente : Usuario
    {
        public Cliente() { }
        public Cliente(string dni, string nombre, string email) 
            : base(dni, nombre, email) { }
    }
}

namespace TicketStats.Core
{
    public sealed class Personal : Usuario
    {
        public Personal() { }
        public Personal(string dni, string nombre, string email) 
            : base(dni, nombre, email) { }
    }
}

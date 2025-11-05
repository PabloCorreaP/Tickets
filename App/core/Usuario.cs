namespace TicketStats.Core
{
    public abstract class Usuario
    {
        public string Dni { get; init; }="";
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        protected Usuario() { }
        protected Usuario(string dni, string nombre, string email)
        {
            Dni = dni;
            Nombre = nombre;
            Email = email;
        }
        public override string ToString() => $"{Nombre} ({Dni})";
    }
}

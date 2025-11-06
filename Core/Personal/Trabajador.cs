using System;
using System.Collections.Generic;

namespace PracticaDIA.UI.Core.Personal
{
    public class Trabajador
    {
        public string DNI { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Lista de tickets asignados a este trabajador
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Trabajador() { }

        public Trabajador(string dni, string nombre, string email)
        {
            DNI = dni;
            Nombre = nombre;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Nombre} ({DNI})";
        }
    }
}

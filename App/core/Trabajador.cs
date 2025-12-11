using System;
using System.Collections.Generic;
using System.Xml.Linq;
using App.core;

namespace App.Core
{
    public class Trabajador : Usuario
    {
        public const string TIPO = "Trabajador";

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
        public override XElement ToXML()
        {
            XElement element = base.ToXML();
            element.SetAttributeValue(Usuario.ETIQUETA_TIPO, TIPO);
            return element;
        }

        public static new Trabajador? FromXML(XElement element)
        {
            string? dni = element.Attribute(ETIQUETA_DNI)?.Value;
            string? nombre = element.Attribute(ETIQUETA_NOMBRE)?.Value;
            string? email = element.Attribute(ETIQUETA_EMAIL)?.Value;

            if (dni == null || nombre == null || email == null)
                return null;

            if (!Utils.DNIValido(dni) || !Utils.EmailValido(email))
                return null;

            return new Trabajador
            {
                DNI = dni,
                Nombre = nombre,
                Email = email,
            };
        }
    }
}
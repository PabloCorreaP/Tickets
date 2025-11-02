using System.Collections.Generic;
using System.Xml.Linq;

namespace App.core;

public class Personal : Usuario
{
    public const string TIPO = "Personal";

    // No se guarda en el XML, se puede construir a partir de la información de
    // los tickets (sería una dependencia circular en el XML si sí se representase)
    public IList<Ticket> TicketsAsignados { get; init; }
    public Personal()
    {
        this.TicketsAsignados = [];
    }
    public override XElement ToXML()
    {
        XElement element = base.ToXML();
        element.SetAttributeValue(Usuario.ETIQUETA_TIPO, TIPO);
        return element;
    }

    public static new Personal? FromXML(XElement element)
    {
        string? dni = element.Attribute(ETIQUETA_DNI)?.Value;
        string? nombre = element.Attribute(ETIQUETA_NOMBRE)?.Value;
        string? email = element.Attribute(ETIQUETA_EMAIL)?.Value;

        if (dni == null || nombre == null || email == null)
            return null;

        if (!Utils.DNIValido(dni) || !Utils.EmailValido(email))
            return null;

        return new Personal
        {
            DNI = dni,
            Nombre = nombre,
            Email = email,
            TicketsAsignados = [] // todo
        };
    }

    public override string ToString()
    {
        return this.ToXML().ToString();
    }
}
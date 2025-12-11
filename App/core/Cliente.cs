using System.Xml.Linq;

namespace App.core;

public class Cliente : Usuario
{
    public const string TIPO = "Cliente";

    public override XElement ToXML()
    {
        XElement element = base.ToXML();
        element.SetAttributeValue(Usuario.ETIQUETA_TIPO, TIPO);
        return element;
    }

    public static new Cliente? FromXML(XElement element)
    {
        string? dni = element.Attribute(ETIQUETA_DNI)?.Value;
        string? nombre = element.Attribute(ETIQUETA_NOMBRE)?.Value;
        string? email = element.Attribute(ETIQUETA_EMAIL)?.Value;

        if (dni == null || nombre == null || email == null)
            return null;

        if (!Utils.DNIValido(dni) || !Utils.EmailValido(email))
            return null;

        return new Cliente
        {
            DNI = dni,
            Nombre = nombre,
            Email = email
        };
    }

    public override string ToString()
    {
        return this.ToXML().ToString();
    }
}
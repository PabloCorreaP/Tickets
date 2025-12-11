using System.Xml.Linq;
using App.Core;

namespace App.core;

public abstract class Usuario
{
    public required string DNI { get; init; }
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    public const string ETIQUETA_USUARIO = "usuario";
    protected const string ETIQUETA_DNI = "dni";
    protected const string ETIQUETA_NOMBRE = "nombre";
    protected const string ETIQUETA_EMAIL = "email";
    protected const string ETIQUETA_TIPO = "tipo";

    public static Usuario? FromXML(XElement element)
    {
        string? tipo = element.Attribute(ETIQUETA_TIPO)?.Value;
        if (tipo == null)
            return null;
        return tipo switch
        {
            Cliente.TIPO => Cliente.FromXML(element),
            Trabajador.TIPO => Trabajador.FromXML(element),
            _ => null,
        };
    }

    public virtual XElement ToXML()
    {
        return new(ETIQUETA_USUARIO,
            new XAttribute(ETIQUETA_DNI, this.DNI),
            new XAttribute(ETIQUETA_NOMBRE, this.Nombre),
            new XAttribute(ETIQUETA_EMAIL, this.Email));
    }

    public override string ToString()
    {
        return this.ToXML().ToString();
    }
}
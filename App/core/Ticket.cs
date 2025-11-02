using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace App.core;

public class Ticket
{
    public required Personal Encargado { get; set; }
    public required Cliente Cliente { get; set; }
    public string? Asunto { get; set; }
    public IList<string> Notas { get; set; }
    public required Resultado Resultado { get; set; }
    public required Estado Estado { get; set; }
    public const string ETIQUETA_TICKET = "ticket";
    protected const string ETIQUETA_ENCARGADO = "encargado";
    protected const string ETIQUETA_CLIENTE = "cliente";
    protected const string ETIQUETA_ASUNTO = "asunto";
    protected const string ETIQUETA_RESULTADO = "resultado";
    protected const string ETIQUETA_ESTADO = "estado";
    protected const string ETIQUETA_NOTA = "nota";

    public Ticket()
    {
        this.Notas = [];
    }

    public static Ticket? FromXML(XElement node)
    {
        string? encargadoDNI = node.Attribute(ETIQUETA_ENCARGADO)?.Value;
        string? clienteDNI = node.Attribute(ETIQUETA_CLIENTE)?.Value;
        string? resultadoStr = node.Attribute(ETIQUETA_RESULTADO)?.Value;
        string? estadoStr = node.Attribute(ETIQUETA_ESTADO)?.Value;
        string? asunto = node.Attribute(ETIQUETA_ASUNTO)?.Value;
        if (encargadoDNI == null || clienteDNI == null || !Enum.TryParse(resultadoStr, out Resultado resultado) ||
            !Enum.TryParse(estadoStr, out Estado estado))
        {
            return null;
        }
        Personal? encargado = ContenedorDatos.GetContenedorDatos().GetPersonal(encargadoDNI);
        Cliente? cliente = ContenedorDatos.GetContenedorDatos().GetCliente(clienteDNI);
        if (encargado == null || cliente == null)
            return null;
        IList<string> notas = [];
        foreach (XElement nota in node.Descendants(ETIQUETA_NOTA))
            notas.Add(nota.Value);
        return new Ticket
        {
            Encargado = encargado,
            Cliente = cliente,
            Resultado = resultado,
            Estado = estado,
            Asunto = asunto,
            Notas = notas,
        };
    }

    public XElement ToXML()
    {
        XElement node = new(ETIQUETA_TICKET,
            new XAttribute(ETIQUETA_ENCARGADO, this.Encargado.DNI),
            new XAttribute(ETIQUETA_CLIENTE, this.Cliente.DNI),
            new XAttribute(ETIQUETA_ASUNTO, this.Asunto ?? ""),
            new XAttribute(ETIQUETA_RESULTADO, this.Resultado.ToString()),
            new XAttribute(ETIQUETA_ESTADO, this.Estado.ToString()));

        foreach (string nota in this.Notas)
            node.Add(new XElement(ETIQUETA_NOTA, nota));

        return node;
    }

    public override string ToString()
    {
        return this.ToXML().ToString();
    }
}
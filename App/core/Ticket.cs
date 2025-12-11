using System;
using System.Collections.Generic;
using System.Xml.Linq;
using App.core;
using App.Services;

namespace App.Core;

public class Ticket {
    public enum Estado {
        NoIniciado,
        EnTramite,
        Imposible,
        Solucionado,
    }
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required Trabajador Encargado { get; set; }
    public required Cliente Cliente { get; init; }
    public required string Asunto { get; init; }
    public required string Notas { get; set; }
    public required Estado Resultado { get; set; }
    public required bool Cerrado { get; set; }
    public const string ETIQUETA_TICKET = "ticket";
    protected const string ETIQUETA_ID = "id";
    protected const string ETIQUETA_ENCARGADO = "encargado";
    protected const string ETIQUETA_CLIENTE = "cliente";
    protected const string ETIQUETA_ASUNTO = "asunto";
    protected const string ETIQUETA_RESULTADO = "resultado";
    protected const string ETIQUETA_CERRADO = "cerrado";
    protected const string ETIQUETA_NOTA = "nota";

    public static Ticket? FromXML(XElement node)
    {
        string? idStr = node.Attribute(ETIQUETA_ID)?.Value;
        string? encargadoDNI = node.Attribute(ETIQUETA_ENCARGADO)?.Value;
        string? clienteDNI = node.Attribute(ETIQUETA_CLIENTE)?.Value;
        string? resultadoStr = node.Attribute(ETIQUETA_RESULTADO)?.Value;
        string? cerradoStr = node.Attribute(ETIQUETA_CERRADO)?.Value;
        string? asunto = node.Attribute(ETIQUETA_ASUNTO)?.Value;
        string? notas = node.Attribute(ETIQUETA_NOTA)?.Value;
        if (encargadoDNI == null || clienteDNI == null || !Enum.TryParse(resultadoStr, out Estado resultado) ||
            !Boolean.TryParse(cerradoStr, out Boolean cerrado) || asunto == null || !Guid.TryParse(idStr, out Guid id))
        {
            return null;
        }
        Trabajador? encargado = RegistroTrabajadores.ObtenerTrabajadorPorDNI(encargadoDNI);
        Cliente? cliente = RegistroClientes.ObtenerClientePorDNI(clienteDNI);
        if (encargado == null || cliente == null)
            return null;
        return new Ticket
        {
            Id = id,
            Encargado = encargado,
            Cliente = cliente,
            Resultado = resultado,
            Cerrado = cerrado,
            Asunto = asunto,
            Notas = notas ?? "",
        };
    }

    public XElement ToXML()
    {
        XElement node = new(ETIQUETA_TICKET,
            new XAttribute(ETIQUETA_ID, this.Id),
            new XAttribute(ETIQUETA_ENCARGADO, this.Encargado.DNI),
            new XAttribute(ETIQUETA_CLIENTE, this.Cliente.DNI),
            new XAttribute(ETIQUETA_ASUNTO, this.Asunto ?? ""),
            new XAttribute(ETIQUETA_NOTA, this.Notas),
            new XAttribute(ETIQUETA_RESULTADO, this.Resultado.ToString()),
            new XAttribute(ETIQUETA_CERRADO, this.Cerrado));

        return node;
    }

    public override string ToString()
    {
        return this.ToXML().ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace App;

public enum Resultado { EnTramite = 0, Imposible = 1, Solucionado = 2 }
public enum Estado { Abierto = 0, Cerrado = 1 }

public sealed class Cliente
{
    public string Dni { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public sealed class Personal
{
    public string Dni { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public sealed class Ticket
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Personal? TrabajadorEncargado { get; set; }
    public Cliente? ClienteOrigen { get; set; }
    public string Asunto { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public Resultado Resultado { get; set; } = Resultado.EnTramite;
    public Estado Estado { get; set; } = Estado.Abierto;
}

public interface ITicketRepository
{
    IQueryable<Ticket> Tickets { get; }
    void Add(Ticket t);
}

public sealed class InMemoryTicketRepository : ITicketRepository
{
    private readonly List<Ticket> _tickets = new();
    public IQueryable<Ticket> Tickets => _tickets.AsQueryable();
    public void Add(Ticket t) => _tickets.Add(t);

    public static InMemoryTicketRepository WithSeed()
    {
        var repo = new InMemoryTicketRepository();

        var c1 = new Cliente { Dni = "00000000A", Nombre = "Cliente Uno", Email = "c1@acme.test" };
        var c2 = new Cliente { Dni = "00000001B", Nombre = "Cliente Dos", Email = "c2@acme.test" };

        var p1 = new Personal { Dni = "11111111H", Nombre = "Técnico A", Email = "ta@empresa.test" };
        var p2 = new Personal { Dni = "22222222J", Nombre = "Técnico B", Email = "tb@empresa.test" };

        repo.Add(new Ticket
        {
            TrabajadorEncargado = p1,
            ClienteOrigen = c1,
            Asunto = "No enciende (Equipo 01)",
            Notas = "Revisar alimentación",
            Resultado = Resultado.EnTramite,
            Estado = Estado.Abierto
        });
        repo.Add(new Ticket
        {
            TrabajadorEncargado = p1,
            ClienteOrigen = c2,
            Asunto = "Error al iniciar (PC Recepción)",
            Notas = "Sucede al arrancar",
            Resultado = Resultado.Imposible,
            Estado = Estado.Cerrado
        });
        repo.Add(new Ticket
        {
            TrabajadorEncargado = p2,
            ClienteOrigen = c2,
            Asunto = "Actualización firmware (Router)",
            Notas = "Versión 1.2.3 aplicada",
            Resultado = Resultado.Solucionado,
            Estado = Estado.Cerrado
        });
        repo.Add(new Ticket
        {
            TrabajadorEncargado = p2,
            ClienteOrigen = c1,
            Asunto = "Limpieza interna (Servidor B)",
            Notas = "Mantenimiento preventivo",
            Resultado = Resultado.EnTramite,
            Estado = Estado.Abierto
        });

        return repo;
    }

}

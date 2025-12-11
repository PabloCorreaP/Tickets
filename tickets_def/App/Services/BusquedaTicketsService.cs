using System.Collections.Generic;
using System.Linq;

namespace App;

public interface IBusquedaTicketsService
{
    IEnumerable<Ticket> Buscar(Resultado? resultado = null, Estado? estado = null);
    IEnumerable<Ticket> BuscarPorResultado(Resultado resultado);
    IEnumerable<Ticket> BuscarPorEstado(Estado estado);
}

public sealed class BusquedaTicketsService : IBusquedaTicketsService
{
    private readonly ITicketRepository _repo;
    public BusquedaTicketsService(ITicketRepository repo) { _repo = repo; }

    public IEnumerable<Ticket> Buscar(Resultado? resultado = null, Estado? estado = null)
    {
        var q = _repo.Tickets;
        if (resultado.HasValue) q = q.Where(t => t.Resultado == resultado.Value);
        if (estado.HasValue)    q = q.Where(t => t.Estado == estado.Value);
        return q.ToList();
    }

    public IEnumerable<Ticket> BuscarPorResultado(Resultado resultado)
        => _repo.Tickets.Where(t => t.Resultado == resultado).ToList();

    public IEnumerable<Ticket> BuscarPorEstado(Estado estado)
        => _repo.Tickets.Where(t => t.Estado == estado).ToList();
}

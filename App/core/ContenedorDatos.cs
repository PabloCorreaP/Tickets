using System.Collections.Generic;

namespace App.core;

public class ContenedorDatos
{
    // Singleton --------------------------------------------------
    private static readonly ContenedorDatos _contenedor = new();
    public static ContenedorDatos GetContenedorDatos()
    {
        return _contenedor;
    }
    private ContenedorDatos()
    {
        
    }
    // ------------------------------------------------------------
    
    private readonly Dictionary<string, Cliente> _clientes_ = [];
    private readonly Dictionary<string, Personal> _personal_ = [];
    private readonly List<Ticket> _tickets_ = [];
    
    public Dictionary<string, Cliente> Clientes {get { return this._clientes_; }}
    public Dictionary<string, Personal> Personal {get { return this._personal_; }}
    public List<Ticket> Tickets {get { return this._tickets_; }}

    public Cliente? GetCliente(string DNI) => this._clientes_.TryGetValue(DNI, out Cliente? value) ? value : null;
    public Personal? GetPersonal(string DNI) => this._personal_.TryGetValue(DNI, out Personal? value) ? value : null;
    public void AddCliente(Cliente c) => this._clientes_.Add(c.DNI, c);
    public void AddPersonal(Personal p) => this._personal_.Add(p.DNI, p);
    public void AddTicket(Ticket t) => this._tickets_.Add(t);
    public void Clear()
    {
        this._clientes_.Clear();
        this._personal_.Clear();
        this._tickets_.Clear();
    }
}
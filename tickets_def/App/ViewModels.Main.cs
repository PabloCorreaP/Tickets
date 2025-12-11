using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App;

public sealed class Option<T> where T : struct
{
    public string Label { get; }
    public T? Value { get; }  // <- Nullable<T> (válido al tener where T : struct)

    public Option(string label, T? value)
    {
        Label = label;
        Value = value;
    }

    public override string ToString() => Label;
}



public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly IBusquedaTicketsService _busquedas;
    public ObservableCollection<Ticket> Tickets { get; } = new();

    public IReadOnlyList<Option<Resultado>> OpcionesResultado { get; } = new List<Option<Resultado>>
{
    new("Todos", null),                    // <- null real, sin (Resultado?)null
    new("En trámite", Resultado.EnTramite),
    new("Imposible", Resultado.Imposible),
    new("Solucionado", Resultado.Solucionado),
};

    public IReadOnlyList<Option<Estado>> OpcionesEstado { get; } = new List<Option<Estado>>
{
    new("Todos", null),                    // <- null real, sin (Estado?)null
    new("Abierto", Estado.Abierto),
    new("Cerrado", Estado.Cerrado),
};



    private Option<Resultado> _resultadoSel;
    public Option<Resultado> ResultadoSel
    {
        get => _resultadoSel;
        set { _resultadoSel = value; OnPropertyChanged(); Refrescar(); }
    }

    private Option<Estado> _estadoSel;
    public Option<Estado> EstadoSel
    {
        get => _estadoSel;
        set { _estadoSel = value; OnPropertyChanged(); Refrescar(); }
    }

    public MainViewModel()
    {
        var repo = InMemoryTicketRepository.WithSeed();
        _busquedas = new BusquedaTicketsService(repo);

        _resultadoSel = OpcionesResultado[0];
        _estadoSel = OpcionesEstado[0];

        Refrescar();
    }

    private void Refrescar()
    {
        Tickets.Clear();
        foreach (var t in _busquedas.Buscar(ResultadoSel.Value, EstadoSel.Value))
            Tickets.Add(t);
    }



    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

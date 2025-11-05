using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TicketStats.Core;

namespace TicketStats.View
{
    public sealed class StatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        private List<Ticket> _tickets = DummyData.Tickets();

        public string TicketsPorEstadoText { get; private set; } = "";
        public string PorTrabajadorText { get; private set; } = "";
        public string PorClienteText { get; private set; } = "";
        public string PorResultadoText { get; private set; } = "";

        public StatisticsViewModel()
        {
            Recalcular();
        }

        public void Recalcular()
        {
            TicketsPorEstadoText = FormatearGrupo(ContarPorEstado(_tickets), ordenarPorClave: true);
            PorTrabajadorText = FormatearGrupo(ContarPorTrabajador(_tickets), ordenarPorValorDesc: true);
            PorClienteText = FormatearGrupo(ContarPorCliente(_tickets), ordenarPorValorDesc: true);
            PorResultadoText = FormatearGrupo(ContarPorResultado(_tickets), ordenarPorClave: true);

            Notify(nameof(TicketsPorEstadoText));
            Notify(nameof(PorTrabajadorText));
            Notify(nameof(PorClienteText));
            Notify(nameof(PorResultadoText));
        }

        private static List<(string Etiqueta, int Total)> ContarPorEstado(IEnumerable<Ticket> tickets)
        {
            var mapa = new Dictionary<Estado, int>();
            foreach (var t in tickets)
            {
                if (!mapa.ContainsKey(t.Estado))
                    mapa[t.Estado] = 0;
                mapa[t.Estado]++;
            }

            var lista = new List<(string, int)>();
            foreach (var par in mapa)
                lista.Add((par.Key.ToString(), par.Value));

            return lista;
        }

        private static List<(string Etiqueta, int Total)> ContarPorTrabajador(IEnumerable<Ticket> tickets)
        {
            var mapa = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var t in tickets)
            {
                var nombre = t.Trabajador?.Nombre ?? "Sin Nombre";
                if (!mapa.ContainsKey(nombre))
                    mapa[nombre] = 0;
                mapa[nombre]++;
            }

            var lista = new List<(string, int)>();
            foreach (var par in mapa)
            {
                lista.Add((par.Key, par.Value));
            }
            return lista;
        }

        private static List<(string Etiqueta, int Total)> ContarPorCliente(IEnumerable<Ticket> tickets)
        {
            var mapa = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var t in tickets)
            {
                var nombre = t.Cliente?.Nombre ?? "(sin)";
                if (!mapa.ContainsKey(nombre)) mapa[nombre] = 0;
                mapa[nombre]++;
            }

            var lista = new List<(string, int)>();
            foreach (var par in mapa)
                lista.Add((par.Key, par.Value));

            return lista;
        }

        private static List<(string Etiqueta, int Total)> ContarPorResultado(IEnumerable<Ticket> tickets)
        {
            var mapa = new Dictionary<Resultado, int>();
            foreach (var t in tickets)
            {
                if (!mapa.ContainsKey(t.Resultado))
                    mapa[t.Resultado] = 0;
                mapa[t.Resultado]++;
            }

            var lista = new List<(string, int)>();
            foreach (var par in mapa)
            {
                lista.Add((par.Key.ToString(), par.Value));
            }
            return lista;
        }


        private static string FormatearGrupo(List<(string Etiqueta, int Total)> filas, bool ordenarPorClave = false, bool ordenarPorValorDesc = false)
        {
            if (ordenarPorClave)
                filas = filas.OrderBy(x => x.Etiqueta, StringComparer.Ordinal).ToList();
            else if (ordenarPorValorDesc)
                filas = filas.OrderByDescending(x => x.Total).ToList();

            int total = 0;
            foreach (var f in filas)
            {
                total += f.Total;
            }
            var toret = new System.Text.StringBuilder();
            foreach (var f in filas)
            {
                toret.AppendLine($"{f.Etiqueta,-20} : {f.Total,3}");
            }
            if (total > 0) 
                toret.AppendLine($"{"TOTAL",-20} : {total,3}");
            return toret.ToString();
        }


        private void Notify([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}

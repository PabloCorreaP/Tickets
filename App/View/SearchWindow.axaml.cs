using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ticketsIndividual.Core;

namespace App.View;

partial class SearchWindow : Window
{
    static Busquedas buscador;
    public SearchWindow(ReadOnlyCollection<Ticket> t)
    {
        InitializeComponent();
        buscador = new Busquedas(t);
        var btSearch = this.GetControl<Button>("SearchBT");
        var btCancel = this.GetControl<Button>("CancelButton");

        btSearch.Click += (_, _) => this.OnSearch();
        btCancel.Click += (_, _) => this.OnExit();

    }

    private void OnSearch()
    {
        var cliente = this.GetControl<TextBox>("ClientName");
        var encargado = this.GetControl<TextBox>("PersonalName");
        IEnumerable<string> ticketsEncontrados = buscador.Search(encargado.Text ?? "" ,cliente.Text ?? "");
        OnViewResult(ticketsEncontrados.ToArray());
    }

    private void OnViewResult(string[] tickets)
    {
        var res = new ResultadoBusqueda(tickets);
        res.ShowDialog(this);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnExit()
    {
        this.Close();
    }
}
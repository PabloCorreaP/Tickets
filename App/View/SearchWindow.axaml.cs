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
        var btSearchCliente = this.GetControl<Button>("SearchClientBT");
        var btSearchPersonal = this.GetControl<Button>("SearchPersonalBT");
        var btCancel = this.GetControl<Button>("CancelButton");

        btSearchCliente.Click += (_, _) => this.OnSearchCliente();
        btSearchPersonal.Click += (_, _) => this.OnSearchPersonal();
        btCancel.Click += (_, _) => this.OnExit();

    }

    private void OnSearchCliente()
    {
        var cliente = this.GetControl<TextBox>("ClientName");
        List<String> ticketsEncontrados = buscador.SearchCliente(cliente.Text ?? "");
        OnViewResult(ticketsEncontrados);
    }
    private void OnSearchPersonal()
    {
        var encargado = this.GetControl<TextBox>("PersonalName");
        List<String> ticketsEncontrados = buscador.SearchPersonal(encargado.Text ?? "");
        OnViewResult(ticketsEncontrados);
    }

    private void OnViewResult(List<String> tickets)
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
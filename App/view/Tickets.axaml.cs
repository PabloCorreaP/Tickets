using System.Collections.Generic;
using App.Core;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace App.View;

public partial class Tickets : Window
{
    private static List<Ticket> tickets = new List<Ticket>();
    private int index = 0;

    // private TextBox f_encargado;
    // private TextBox f_cliente;
    // private TextBox f_asunto;
    // private TextBox f_notas;
    // private ComboBox f_res;
    // private CheckBox f_cerrado;

    // private Button b_prev;
    // private Button b_next;
    // private Button b_delete;
    // private Button b_create;

    public Tickets()
    {
        InitializeComponent();

        this.f_encargado = this.GetControl<TextBox>("f_encargado");
        this.f_cliente = this.GetControl<TextBox>("f_cliente");
        this.f_asunto = this.GetControl<TextBox>("f_asunto");
        this.f_notas = this.GetControl<TextBox>("f_notas");
        this.f_res = this.GetControl<ComboBox>("f_res");
        this.f_cerrado = this.GetControl<CheckBox>("f_cerrado");

        this.b_prev = this.GetControl<Button>("b_prev");
        this.b_next = this.GetControl<Button>("b_next");
        this.b_delete = this.GetControl<Button>("b_delete");
        this.b_create = this.GetControl<Button>("b_create");

        this.Refresh();
    }
    
    public void OnPrev(object sender, RoutedEventArgs args) {
        this.index -= 1;
        this.Refresh();
    }

    public void OnNext(object sender, RoutedEventArgs args) {
        this.index += 1;
        this.Refresh();
    }

    public void OnDelete(object sender, RoutedEventArgs args) {
        Tickets.tickets.RemoveAt(this.index);
        this.Refresh();
    }
    
    public void OnCreate(object sender, RoutedEventArgs args) {
        var ticket = new Ticket {
            DNIEncargado = this.f_encargado.Text!,
            DNICliente = this.f_cliente.Text!,
            Asunto = this.f_asunto.Text!,
            Notas = this.f_notas.Text!,
            Resultado = (Ticket.Estado)this.f_res.SelectedIndex,
            Cerrado = this.f_cerrado.IsChecked ?? false
        };

        Tickets.tickets.Add(ticket);
        this.Refresh();
    }

    private void Refresh() {
        this.RefreshButtons();
        this.RefreshFields();
    }

    private void RefreshButtons() {
        this.b_prev.IsEnabled = this.index != 0;
        this.b_next.IsEnabled = this.index < Tickets.tickets.Count;
        this.b_delete.IsEnabled = this.index < Tickets.tickets.Count;
        this.b_create.IsEnabled = this.index == Tickets.tickets.Count;
    }

    private void RefreshFields() {
        var adding = this.index == Tickets.tickets.Count;
        if (adding) {
            this.f_asunto.Text = "";
            this.f_cerrado.IsChecked = false;
            this.f_cliente.Text = "";
            this.f_encargado.Text = "";
            this.f_notas.Text = "";
            this.f_res.SelectedIndex = (int)Ticket.Estado.NoIniciado;
        } else {
            var item = Tickets.tickets[this.index];

            this.f_asunto.Text = item.Asunto;
            this.f_cerrado.IsChecked = item.Cerrado;
            this.f_cliente.Text = item.DNICliente;
            this.f_encargado.Text = item.DNIEncargado;
            this.f_notas.Text = item.Notas;
            this.f_res.SelectedIndex = (int)item.Resultado;
        }

        this.f_cliente.IsEnabled = adding;
        this.f_asunto.IsEnabled = adding;
    }
}

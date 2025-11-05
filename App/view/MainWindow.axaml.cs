using Avalonia.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ticketsIndividual.Core;

namespace App.View;

public partial class MainWindow : Window
{
    Tickets tickets;
    public MainWindow()
    {
        this.tickets = new Tickets();
        Cliente c1 = new Cliente { dni = "1111111", email = "a", nombre = "cliente1" };
        Cliente c2 = new Cliente { dni = "1111111", email = "a", nombre = "cliente2" };
        Personal p1 = new Personal { dni = "1111111", email = "a", nombre = "personal1" };
        Personal p2 = new Personal { dni = "1111111", email = "a", nombre = "personal2" };

        Ticket t1 = new Ticket { asunto="ticket1", cliente=c1, encargado= p1 };
        Ticket t2 = new Ticket { asunto="ticket2", cliente=c1, encargado= p1 };
        Ticket t3 = new Ticket { asunto="ticket3", cliente=c2, encargado= p2 };
        Ticket t4 = new Ticket { asunto="ticket4", cliente=c2, encargado= p1 };
        
        
        tickets.Add(t2);
        tickets.Add(t3);
        tickets.Add(t4);
        for (int i = 0; i < 100; i++)
        {
            tickets.Add( new Ticket { asunto = "ticket"+i, cliente = c1, encargado = p1 });
        }


        InitializeComponent();
        var btBusqueda = this.GetControl<Button>("BtBusqueda");
        btBusqueda.Click += async (_, _) => await this.OnSearch(tickets.GetTickets());
    }

    private async Task OnSearch(ReadOnlyCollection<Ticket> tickets)
    {
        var searchWindow = new SearchWindow(tickets);
        await searchWindow.ShowDialog(this);
    }
}
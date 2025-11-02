using System.Linq;
using System.Xml.Linq;
using App.core;
using Avalonia.Controls;

namespace App.view;

public partial class MainWindow : Window
{
    private const string PATH_TO_XML = "tickets.xml";
    private const string ETIQUETA_APP_TICKETS = "app_tickets";
    private const string ETIQUETA_CLIENTES = "clientes";
    private const string ETIQUETA_PERSONAL = "personal";
    private const string ETIQUETA_TICKETS = "tickets";
    private static readonly ContenedorDatos datos = ContenedorDatos.GetContenedorDatos();
    public MainWindow()
    {
        InitializeComponent();

        this.CrearDatosRelleno();

        var btToXML = this.GetControl<Button>("BtToXML");
        var btFromXML = this.GetControl<Button>("BtFromXML");

        btToXML.Click += (_, _) => this.ToXML().Save(PATH_TO_XML);
        btFromXML.Click += (_, _) => this.FromXML(XDocument.Load(PATH_TO_XML).Descendants(ETIQUETA_APP_TICKETS).FirstOrDefault(new XElement(ETIQUETA_APP_TICKETS)));
    }

    private void CrearDatosRelleno()
    {
        Cliente cliente = new()
        {
            DNI = "49713276H",
            Email = "direccionfalsa@correofalso.com",
            Nombre = "Yo"
        };
        Personal personal = new()
        {
            DNI = "49713276H",
            Email = "yo@empresamalvada.com",
            Nombre = "Yo si trabajase"
        };
        Ticket ticket = new()
        {
            Encargado = personal,
            Cliente = cliente,
            Resultado = Resultado.EN_TRAMITE,
            Estado = Estado.ABIERTO,
            Asunto = "Hacer trabajo DIA",
            Notas = ["Fecha entrega individual: 25/11/06", "Fecha entrega grupal: no me acuerdo"]
        };
        datos.AddCliente(cliente);
        datos.AddPersonal(personal);
        datos.AddTicket(ticket);
    }

    private XElement ToXML()
    {
        XElement root = new(ETIQUETA_APP_TICKETS);

        XElement clientes = new(ETIQUETA_CLIENTES);
        foreach (Cliente cliente in datos.Clientes.Values)
            clientes.Add(cliente.ToXML());
        root.Add(clientes);

        XElement personal = new(ETIQUETA_PERSONAL);
        foreach (Personal empleado in datos.Personal.Values)
            personal.Add(empleado.ToXML());
        root.Add(personal);

        XElement tickets = new(ETIQUETA_TICKETS);
        foreach (Ticket ticket in datos.Tickets)
            tickets.Add(ticket.ToXML());
        root.Add(tickets);

        return root;
    }
    
    private void FromXML(XElement doc)
    {
        XElement? nodoClientes = doc.Descendants(ETIQUETA_CLIENTES).FirstOrDefault();
        if (nodoClientes == null)
            return;
        datos.Clientes.Clear();
        foreach (Cliente? c in nodoClientes.Descendants(Usuario.ETIQUETA_USUARIO).Select(nodo => Cliente.FromXML(nodo)))
        {
            if (c != null)
            {
                datos.AddCliente(c);
            }
        }
        
        XElement? nodoPersonal = doc.Descendants(ETIQUETA_PERSONAL).FirstOrDefault();
        if (nodoPersonal == null)
            return;
        datos.Personal.Clear();
        foreach (Personal? p in nodoPersonal.Descendants(Usuario.ETIQUETA_USUARIO).Select(nodo => Personal.FromXML(nodo)))
        {
            if (p != null)
            {
                datos.AddPersonal(p);
            }
        }
        
        XElement? nodoTickets = doc.Descendants(ETIQUETA_TICKETS).FirstOrDefault();
        if (nodoTickets == null)
            return;
        datos.Tickets.Clear();
        foreach (Ticket? t in nodoTickets.Descendants(Ticket.ETIQUETA_TICKET).Select(nodo => Ticket.FromXML(nodo)))
        {
            if (t != null)
            {
                datos.AddTicket(t);
            }
        }
    }
}
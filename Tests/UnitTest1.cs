using System.Xml.Linq;
using App.core;

namespace Tests;

public class Tests
{
    private Cliente cliente;
    private Personal personal;
    private Ticket ticket;
    [SetUp]
    public void Setup()
    {
        cliente = new() {
            DNI = "49713276H",
            Email = "direccionfalsa@correofalso.com",
            Nombre = "supongo que considero mi dni un dato menos privado que mi correo"
        };
        personal = new() {
            DNI = "49713276H",
            Email = "yo@empresamalvada.com",
            Nombre = "ahora cotizo :("
        };
        ticket = new()
        {
            Encargado = personal,
            Cliente = cliente,
            Resultado = Resultado.EN_TRAMITE,
            Estado = Estado.ABIERTO,
            Asunto = "Bombardear la esei",
            Notas = ["Sacar a cacho del edificio primero", "Y a rodeiro"]
        };
        ContenedorDatos.GetContenedorDatos().Clear();
        ContenedorDatos.GetContenedorDatos().AddCliente(cliente);
        ContenedorDatos.GetContenedorDatos().AddPersonal(personal);
        ContenedorDatos.GetContenedorDatos().AddTicket(ticket);
    }

    [Test]
    public void TestDNIValido()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Utils.DNIValido("49713276H"), Is.True);
            Assert.That(Utils.DNIValido("49713276F"), Is.False);
            Assert.That(Utils.DNIValido("4971327H"), Is.False);
            Assert.That(Utils.DNIValido("497132766H"), Is.False);
            Assert.That(Utils.DNIValido(""), Is.False);
        });
    }

    [Test]
    public void TestEmailValido()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Utils.EmailValido("sadasda@asdasd.asdasd"), Is.True);
            Assert.That(Utils.EmailValido("sadasdaasdasd.asdasd"), Is.False);
            Assert.That(Utils.EmailValido("sadasda@asdasdasdasd"), Is.False);
            Assert.That(Utils.EmailValido("sadasdaasdasdasdasd"), Is.False);
            Assert.That(Utils.EmailValido("sadasda@asdasd."), Is.False);
            Assert.That(Utils.EmailValido("sadasda@.asdasd"), Is.False);
            Assert.That(Utils.EmailValido("@asdasd.asdasd"), Is.False);
            Assert.That(Utils.EmailValido(""), Is.False);
        });
    }

    [Test]
    public void TestTicketXML()
    {
        XElement ticketElement = ticket.ToXML();
        Ticket? ticketFromElement = Ticket.FromXML(ticketElement);
        Assert.That(ticketFromElement, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(ticketFromElement.Asunto, Is.EqualTo(ticket.Asunto));
            Assert.That(ticketFromElement.Cliente.DNI, Is.EqualTo(ticket.Cliente.DNI));
            Assert.That(ticketFromElement.Encargado.DNI, Is.EqualTo(ticket.Encargado.DNI));
            Assert.That(ticketFromElement.Estado, Is.EqualTo(ticket.Estado));
            Assert.That(ticketFromElement.Resultado, Is.EqualTo(ticket.Resultado));
            Assert.That(ticketFromElement.Notas, Is.EqualTo(ticket.Notas));
        });
    }

    [Test]
    public void TestClienteXML()
    {
        XElement clienteElement = cliente.ToXML();
        Cliente? clienteFromElement = Cliente.FromXML(clienteElement);
        Assert.That(clienteFromElement, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(clienteFromElement.DNI, Is.EqualTo(cliente.DNI));
            Assert.That(clienteFromElement.Email, Is.EqualTo(cliente.Email));
            Assert.That(clienteFromElement.Nombre, Is.EqualTo(cliente.Nombre));
        });
    }

    [Test]
    public void TestPersonalXML()
    {
        XElement personalElement = personal.ToXML();
        Personal? personalFromElement = Personal.FromXML(personalElement);
        Assert.That(personalFromElement, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(personalFromElement.DNI, Is.EqualTo(personal.DNI));
            Assert.That(personalFromElement.Email, Is.EqualTo(personal.Email));
            Assert.That(personalFromElement.Nombre, Is.EqualTo(personal.Nombre));
            Assert.That(personalFromElement.TicketsAsignados, Is.EqualTo(personal.TicketsAsignados));
        });
    }
}

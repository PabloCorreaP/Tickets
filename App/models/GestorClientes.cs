using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App.Models;

public class GestorClientes
{
    private List<Cliente> _listaClientes = new();

    public List<Cliente> ListaClientes
    {
        get => _listaClientes.ToList();
        set => _listaClientes = value;
    }

    public void AgregarCliente(Cliente cliente)
    {
        _listaClientes.Add(cliente);
    }

    public Cliente? ObtenerClientePorDni(int dni)
    {
        return _listaClientes.Find(c => c.Dni == dni);
    }

    public void EliminarClientePorDni(int dni)
    {
        var cliente = ObtenerClientePorDni(dni);
        if (cliente != null)
        {
            _listaClientes.Remove(cliente);
        }
    }

    public void Guardar(string rutaArchivo)
    {
        var lineas = ListaClientes.Select(c => $"{c.Dni};{c.Nombre};{c.Email}");
        File.WriteAllLines(rutaArchivo, lineas);
    }

    public void Cargar(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
            return;

        var lineas = File.ReadAllLines(rutaArchivo);
        _listaClientes.Clear();

        foreach (var linea in lineas)
        {
            var partes = linea.Split(';');
            if (partes.Length == 3 &&
                int.TryParse(partes[0], out var id))
            {
                var cliente = new Cliente(id, partes[1], partes[2]);
                _listaClientes.Add(cliente);
            }
        }
    }
}

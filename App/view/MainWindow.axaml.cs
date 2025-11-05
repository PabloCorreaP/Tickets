using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using App.Models;

namespace App;

public partial class MainWindow : Window
{
    private readonly GestorClientes _gestorClientes = new();
    private bool _modoModificar;

    public MainWindow()
    {
        InitializeComponent();
        EstablecerModo(false);
        RefrescarLista();
    }

    private void GuardarCliente_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(IdTextBox.Text?.Trim(), out var dni) || dni <= 0)
        {
            MostrarEstado("El DNI debe ser un número entero positivo.");
            return;
        }

        var nombre = NombreTextBox.Text?.Trim() ?? string.Empty;
        var email = EmailTextBox.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nombre))
        {
            MostrarEstado("El nombre no puede estar vacío.");
            return;
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            MostrarEstado("Proporciona un email válido.");
            return;
        }

        var existente = _gestorClientes.ObtenerClientePorDni(dni);

        if (_modoModificar)
        {
            if (existente is null)
            {
                MostrarEstado("No existe un cliente con ese DNI. Cambia a modo crear para agregarlo.");
                return;
            }

            existente.Nombre = nombre;
            existente.Email = email;
            MostrarEstado($"Cliente {nombre} actualizado.");
        }
        else
        {
            if (existente is not null)
            {
                MostrarEstado("Ya existe un cliente con ese DNI. Cambia a modo modificar para actualizarlo.");
                return;
            }

            var nuevo = new Cliente(dni, nombre, email);
            _gestorClientes.AgregarCliente(nuevo);
            MostrarEstado($"Cliente {nombre} creado.");
        }

        RefrescarLista(dni);
    }

    private void EliminarCliente_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(IdTextBox.Text?.Trim(), out var dni))
        {
            MostrarEstado("Selecciona un cliente o indica un DNI válido para eliminar.");
            return;
        }

        var existente = _gestorClientes.ObtenerClientePorDni(dni);
        if (existente is null)
        {
            MostrarEstado("No se encontró el cliente a eliminar.");
            return;
        }

        _gestorClientes.EliminarClientePorDni(dni);
        MostrarEstado($"Cliente {existente.Nombre} eliminado.");
        RefrescarLista();
        LimpiarFormulario();
        EstablecerModo(false);
    }

    private void LimpiarFormulario_OnClick(object? sender, RoutedEventArgs e)
    {
        LimpiarFormulario();
        MostrarEstado("Formulario listo para un nuevo cliente.");
        EstablecerModo(false);
    }

    private void ClientesListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ClientesListBox.SelectedItem is Cliente seleccionado)
        {
            IdTextBox.Text = seleccionado.Dni.ToString();
            NombreTextBox.Text = seleccionado.Nombre;
            EmailTextBox.Text = seleccionado.Email;
            MostrarEstado($"Editando cliente {seleccionado.Nombre}.");
            EstablecerModo(true);
        }
        else
        {
            LimpiarCamposFormulario();
            MostrarEstado("Selecciona un cliente o ingresa la información para crear uno nuevo.");
            EstablecerModo(false);
        }
    }

    private void RecargarClientes_OnClick(object? sender, RoutedEventArgs e)
    {
        RefrescarLista();
        MostrarEstado("Lista de clientes actualizada.");
    }

    private async void GuardarArchivo_OnClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Title = "Guardar clientes",
            Filters = new List<FileDialogFilter>
            {
                new()
                {
                    Name = "CSV",
                    Extensions = new List<string> { "csv" }
                },
                new()
                {
                    Name = "Texto",
                    Extensions = new List<string> { "txt" }
                }
            }
        };

        var path = await dialog.ShowAsync(this);
        if (string.IsNullOrWhiteSpace(path))
            return;

        try
        {
            _gestorClientes.Guardar(path);
            MostrarEstado($"Clientes guardados en {path}.");
        }
        catch (Exception ex)
        {
            MostrarEstado($"Error al guardar: {ex.Message}");
        }
    }

    private async void CargarArchivo_OnClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Cargar clientes",
            AllowMultiple = false,
            Filters = new List<FileDialogFilter>
            {
                new()
                {
                    Name = "CSV",
                    Extensions = new List<string> { "csv" }
                },
                new()
                {
                    Name = "Texto",
                    Extensions = new List<string> { "txt" }
                }
            }
        };

        var paths = await dialog.ShowAsync(this);
        var path = paths?.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(path))
            return;

        try
        {
            _gestorClientes.Cargar(path);
            RefrescarLista();
            LimpiarFormulario();
            MostrarEstado($"Clientes cargados desde {path}.");
            EstablecerModo(false);
        }
        catch (Exception ex)
        {
            MostrarEstado($"Error al cargar: {ex.Message}");
        }
    }

    private void RefrescarLista(int? clienteSeleccionadoDni = null)
    {
        var selectedDni = clienteSeleccionadoDni ?? (ClientesListBox.SelectedItem as Cliente)?.Dni;
        var clientes = _gestorClientes.ListaClientes;
        ClientesListBox.ItemsSource = null;
        ClientesListBox.ItemsSource = clientes;

        if (selectedDni.HasValue)
        {
            var seleccionado = clientes.FirstOrDefault(c => c.Dni == selectedDni.Value);
            if (seleccionado != null)
            {
                ClientesListBox.SelectedItem = seleccionado;
            }
            else
            {
                ClientesListBox.SelectedItem = null;
            }
        }
    }

    private void LimpiarFormulario()
    {
        ClientesListBox.SelectedItem = null;
        LimpiarCamposFormulario();
    }

    private void LimpiarCamposFormulario()
    {
        IdTextBox.Text = string.Empty;
        NombreTextBox.Text = string.Empty;
        EmailTextBox.Text = string.Empty;
    }

    private void MostrarEstado(string mensaje)
    {
        StatusTextBlock.Text = mensaje;
    }

    private void ToggleModo_OnClick(object? sender, RoutedEventArgs e)
    {
        EstablecerModo(!_modoModificar);
        MostrarEstado(_modoModificar
            ? "Modo modificar activo. Selecciona un cliente existente para actualizarlo."
            : "Modo crear activo. Introduce los datos para un nuevo cliente.");
    }

    private void EstablecerModo(bool modificar)
    {
        _modoModificar = modificar;
        if (ToggleModoButton != null)
        {
            ToggleModoButton.Content = _modoModificar ? "Modo: Modificar" : "Modo: Crear";
        }
    }
}

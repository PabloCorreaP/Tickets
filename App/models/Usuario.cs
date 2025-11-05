namespace App.Models;

public abstract class Usuario
{
    public int Dni { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }

    protected Usuario(int dni, string nombre, string email)
    {
        Dni = dni;
        Nombre = nombre;
        Email = email;
    }
}

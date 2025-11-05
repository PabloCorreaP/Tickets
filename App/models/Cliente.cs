namespace App.Models;

public class Cliente : Usuario
{
    public Cliente(int dni, string nombre, string email) : base(dni, nombre, email)
    {
    }
}

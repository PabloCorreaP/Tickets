namespace App.core;

public static class Utils
{
    private static readonly char[] DNI_LETRAS = ['T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E'];
    public static bool DNIValido(string dni)
    {
        if (dni.Length != 9)
            return false;
        if (!int.TryParse(dni[..8], out int nif))
            return false;
        char letra = DNI_LETRAS[nif % 23];
        return letra == dni[8];
    }

    public static bool EmailValido(string email)
    {
        string[] splitAt = email.Split('@');
        if (splitAt.Length != 2)
            return false;
        if (splitAt[0].Length <= 0)
            return false;
        string[] splitDot = splitAt[1].Split('.');
        if (splitDot.Length != 2 || splitDot[0].Length <= 0 || splitDot[1].Length <= 0)
            return false;
        return true;
    }
}
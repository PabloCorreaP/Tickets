namespace App.Core;

public class Ticket {
    public enum Estado {
        NoIniciado,
        EnTramite,
        Imposible,
        Solucionado,
    }

    public required string DNIEncargado { get; set; }
    public required string DNICliente { get; init; }
    public required string Asunto { get; init; }
    public required string Notas { get; set; }
    public required Estado Resultado { get; set; }
    public required bool Cerrado { get; set; }
}

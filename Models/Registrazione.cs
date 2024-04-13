using System.ComponentModel.DataAnnotations;

public class Registrazione
{
    public int RegistrazioneId{ get; set; }
    public DateTime Data{ get; set; }
    public string? nome { get; set; }
    public string? cognome{ get; set; }
    [EmailAddress]
    public string? email { get; set; }
    public string? password{ get; set; }
    public string? sesso { get; set; }
    public string? ruolo { get; set; }
}
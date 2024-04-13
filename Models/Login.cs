using System.ComponentModel.DataAnnotations;

public class Login
{

    public DateTime DataNascita { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    public string Password{ get; set; }
    
    public string Sesso { get; set; }
    public string Ruolo { get; set; }
    public int RegistrazioneId{ get; set; }//chiave esterna
}

namespace CoisasEmprestadas.Negocio.Models;

public class Emprestimo
{
    public int Id { get; set; }

    public string Item { get; set; } = string.Empty;

    public string Amigo { get; set; } = string.Empty;

    public string? Contato { get; set; }

    public DateTime DataEmprestimo { get; set; }

    public DateTime? DataDevolucaoPrevista { get; set; }

    public DateTime? DataDevolucaoReal { get; set; }

    public bool Devolvido => DataDevolucaoReal.HasValue;

    public bool Atrasado =>
        !Devolvido &&
        DataDevolucaoPrevista.HasValue &&
        DataDevolucaoPrevista.Value.Date < DateTime.Today;

    public int DiasDeAtraso =>
        Atrasado ? (DateTime.Today - DataDevolucaoPrevista!.Value.Date).Days : 0;
}

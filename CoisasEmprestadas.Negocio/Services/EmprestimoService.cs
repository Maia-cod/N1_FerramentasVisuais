using CoisasEmprestadas.Negocio.Exceptions;
using CoisasEmprestadas.Negocio.Models;
using CoisasEmprestadas.Negocio.Repositories;

namespace CoisasEmprestadas.Negocio.Services;

public class EmprestimoService
{
    private const int LimiteItem = 150;
    private const int LimiteAmigo = 120;
    private const int LimiteContato = 120;

    private readonly IEmprestimoRepository _repositorio;

    public EmprestimoService(IEmprestimoRepository repositorio)
    {
        _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
    }

    public void GarantirEstrutura()
    {
        _repositorio.GarantirEstrutura();
    }

    public int Registrar(Emprestimo emprestimo)
    {
        ArgumentNullException.ThrowIfNull(emprestimo);
        Validar(emprestimo);
        return _repositorio.Inserir(emprestimo);
    }

    public void RegistrarDevolucao(int id, DateTime dataDevolucao)
    {
        if (id <= 0)
            throw new RegraNegocioException("Selecione um empréstimo válido para registrar a devolução.");

        if (dataDevolucao.Date > DateTime.Today)
            throw new RegraNegocioException("A data de devolução não pode ser futura.");

        _repositorio.MarcarComoDevolvido(id, dataDevolucao.Date);
    }

    public IList<Emprestimo> Listar()
    {
        return _repositorio.ListarTodos();
    }

    private static void Validar(Emprestimo emprestimo)
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(emprestimo.Item))
            erros.Add("Informe o item que foi emprestado.");
        else if (emprestimo.Item.Trim().Length > LimiteItem)
            erros.Add($"O item deve ter no máximo {LimiteItem} caracteres.");

        if (string.IsNullOrWhiteSpace(emprestimo.Amigo))
            erros.Add("Informe o nome do amigo.");
        else if (emprestimo.Amigo.Trim().Length > LimiteAmigo)
            erros.Add($"O nome do amigo deve ter no máximo {LimiteAmigo} caracteres.");

        if (!string.IsNullOrWhiteSpace(emprestimo.Contato) &&
            emprestimo.Contato!.Trim().Length > LimiteContato)
            erros.Add($"O contato deve ter no máximo {LimiteContato} caracteres.");

        if (emprestimo.DataEmprestimo == default)
            erros.Add("Informe a data do empréstimo.");
        else if (emprestimo.DataEmprestimo.Date > DateTime.Today)
            erros.Add("A data do empréstimo não pode ser futura.");

        if (emprestimo.DataDevolucaoPrevista.HasValue &&
            emprestimo.DataDevolucaoPrevista.Value.Date < emprestimo.DataEmprestimo.Date)
            erros.Add("A data combinada de devolução não pode ser anterior à data do empréstimo.");

        if (erros.Count > 0)
            throw new RegraNegocioException("• " + string.Join(Environment.NewLine + "• ", erros));
    }
}

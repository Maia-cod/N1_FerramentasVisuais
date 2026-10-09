using CoisasEmprestadas.Negocio.Models;

namespace CoisasEmprestadas.Negocio.Repositories;

public interface IEmprestimoRepository
{
    void GarantirEstrutura();

    int Inserir(Emprestimo emprestimo);

    void MarcarComoDevolvido(int id, DateTime dataDevolucao);

    IList<Emprestimo> ListarTodos();
}

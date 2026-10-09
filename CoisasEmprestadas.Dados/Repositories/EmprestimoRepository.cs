using CoisasEmprestadas.Dados.Database;
using CoisasEmprestadas.Negocio.Models;
using CoisasEmprestadas.Negocio.Repositories;
using MySqlConnector;

namespace CoisasEmprestadas.Dados.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    public void GarantirEstrutura()
    {
        BancoInitializer.GarantirEstrutura();
    }

    public int Inserir(Emprestimo emprestimo)
    {
        using var conexao = ConnectionFactory.CriarConexao();
        using var transacao = conexao.BeginTransaction();

        try
        {
            const string sql = @"INSERT INTO Emprestimo
                                     (Item, Amigo, Contato, DataEmprestimo, DataDevolucaoPrevista, DataDevolucaoReal)
                                 VALUES
                                     (@Item, @Amigo, @Contato, @DataEmprestimo, @DataDevolucaoPrevista, NULL);
                                 SELECT LAST_INSERT_ID();";

            using var comando = new MySqlCommand(sql, conexao, transacao);
            comando.Parameters.AddWithValue("@Item", emprestimo.Item.Trim());
            comando.Parameters.AddWithValue("@Amigo", emprestimo.Amigo.Trim());
            comando.Parameters.AddWithValue("@Contato",
                string.IsNullOrWhiteSpace(emprestimo.Contato) ? DBNull.Value : emprestimo.Contato!.Trim());
            comando.Parameters.AddWithValue("@DataEmprestimo", emprestimo.DataEmprestimo.Date);
            comando.Parameters.AddWithValue("@DataDevolucaoPrevista",
                (object?)emprestimo.DataDevolucaoPrevista?.Date ?? DBNull.Value);

            var id = Convert.ToInt32(comando.ExecuteScalar());

            RegistrarLog(conexao, transacao, id, "CADASTRO");

            transacao.Commit();
            return id;
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }

    public void MarcarComoDevolvido(int id, DateTime dataDevolucao)
    {
        using var conexao = ConnectionFactory.CriarConexao();
        using var transacao = conexao.BeginTransaction();

        try
        {
            const string sql = @"UPDATE Emprestimo
                                    SET DataDevolucaoReal = @DataDevolucao
                                  WHERE Id = @Id
                                    AND DataDevolucaoReal IS NULL;";

            using var comando = new MySqlCommand(sql, conexao, transacao);
            comando.Parameters.AddWithValue("@DataDevolucao", dataDevolucao.Date);
            comando.Parameters.AddWithValue("@Id", id);

            var linhasAfetadas = comando.ExecuteNonQuery();

            if (linhasAfetadas == 0)
                throw new InvalidOperationException("Empréstimo não encontrado ou já registrado como devolvido.");

            RegistrarLog(conexao, transacao, id, "DEVOLUCAO");

            transacao.Commit();
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }

    public IList<Emprestimo> ListarTodos()
    {
        var emprestimos = new List<Emprestimo>();

        using var conexao = ConnectionFactory.CriarConexao();

        const string sql = @"SELECT Id, Item, Amigo, Contato, DataEmprestimo, DataDevolucaoPrevista, DataDevolucaoReal
                               FROM Emprestimo
                           ORDER BY DataDevolucaoReal IS NOT NULL, DataDevolucaoPrevista IS NULL, DataDevolucaoPrevista, DataEmprestimo;";

        using var comando = new MySqlCommand(sql, conexao);
        using var leitor = comando.ExecuteReader();

        while (leitor.Read())
        {
            emprestimos.Add(new Emprestimo
            {
                Id = leitor.GetInt32(0),
                Item = leitor.GetString(1),
                Amigo = leitor.GetString(2),
                Contato = leitor.IsDBNull(3) ? null : leitor.GetString(3),
                DataEmprestimo = leitor.GetDateTime(4),
                DataDevolucaoPrevista = leitor.IsDBNull(5) ? null : leitor.GetDateTime(5),
                DataDevolucaoReal = leitor.IsDBNull(6) ? null : leitor.GetDateTime(6)
            });
        }

        return emprestimos;
    }

    private static void RegistrarLog(MySqlConnection conexao, MySqlTransaction transacao, int idEmprestimo, string operacao)
    {
        const string sql = @"INSERT INTO LogOperacao (IdEmprestimo, Operacao) VALUES (@IdEmprestimo, @Operacao);";

        using var comando = new MySqlCommand(sql, conexao, transacao);
        comando.Parameters.AddWithValue("@IdEmprestimo", idEmprestimo);
        comando.Parameters.AddWithValue("@Operacao", operacao);
        comando.ExecuteNonQuery();
    }
}

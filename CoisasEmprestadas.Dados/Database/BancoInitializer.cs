using MySqlConnector;

namespace CoisasEmprestadas.Dados.Database;

public static class BancoInitializer
{
    public static void GarantirEstrutura()
    {
        using var conexao = ConnectionFactory.CriarConexaoServidor();

        foreach (var comandoSql in ScriptBanco.Comandos)
        {
            using var comando = new MySqlCommand(comandoSql, conexao);
            comando.ExecuteNonQuery();
        }
    }
}

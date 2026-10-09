using System.Configuration;
using MySqlConnector;

namespace CoisasEmprestadas.Dados.Database;

public static class ConnectionFactory
{
    private const string NomeConnectionString = "CoisasEmprestadas";

    public static string ObterConnectionString()
    {
        var configurada = ConfigurationManager.ConnectionStrings[NomeConnectionString];

        if (configurada is null || string.IsNullOrWhiteSpace(configurada.ConnectionString))
            throw new ConfigurationErrorsException(
                $"A connection string '{NomeConnectionString}' não foi encontrada no arquivo de configuração (App.config).");

        return configurada.ConnectionString;
    }

    public static MySqlConnection CriarConexao()
    {
        var conexao = new MySqlConnection(ObterConnectionString());
        conexao.Open();
        return conexao;
    }

    public static MySqlConnection CriarConexaoServidor()
    {
        var builder = new MySqlConnectionStringBuilder(ObterConnectionString())
        {
            Database = string.Empty
        };

        var conexao = new MySqlConnection(builder.ConnectionString);
        conexao.Open();
        return conexao;
    }
}

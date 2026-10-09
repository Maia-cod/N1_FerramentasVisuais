using System.Configuration;
using CoisasEmprestadas.Negocio.Exceptions;
using MySqlConnector;

namespace CoisasEmprestadas.UI.Helpers;

internal static class TratadorErros
{
    public static void Mostrar(Exception excecao, IWin32Window? janela = null)
    {
        switch (excecao)
        {
            case RegraNegocioException regra:
                Exibir(janela, regra.Message, "Validação", MessageBoxIcon.Warning);
                break;

            case MySqlException mySql:
                Exibir(janela, TraduzirMySql(mySql), "Banco de dados", MessageBoxIcon.Error);
                break;

            case ConfigurationErrorsException configuracao:
                Exibir(janela, configuracao.Message, "Configuração", MessageBoxIcon.Error);
                break;

            case InvalidOperationException operacao:
                Exibir(janela, operacao.Message, "Operação não permitida", MessageBoxIcon.Warning);
                break;

            default:
                Exibir(janela, "Não foi possível concluir a operação. Tente novamente ou contate o suporte.",
                    "Erro", MessageBoxIcon.Error);
                break;
        }
    }

    private static void Exibir(IWin32Window? janela, string mensagem, string titulo, MessageBoxIcon icone)
    {
        if (janela is null)
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, icone);
        else
            MessageBox.Show(janela, mensagem, titulo, MessageBoxButtons.OK, icone);
    }

    private static string TraduzirMySql(MySqlException excecao) => excecao.Number switch
    {
        1042 or 2002 or 2003 =>
            "Não foi possível conectar ao servidor de banco de dados. Verifique se o MySQL/MariaDB do XAMPP está em execução.",
        1045 =>
            "Acesso negado ao banco de dados. Confira usuário e senha na connection string do App.config.",
        1049 =>
            "O banco de dados informado não existe. Verifique o nome na connection string do App.config.",
        1062 =>
            "Já existe um registro com estes dados.",
        1146 =>
            "A estrutura do banco de dados não foi encontrada. Reinicie a aplicação para recriá-la.",
        1265 or 1406 =>
            "Um dos valores informados é maior do que o campo do banco de dados permite.",
        3819 =>
            "Os dados informados violam uma restrição de integridade do banco de dados.",
        _ =>
            $"Ocorreu um erro no banco de dados (código {excecao.Number}). Tente novamente."
    };
}

namespace CoisasEmprestadas.Negocio.Exceptions;

public class RegraNegocioException : Exception
{
    public RegraNegocioException(string mensagem) : base(mensagem)
    {
    }

    public RegraNegocioException(string mensagem, Exception innerException)
        : base(mensagem, innerException)
    {
    }
}

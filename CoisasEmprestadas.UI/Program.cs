using CoisasEmprestadas.Dados.Repositories;
using CoisasEmprestadas.Negocio.Repositories;
using CoisasEmprestadas.Negocio.Services;
using CoisasEmprestadas.UI.Forms;

namespace CoisasEmprestadas.UI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        IEmprestimoRepository repositorio = new EmprestimoRepository();
        var servico = new EmprestimoService(repositorio);

        Application.Run(new FrmPrincipal(servico));
    }
}

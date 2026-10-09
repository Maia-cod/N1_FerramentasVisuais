using CoisasEmprestadas.Negocio.Models;
using CoisasEmprestadas.Negocio.Services;
using CoisasEmprestadas.UI.Helpers;

namespace CoisasEmprestadas.UI.Forms;

public partial class FrmDevolucao : Form
{
    private readonly Emprestimo _emprestimo;
    private readonly EmprestimoService _servico;

    public FrmDevolucao()
    {
        InitializeComponent();
        _emprestimo = null!;
        _servico = null!;
    }

    public FrmDevolucao(Emprestimo emprestimo, EmprestimoService servico) : this()
    {
        _emprestimo = emprestimo ?? throw new ArgumentNullException(nameof(emprestimo));
        _servico = servico ?? throw new ArgumentNullException(nameof(servico));
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        lblTitulo.Text = $"Item: {_emprestimo.Item}{Environment.NewLine}Amigo: {_emprestimo.Amigo}";
        dtpDataDevolucao.MaxDate = DateTime.Today;
        dtpDataDevolucao.Value = DateTime.Today;
    }

    private void btnConfirmar_Click(object? sender, EventArgs e)
    {
        try
        {
            _servico.RegistrarDevolucao(_emprestimo.Id, dtpDataDevolucao.Value.Date);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception excecao)
        {
            TratadorErros.Mostrar(excecao, this);
        }
    }
}

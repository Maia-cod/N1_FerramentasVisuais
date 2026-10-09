using System.Globalization;
using CoisasEmprestadas.Negocio.Models;
using CoisasEmprestadas.Negocio.Services;
using CoisasEmprestadas.UI.Helpers;

namespace CoisasEmprestadas.UI.Forms;

public partial class FrmEmprestimo : Form
{
    private const string FormatoData = "dd/MM/yyyy";

    private readonly EmprestimoService _servico;

    public FrmEmprestimo()
    {
        InitializeComponent();
        _servico = null!;
    }

    public FrmEmprestimo(EmprestimoService servico) : this()
    {
        _servico = servico ?? throw new ArgumentNullException(nameof(servico));
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        mskDataEmprestimo.Text = DateTime.Today.ToString(FormatoData);
        mskDataPrevista.Text = DateTime.Today.AddDays(7).ToString(FormatoData);
        mskDataPrevista.Enabled = false;
        txtItem.Focus();
    }

    private void chkDataPrevista_CheckedChanged(object? sender, EventArgs e)
    {
        mskDataPrevista.Enabled = chkDataPrevista.Checked;

        if (chkDataPrevista.Checked)
            mskDataPrevista.Focus();
    }

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        errEmprestimo.Clear();

        if (!ValidarFormulario())
            return;

        var emprestimo = new Emprestimo
        {
            Item = txtItem.Text.Trim(),
            Amigo = txtAmigo.Text.Trim(),
            Contato = string.IsNullOrWhiteSpace(mskContato.Text) ? null : mskContato.Text.Trim(),
            DataEmprestimo = ConverterData(mskDataEmprestimo.Text),
            DataDevolucaoPrevista = chkDataPrevista.Checked ? ConverterData(mskDataPrevista.Text) : null
        };

        try
        {
            _servico.Registrar(emprestimo);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception excecao)
        {
            TratadorErros.Mostrar(excecao, this);
        }
    }

    private bool ValidarFormulario()
    {
        var valido = true;
        Control? primeiroInvalido = null;

        if (string.IsNullOrWhiteSpace(txtItem.Text))
        {
            errEmprestimo.SetError(txtItem, "Informe o item que foi emprestado.");
            primeiroInvalido ??= txtItem;
            valido = false;
        }

        if (string.IsNullOrWhiteSpace(txtAmigo.Text))
        {
            errEmprestimo.SetError(txtAmigo, "Informe o nome do amigo.");
            primeiroInvalido ??= txtAmigo;
            valido = false;
        }

        if (!mskContato.MaskCompleted && !string.IsNullOrWhiteSpace(mskContato.Text))
        {
            errEmprestimo.SetError(mskContato, "Preencha o telefone completo ou deixe em branco.");
            primeiroInvalido ??= mskContato;
            valido = false;
        }

        if (!TentarConverterData(mskDataEmprestimo.Text, out _))
        {
            errEmprestimo.SetError(mskDataEmprestimo, "Informe uma data válida (dd/mm/aaaa).");
            primeiroInvalido ??= mskDataEmprestimo;
            valido = false;
        }

        if (chkDataPrevista.Checked && !TentarConverterData(mskDataPrevista.Text, out _))
        {
            errEmprestimo.SetError(mskDataPrevista, "Informe uma data válida (dd/mm/aaaa).");
            primeiroInvalido ??= mskDataPrevista;
            valido = false;
        }

        if (!valido)
            primeiroInvalido?.Focus();

        return valido;
    }

    private static bool TentarConverterData(string texto, out DateTime data)
    {
        return DateTime.TryParseExact(texto, FormatoData, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out data);
    }

    private static DateTime ConverterData(string texto)
    {
        return DateTime.ParseExact(texto, FormatoData, CultureInfo.InvariantCulture);
    }
}

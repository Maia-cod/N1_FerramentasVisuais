using CoisasEmprestadas.Negocio.Models;
using CoisasEmprestadas.Negocio.Services;
using CoisasEmprestadas.UI.Helpers;

namespace CoisasEmprestadas.UI.Forms;

public partial class FrmPrincipal : Form
{
    private readonly EmprestimoService? _servico;

    public FrmPrincipal()
    {
        InitializeComponent();
        ConfigurarGrade();
    }

    public FrmPrincipal(EmprestimoService servico) : this()
    {
        _servico = servico ?? throw new ArgumentNullException(nameof(servico));
    }

    private void ConfigurarGrade()
    {
        dgvEmprestimos.AutoGenerateColumns = false;
        dgvEmprestimos.Columns.Clear();
        dgvEmprestimos.Columns.Add(CriarColuna("colId", "Id", 50));
        dgvEmprestimos.Columns.Add(CriarColuna("colItem", "Item", 180));
        dgvEmprestimos.Columns.Add(CriarColuna("colAmigo", "Amigo", 140));
        dgvEmprestimos.Columns.Add(CriarColuna("colContato", "Contato", 130));
        dgvEmprestimos.Columns.Add(CriarColuna("colDataEmprestimo", "Empréstimo", 90));
        dgvEmprestimos.Columns.Add(CriarColuna("colDataPrevista", "Devolução prevista", 120));
        dgvEmprestimos.Columns.Add(CriarColuna("colDataReal", "Devolvido em", 100));
        dgvEmprestimos.Columns.Add(CriarColuna("colSituacao", "Situação", 160, preencher: true));
    }

    private static DataGridViewTextBoxColumn CriarColuna(string nome, string titulo, int largura, bool preencher = false)
    {
        var coluna = new DataGridViewTextBoxColumn
        {
            Name = nome,
            HeaderText = titulo,
            Width = largura,
            ReadOnly = true
        };

        if (preencher)
            coluna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        return coluna;
    }

    private void FrmPrincipal_Load(object? sender, EventArgs e)
    {
        if (_servico is null)
            return;

        try
        {
            _servico.GarantirEstrutura();
            CarregarEmprestimos();
            btnNovoEmprestimo.Focus();
        }
        catch (Exception excecao)
        {
            TratadorErros.Mostrar(excecao, this);
        }
    }

    private void CarregarEmprestimos()
    {
        if (_servico is null)
            return;

        dgvEmprestimos.Rows.Clear();

        foreach (var emprestimo in _servico.Listar())
        {
            var indice = dgvEmprestimos.Rows.Add(
                emprestimo.Id,
                emprestimo.Item,
                emprestimo.Amigo,
                string.IsNullOrWhiteSpace(emprestimo.Contato) ? "-" : emprestimo.Contato,
                emprestimo.DataEmprestimo.ToString("dd/MM/yyyy"),
                emprestimo.DataDevolucaoPrevista?.ToString("dd/MM/yyyy") ?? "-",
                emprestimo.DataDevolucaoReal?.ToString("dd/MM/yyyy") ?? "-",
                DescreverSituacao(emprestimo));

            var linha = dgvEmprestimos.Rows[indice];
            linha.Tag = emprestimo;
            EstilizarLinha(linha, emprestimo);
        }

        AtualizarResumo();
        AtualizarBotoes();
    }

    private static void EstilizarLinha(DataGridViewRow linha, Emprestimo emprestimo)
    {
        if (emprestimo.Atrasado)
        {
            linha.DefaultCellStyle.BackColor = Color.MistyRose;
            linha.DefaultCellStyle.ForeColor = Color.FromArgb(160, 0, 0);
            linha.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
            linha.DefaultCellStyle.SelectionForeColor = Color.White;
            linha.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }
        else if (emprestimo.Devolvido)
        {
            linha.DefaultCellStyle.ForeColor = Color.Gray;
        }
    }

    private static string DescreverSituacao(Emprestimo emprestimo)
    {
        if (emprestimo.Devolvido)
            return "Devolvido";

        if (emprestimo.Atrasado)
            return $"Atrasado ({emprestimo.DiasDeAtraso} dia(s))";

        return "Emprestado";
    }

    private void AtualizarResumo()
    {
        var emprestimos = dgvEmprestimos.Rows
            .Cast<DataGridViewRow>()
            .Select(linha => linha.Tag as Emprestimo)
            .Where(emprestimo => emprestimo is not null)
            .ToList();

        var total = emprestimos.Count;
        var devolvidos = emprestimos.Count(e => e!.Devolvido);
        var atrasados = emprestimos.Count(e => e!.Atrasado);

        lblResumo.Text =
            $"Total: {total}   |   Em aberto: {total - devolvidos}   |   Em atraso: {atrasados}   |   Devolvidos: {devolvidos}";
    }

    private void AtualizarBotoes()
    {
        var selecionado = ObterEmprestimoSelecionado();
        btnRegistrarDevolucao.Enabled = selecionado is { Devolvido: false };
    }

    private Emprestimo? ObterEmprestimoSelecionado()
    {
        return dgvEmprestimos.CurrentRow?.Tag as Emprestimo;
    }

    private void btnNovoEmprestimo_Click(object? sender, EventArgs e)
    {
        if (_servico is null)
            return;

        using var dialogo = new FrmEmprestimo(_servico);

        if (dialogo.ShowDialog(this) == DialogResult.OK)
            CarregarEmprestimosComTratamento();
    }

    private void btnRegistrarDevolucao_Click(object? sender, EventArgs e)
    {
        RegistrarDevolucaoSelecionada();
    }

    private void dgvEmprestimos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            RegistrarDevolucaoSelecionada();
    }

    private void RegistrarDevolucaoSelecionada()
    {
        if (_servico is null)
            return;

        var selecionado = ObterEmprestimoSelecionado();

        if (selecionado is null)
        {
            MessageBox.Show(this, "Selecione um empréstimo na lista.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (selecionado.Devolvido)
        {
            MessageBox.Show(this, "Este item já foi registrado como devolvido.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialogo = new FrmDevolucao(selecionado, _servico);

        if (dialogo.ShowDialog(this) == DialogResult.OK)
            CarregarEmprestimosComTratamento();
    }

    private void btnAtualizar_Click(object? sender, EventArgs e)
    {
        CarregarEmprestimosComTratamento();
    }

    private void CarregarEmprestimosComTratamento()
    {
        try
        {
            CarregarEmprestimos();
        }
        catch (Exception excecao)
        {
            TratadorErros.Mostrar(excecao, this);
        }
    }

    private void btnSair_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void dgvEmprestimos_SelectionChanged(object? sender, EventArgs e)
    {
        AtualizarBotoes();
    }

    private void FrmPrincipal_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            e.Handled = true;
            CarregarEmprestimosComTratamento();
        }
    }
}

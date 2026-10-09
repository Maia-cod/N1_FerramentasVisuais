using System.ComponentModel;

namespace CoisasEmprestadas.UI.Forms;

partial class FrmPrincipal
{
    private IContainer components = null;

    private TableLayoutPanel tblPrincipal;
    private FlowLayoutPanel flpAcoes;
    private Button btnNovoEmprestimo;
    private Button btnRegistrarDevolucao;
    private Button btnAtualizar;
    private Button btnSair;
    private DataGridView dgvEmprestimos;
    private Label lblResumo;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        tblPrincipal = new TableLayoutPanel();
        flpAcoes = new FlowLayoutPanel();
        btnNovoEmprestimo = new Button();
        btnRegistrarDevolucao = new Button();
        btnAtualizar = new Button();
        btnSair = new Button();
        dgvEmprestimos = new DataGridView();
        lblResumo = new Label();
        tblPrincipal.SuspendLayout();
        flpAcoes.SuspendLayout();
        ((ISupportInitialize)dgvEmprestimos).BeginInit();
        SuspendLayout();
        // 
        // tblPrincipal
        // 
        tblPrincipal.ColumnCount = 1;
        tblPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblPrincipal.Controls.Add(flpAcoes, 0, 0);
        tblPrincipal.Controls.Add(dgvEmprestimos, 0, 1);
        tblPrincipal.Controls.Add(lblResumo, 0, 2);
        tblPrincipal.Dock = DockStyle.Fill;
        tblPrincipal.Location = new Point(0, 0);
        tblPrincipal.Name = "tblPrincipal";
        tblPrincipal.RowCount = 3;
        tblPrincipal.RowStyles.Add(new RowStyle());
        tblPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tblPrincipal.RowStyles.Add(new RowStyle());
        tblPrincipal.Size = new Size(900, 560);
        tblPrincipal.TabIndex = 0;
        // 
        // flpAcoes
        // 
        flpAcoes.AutoSize = true;
        flpAcoes.Controls.Add(btnNovoEmprestimo);
        flpAcoes.Controls.Add(btnRegistrarDevolucao);
        flpAcoes.Controls.Add(btnAtualizar);
        flpAcoes.Controls.Add(btnSair);
        flpAcoes.Dock = DockStyle.Fill;
        flpAcoes.Location = new Point(3, 3);
        flpAcoes.Name = "flpAcoes";
        flpAcoes.Padding = new Padding(8);
        flpAcoes.Size = new Size(894, 54);
        flpAcoes.TabIndex = 0;
        flpAcoes.WrapContents = false;
        // 
        // btnNovoEmprestimo
        // 
        btnNovoEmprestimo.AutoSize = true;
        btnNovoEmprestimo.Location = new Point(12, 12);
        btnNovoEmprestimo.Margin = new Padding(4);
        btnNovoEmprestimo.Name = "btnNovoEmprestimo";
        btnNovoEmprestimo.Padding = new Padding(6, 2, 6, 2);
        btnNovoEmprestimo.Size = new Size(140, 30);
        btnNovoEmprestimo.TabIndex = 0;
        btnNovoEmprestimo.Text = "&Novo empréstimo";
        btnNovoEmprestimo.UseVisualStyleBackColor = true;
        btnNovoEmprestimo.Click += btnNovoEmprestimo_Click;
        // 
        // btnRegistrarDevolucao
        // 
        btnRegistrarDevolucao.AutoSize = true;
        btnRegistrarDevolucao.Enabled = false;
        btnRegistrarDevolucao.Location = new Point(160, 12);
        btnRegistrarDevolucao.Margin = new Padding(4);
        btnRegistrarDevolucao.Name = "btnRegistrarDevolucao";
        btnRegistrarDevolucao.Padding = new Padding(6, 2, 6, 2);
        btnRegistrarDevolucao.Size = new Size(150, 30);
        btnRegistrarDevolucao.TabIndex = 1;
        btnRegistrarDevolucao.Text = "&Registrar devolução";
        btnRegistrarDevolucao.UseVisualStyleBackColor = true;
        btnRegistrarDevolucao.Click += btnRegistrarDevolucao_Click;
        // 
        // btnAtualizar
        // 
        btnAtualizar.AutoSize = true;
        btnAtualizar.Location = new Point(318, 12);
        btnAtualizar.Margin = new Padding(4);
        btnAtualizar.Name = "btnAtualizar";
        btnAtualizar.Padding = new Padding(6, 2, 6, 2);
        btnAtualizar.Size = new Size(120, 30);
        btnAtualizar.TabIndex = 2;
        btnAtualizar.Text = "At&ualizar (F5)";
        btnAtualizar.UseVisualStyleBackColor = true;
        btnAtualizar.Click += btnAtualizar_Click;
        // 
        // btnSair
        // 
        btnSair.AutoSize = true;
        btnSair.Location = new Point(446, 12);
        btnSair.Margin = new Padding(4);
        btnSair.Name = "btnSair";
        btnSair.Padding = new Padding(6, 2, 6, 2);
        btnSair.Size = new Size(90, 30);
        btnSair.TabIndex = 3;
        btnSair.Text = "&Sair (Esc)";
        btnSair.UseVisualStyleBackColor = true;
        btnSair.Click += btnSair_Click;
        // 
        // dgvEmprestimos
        // 
        dgvEmprestimos.AllowUserToAddRows = false;
        dgvEmprestimos.AllowUserToDeleteRows = false;
        dgvEmprestimos.AllowUserToResizeRows = false;
        dgvEmprestimos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvEmprestimos.Dock = DockStyle.Fill;
        dgvEmprestimos.Location = new Point(3, 63);
        dgvEmprestimos.MultiSelect = false;
        dgvEmprestimos.Name = "dgvEmprestimos";
        dgvEmprestimos.ReadOnly = true;
        dgvEmprestimos.RowHeadersVisible = false;
        dgvEmprestimos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEmprestimos.Size = new Size(894, 466);
        dgvEmprestimos.TabIndex = 4;
        dgvEmprestimos.CellDoubleClick += dgvEmprestimos_CellDoubleClick;
        dgvEmprestimos.SelectionChanged += dgvEmprestimos_SelectionChanged;
        // 
        // lblResumo
        // 
        lblResumo.Dock = DockStyle.Fill;
        lblResumo.Location = new Point(3, 532);
        lblResumo.Name = "lblResumo";
        lblResumo.Padding = new Padding(8, 4, 8, 4);
        lblResumo.Size = new Size(894, 25);
        lblResumo.TabIndex = 5;
        lblResumo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // FrmPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnSair;
        ClientSize = new Size(900, 560);
        Controls.Add(tblPrincipal);
        KeyPreview = true;
        MinimumSize = new Size(720, 420);
        Name = "FrmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Coisas Emprestadas";
        KeyDown += FrmPrincipal_KeyDown;
        Load += FrmPrincipal_Load;
        tblPrincipal.ResumeLayout(false);
        tblPrincipal.PerformLayout();
        flpAcoes.ResumeLayout(false);
        flpAcoes.PerformLayout();
        ((ISupportInitialize)dgvEmprestimos).EndInit();
        ResumeLayout(false);
    }
}

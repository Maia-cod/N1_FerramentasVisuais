using System.ComponentModel;

namespace CoisasEmprestadas.UI.Forms;

partial class FrmEmprestimo
{
    private IContainer components = null;

    private Label lblItem;
    private TextBox txtItem;
    private Label lblAmigo;
    private TextBox txtAmigo;
    private Label lblContato;
    private MaskedTextBox mskContato;
    private Label lblDataEmprestimo;
    private MaskedTextBox mskDataEmprestimo;
    private CheckBox chkDataPrevista;
    private Label lblDataPrevista;
    private MaskedTextBox mskDataPrevista;
    private Button btnSalvar;
    private Button btnCancelar;
    private ErrorProvider errEmprestimo;

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
        lblItem = new Label();
        txtItem = new TextBox();
        lblAmigo = new Label();
        txtAmigo = new TextBox();
        lblContato = new Label();
        mskContato = new MaskedTextBox();
        lblDataEmprestimo = new Label();
        mskDataEmprestimo = new MaskedTextBox();
        chkDataPrevista = new CheckBox();
        lblDataPrevista = new Label();
        mskDataPrevista = new MaskedTextBox();
        btnSalvar = new Button();
        btnCancelar = new Button();
        errEmprestimo = new ErrorProvider(components);
        ((ISupportInitialize)errEmprestimo).BeginInit();
        SuspendLayout();
        // 
        // lblItem
        // 
        lblItem.AutoSize = true;
        lblItem.Location = new Point(16, 21);
        lblItem.Name = "lblItem";
        lblItem.Size = new Size(60, 15);
        lblItem.TabIndex = 0;
        lblItem.Text = "Item *";
        // 
        // txtItem
        // 
        txtItem.Location = new Point(140, 18);
        txtItem.MaxLength = 150;
        txtItem.Name = "txtItem";
        txtItem.Size = new Size(220, 23);
        txtItem.TabIndex = 1;
        // 
        // lblAmigo
        // 
        lblAmigo.AutoSize = true;
        lblAmigo.Location = new Point(16, 53);
        lblAmigo.Name = "lblAmigo";
        lblAmigo.Size = new Size(70, 15);
        lblAmigo.TabIndex = 2;
        lblAmigo.Text = "Amigo *";
        // 
        // txtAmigo
        // 
        txtAmigo.Location = new Point(140, 50);
        txtAmigo.MaxLength = 120;
        txtAmigo.Name = "txtAmigo";
        txtAmigo.Size = new Size(220, 23);
        txtAmigo.TabIndex = 3;
        // 
        // lblContato
        // 
        lblContato.AutoSize = true;
        lblContato.Location = new Point(16, 85);
        lblContato.Name = "lblContato";
        lblContato.Size = new Size(75, 15);
        lblContato.TabIndex = 4;
        lblContato.Text = "Contato";
        // 
        // mskContato
        // 
        mskContato.Location = new Point(140, 82);
        mskContato.Mask = "(00) 00000-0000";
        mskContato.Name = "mskContato";
        mskContato.Size = new Size(220, 23);
        mskContato.TabIndex = 5;
        mskContato.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        // 
        // lblDataEmprestimo
        // 
        lblDataEmprestimo.AutoSize = true;
        lblDataEmprestimo.Location = new Point(16, 117);
        lblDataEmprestimo.Name = "lblDataEmprestimo";
        lblDataEmprestimo.Size = new Size(90, 15);
        lblDataEmprestimo.TabIndex = 6;
        lblDataEmprestimo.Text = "Empréstimo *";
        // 
        // mskDataEmprestimo
        // 
        mskDataEmprestimo.Location = new Point(140, 114);
        mskDataEmprestimo.Mask = "00/00/0000";
        mskDataEmprestimo.Name = "mskDataEmprestimo";
        mskDataEmprestimo.Size = new Size(220, 23);
        mskDataEmprestimo.TabIndex = 7;
        mskDataEmprestimo.TextMaskFormat = MaskFormat.IncludeLiterals;
        // 
        // chkDataPrevista
        // 
        chkDataPrevista.AutoSize = true;
        chkDataPrevista.Location = new Point(140, 147);
        chkDataPrevista.Name = "chkDataPrevista";
        chkDataPrevista.Size = new Size(190, 19);
        chkDataPrevista.TabIndex = 8;
        chkDataPrevista.Text = "Combinar data de devolução";
        chkDataPrevista.UseVisualStyleBackColor = true;
        chkDataPrevista.CheckedChanged += chkDataPrevista_CheckedChanged;
        // 
        // lblDataPrevista
        // 
        lblDataPrevista.AutoSize = true;
        lblDataPrevista.Location = new Point(16, 179);
        lblDataPrevista.Name = "lblDataPrevista";
        lblDataPrevista.Size = new Size(110, 15);
        lblDataPrevista.TabIndex = 9;
        lblDataPrevista.Text = "Devolução prevista";
        // 
        // mskDataPrevista
        // 
        mskDataPrevista.Enabled = false;
        mskDataPrevista.Location = new Point(140, 176);
        mskDataPrevista.Mask = "00/00/0000";
        mskDataPrevista.Name = "mskDataPrevista";
        mskDataPrevista.Size = new Size(220, 23);
        mskDataPrevista.TabIndex = 10;
        mskDataPrevista.TextMaskFormat = MaskFormat.IncludeLiterals;
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(190, 216);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(80, 28);
        btnSalvar.TabIndex = 11;
        btnSalvar.Text = "&Salvar";
        btnSalvar.UseVisualStyleBackColor = true;
        btnSalvar.Click += btnSalvar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(280, 216);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(80, 28);
        btnCancelar.TabIndex = 12;
        btnCancelar.Text = "&Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        // 
        // errEmprestimo
        // 
        errEmprestimo.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        errEmprestimo.ContainerControl = this;
        // 
        // FrmEmprestimo
        // 
        AcceptButton = btnSalvar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(378, 262);
        Controls.Add(lblItem);
        Controls.Add(txtItem);
        Controls.Add(lblAmigo);
        Controls.Add(txtAmigo);
        Controls.Add(lblContato);
        Controls.Add(mskContato);
        Controls.Add(lblDataEmprestimo);
        Controls.Add(mskDataEmprestimo);
        Controls.Add(chkDataPrevista);
        Controls.Add(lblDataPrevista);
        Controls.Add(mskDataPrevista);
        Controls.Add(btnSalvar);
        Controls.Add(btnCancelar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmEmprestimo";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Novo empréstimo";
        ((ISupportInitialize)errEmprestimo).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}

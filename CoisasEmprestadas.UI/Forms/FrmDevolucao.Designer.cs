using System.ComponentModel;

namespace CoisasEmprestadas.UI.Forms;

partial class FrmDevolucao
{
    private IContainer components = null;

    private Label lblTitulo;
    private Label lblData;
    private DateTimePicker dtpDataDevolucao;
    private Button btnConfirmar;
    private Button btnCancelar;

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
        lblTitulo = new Label();
        lblData = new Label();
        dtpDataDevolucao = new DateTimePicker();
        btnConfirmar = new Button();
        btnCancelar = new Button();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTitulo.Location = new Point(16, 16);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(200, 15);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Item";
        // 
        // lblData
        // 
        lblData.AutoSize = true;
        lblData.Location = new Point(16, 62);
        lblData.Name = "lblData";
        lblData.Size = new Size(110, 15);
        lblData.TabIndex = 1;
        lblData.Text = "Data da devolução";
        // 
        // dtpDataDevolucao
        // 
        dtpDataDevolucao.Format = DateTimePickerFormat.Short;
        dtpDataDevolucao.Location = new Point(140, 58);
        dtpDataDevolucao.Name = "dtpDataDevolucao";
        dtpDataDevolucao.Size = new Size(220, 23);
        dtpDataDevolucao.TabIndex = 2;
        // 
        // btnConfirmar
        // 
        btnConfirmar.Location = new Point(190, 104);
        btnConfirmar.Name = "btnConfirmar";
        btnConfirmar.Size = new Size(80, 28);
        btnConfirmar.TabIndex = 3;
        btnConfirmar.Text = "&Confirmar";
        btnConfirmar.UseVisualStyleBackColor = true;
        btnConfirmar.Click += btnConfirmar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(280, 104);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(80, 28);
        btnCancelar.TabIndex = 4;
        btnCancelar.Text = "&Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        // 
        // FrmDevolucao
        // 
        AcceptButton = btnConfirmar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(378, 150);
        Controls.Add(lblTitulo);
        Controls.Add(lblData);
        Controls.Add(dtpDataDevolucao);
        Controls.Add(btnConfirmar);
        Controls.Add(btnCancelar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmDevolucao";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Registrar devolução";
        ResumeLayout(false);
        PerformLayout();
    }
}

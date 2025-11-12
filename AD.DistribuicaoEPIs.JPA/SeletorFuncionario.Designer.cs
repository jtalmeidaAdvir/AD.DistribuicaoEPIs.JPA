
namespace AD.DistribuicaoEPIs.JPA
{
    partial class SeletorFuncionario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBoxPesquisa = new System.Windows.Forms.GroupBox();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.lblPesquisa = new System.Windows.Forms.Label();
            this.listViewFuncionarios = new System.Windows.Forms.ListView();
            this.colCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBoxPesquisa.SuspendLayout();
            this.SuspendLayout();

            System.Drawing.Font defaultFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            System.Drawing.Font headerFont = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // groupBoxPesquisa
            // 
            this.groupBoxPesquisa.Controls.Add(this.lblPesquisa);
            this.groupBoxPesquisa.Controls.Add(this.txtPesquisa);
            this.groupBoxPesquisa.Font = headerFont;
            this.groupBoxPesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.groupBoxPesquisa.Location = new System.Drawing.Point(20, 20);
            this.groupBoxPesquisa.Name = "groupBoxPesquisa";
            this.groupBoxPesquisa.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxPesquisa.Size = new System.Drawing.Size(660, 70);
            this.groupBoxPesquisa.TabIndex = 0;
            this.groupBoxPesquisa.TabStop = false;
            this.groupBoxPesquisa.Text = "🔍 Pesquisar Funcionário";
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Font = defaultFont;
            this.txtPesquisa.Location = new System.Drawing.Point(90, 30);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(550, 25);
            this.txtPesquisa.TabIndex = 1;
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);
            // 
            // lblPesquisa
            // 
            this.lblPesquisa.AutoSize = true;
            this.lblPesquisa.Font = defaultFont;
            this.lblPesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPesquisa.Location = new System.Drawing.Point(20, 33);
            this.lblPesquisa.Name = "lblPesquisa";
            this.lblPesquisa.Size = new System.Drawing.Size(64, 17);
            this.lblPesquisa.TabIndex = 0;
            this.lblPesquisa.Text = "Pesquisa:";
            // 
            // listViewFuncionarios
            // 
            this.listViewFuncionarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCodigo,
            this.colNome});
            this.listViewFuncionarios.Font = defaultFont;
            this.listViewFuncionarios.FullRowSelect = true;
            this.listViewFuncionarios.GridLines = true;
            this.listViewFuncionarios.HideSelection = false;
            this.listViewFuncionarios.Location = new System.Drawing.Point(20, 100);
            this.listViewFuncionarios.MultiSelect = false;
            this.listViewFuncionarios.Name = "listViewFuncionarios";
            this.listViewFuncionarios.Size = new System.Drawing.Size(660, 350);
            this.listViewFuncionarios.TabIndex = 1;
            this.listViewFuncionarios.UseCompatibleStateImageBehavior = false;
            this.listViewFuncionarios.View = System.Windows.Forms.View.Details;
            this.listViewFuncionarios.DoubleClick += new System.EventHandler(this.listViewFuncionarios_DoubleClick);
            // 
            // colCodigo
            // 
            this.colCodigo.Text = "Código";
            this.colCodigo.Width = 120;
            // 
            // colNome
            // 
            this.colNome.Text = "Nome";
            this.colNome.Width = 520;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelecionar.ForeColor = System.Drawing.Color.White;
            this.btnSelecionar.Location = new System.Drawing.Point(465, 465);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(105, 38);
            this.btnSelecionar.TabIndex = 2;
            this.btnSelecionar.Text = "✓ Selecionar";
            this.btnSelecionar.UseVisualStyleBackColor = false;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.Location = new System.Drawing.Point(576, 465);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 38);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // SeletorFuncionario
            // 
            this.AcceptButton = this.btnSelecionar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(700, 520);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSelecionar);
            this.Controls.Add(this.listViewFuncionarios);
            this.Controls.Add(this.groupBoxPesquisa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeletorFuncionario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecionar Funcionário";
            this.Load += new System.EventHandler(this.SeletorFuncionario_Load);
            this.groupBoxPesquisa.ResumeLayout(false);
            this.groupBoxPesquisa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPesquisa;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.Label lblPesquisa;
        private System.Windows.Forms.ListView listViewFuncionarios;
        private System.Windows.Forms.ColumnHeader colCodigo;
        private System.Windows.Forms.ColumnHeader colNome;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

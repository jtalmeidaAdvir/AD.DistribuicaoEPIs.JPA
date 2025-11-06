
namespace AD.DistribuicaoEPIs.JPA
{
    partial class EditorDistribuicaoEPIs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxColaborador = new System.Windows.Forms.GroupBox();
            this.txtColaborador = new System.Windows.Forms.TextBox();
            this.btnProcurarColaborador = new System.Windows.Forms.Button();
            this.lblColaborador = new System.Windows.Forms.Label();
            this.lblNomeColaborador = new System.Windows.Forms.Label();
            this.txtNomeColaborador = new System.Windows.Forms.TextBox();
            this.groupBoxEPIs = new System.Windows.Forms.GroupBox();
            this.dgvEPIs = new System.Windows.Forms.DataGridView();
            this.colArtigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdicionarEPI = new System.Windows.Forms.Button();
            this.btnRemoverEPI = new System.Windows.Forms.Button();
            this.groupBoxDataEntrega = new System.Windows.Forms.GroupBox();
            this.dtpDataEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblDataEntrega = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBoxColaborador.SuspendLayout();
            this.groupBoxEPIs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEPIs)).BeginInit();
            this.groupBoxDataEntrega.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxColaborador
            // 
            this.groupBoxColaborador.Controls.Add(this.txtNomeColaborador);
            this.groupBoxColaborador.Controls.Add(this.lblNomeColaborador);
            this.groupBoxColaborador.Controls.Add(this.btnProcurarColaborador);
            this.groupBoxColaborador.Controls.Add(this.txtColaborador);
            this.groupBoxColaborador.Controls.Add(this.lblColaborador);
            this.groupBoxColaborador.Location = new System.Drawing.Point(12, 12);
            this.groupBoxColaborador.Name = "groupBoxColaborador";
            this.groupBoxColaborador.Size = new System.Drawing.Size(760, 90);
            this.groupBoxColaborador.TabIndex = 0;
            this.groupBoxColaborador.TabStop = false;
            this.groupBoxColaborador.Text = "Colaborador Destinatário";
            // 
            // txtColaborador
            // 
            this.txtColaborador.Location = new System.Drawing.Point(100, 25);
            this.txtColaborador.Name = "txtColaborador";
            this.txtColaborador.Size = new System.Drawing.Size(150, 20);
            this.txtColaborador.TabIndex = 1;
            // 
            // btnProcurarColaborador
            // 
            this.btnProcurarColaborador.Location = new System.Drawing.Point(256, 23);
            this.btnProcurarColaborador.Name = "btnProcurarColaborador";
            this.btnProcurarColaborador.Size = new System.Drawing.Size(75, 23);
            this.btnProcurarColaborador.TabIndex = 2;
            this.btnProcurarColaborador.Text = "Procurar...";
            this.btnProcurarColaborador.UseVisualStyleBackColor = true;
            // 
            // lblColaborador
            // 
            this.lblColaborador.AutoSize = true;
            this.lblColaborador.Location = new System.Drawing.Point(15, 28);
            this.lblColaborador.Name = "lblColaborador";
            this.lblColaborador.Size = new System.Drawing.Size(68, 13);
            this.lblColaborador.TabIndex = 0;
            this.lblColaborador.Text = "Colaborador:";
            // 
            // lblNomeColaborador
            // 
            this.lblNomeColaborador.AutoSize = true;
            this.lblNomeColaborador.Location = new System.Drawing.Point(15, 58);
            this.lblNomeColaborador.Name = "lblNomeColaborador";
            this.lblNomeColaborador.Size = new System.Drawing.Size(38, 13);
            this.lblNomeColaborador.TabIndex = 3;
            this.lblNomeColaborador.Text = "Nome:";
            // 
            // txtNomeColaborador
            // 
            this.txtNomeColaborador.Location = new System.Drawing.Point(100, 55);
            this.txtNomeColaborador.Name = "txtNomeColaborador";
            this.txtNomeColaborador.ReadOnly = true;
            this.txtNomeColaborador.Size = new System.Drawing.Size(640, 20);
            this.txtNomeColaborador.TabIndex = 4;
            // 
            // groupBoxEPIs
            // 
            this.groupBoxEPIs.Controls.Add(this.btnRemoverEPI);
            this.groupBoxEPIs.Controls.Add(this.btnAdicionarEPI);
            this.groupBoxEPIs.Controls.Add(this.dgvEPIs);
            this.groupBoxEPIs.Location = new System.Drawing.Point(12, 108);
            this.groupBoxEPIs.Name = "groupBoxEPIs";
            this.groupBoxEPIs.Size = new System.Drawing.Size(760, 280);
            this.groupBoxEPIs.TabIndex = 1;
            this.groupBoxEPIs.TabStop = false;
            this.groupBoxEPIs.Text = "EPIs a Distribuir";
            // 
            // dgvEPIs
            // 
            this.dgvEPIs.AllowUserToAddRows = false;
            this.dgvEPIs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEPIs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colArtigo,
            this.colDescricao,
            this.colQuantidade,
            this.colUnidade});
            this.dgvEPIs.Location = new System.Drawing.Point(15, 25);
            this.dgvEPIs.Name = "dgvEPIs";
            this.dgvEPIs.Size = new System.Drawing.Size(725, 210);
            this.dgvEPIs.TabIndex = 0;
            // Pré-preencher com EPIs padrão
            this.dgvEPIs.Rows.Add("EPI001", "Capacete de Proteção c/ ou s/ francalete", "0", "UN");
            this.dgvEPIs.Rows.Add("EPI002", "Luvas de proteção mecânica e/ou química", "0", "PAR");
            this.dgvEPIs.Rows.Add("EPI003", "Capa Chuva ou Fato oleado", "0", "UN");
            this.dgvEPIs.Rows.Add("EPI004", "Botas com biqueira e palmilha de aço", "0", "PAR");
            this.dgvEPIs.Rows.Add("EPI005", "Colete refletor", "0", "UN");
            this.dgvEPIs.Rows.Add("EPI006", "Protetor auricular", "0", "UN");
            // 
            // colArtigo
            // 
            this.colArtigo.HeaderText = "Artigo";
            this.colArtigo.Name = "colArtigo";
            this.colArtigo.ReadOnly = true;
            this.colArtigo.Width = 120;
            // 
            // colDescricao
            // 
            this.colDescricao.HeaderText = "Descrição";
            this.colDescricao.Name = "colDescricao";
            this.colDescricao.ReadOnly = true;
            this.colDescricao.Width = 350;
            // 
            // colQuantidade
            // 
            this.colQuantidade.HeaderText = "Quantidade";
            this.colQuantidade.Name = "colQuantidade";
            this.colQuantidade.Width = 100;
            // 
            // colUnidade
            // 
            this.colUnidade.HeaderText = "Unidade";
            this.colUnidade.Name = "colUnidade";
            this.colUnidade.ReadOnly = true;
            this.colUnidade.Width = 80;
            // 
            // btnAdicionarEPI
            // 
            this.btnAdicionarEPI.Location = new System.Drawing.Point(15, 241);
            this.btnAdicionarEPI.Name = "btnAdicionarEPI";
            this.btnAdicionarEPI.Size = new System.Drawing.Size(100, 25);
            this.btnAdicionarEPI.TabIndex = 1;
            this.btnAdicionarEPI.Text = "Adicionar EPI";
            this.btnAdicionarEPI.UseVisualStyleBackColor = true;
            this.btnAdicionarEPI.Visible = false;
            // 
            // btnRemoverEPI
            // 
            this.btnRemoverEPI.Location = new System.Drawing.Point(121, 241);
            this.btnRemoverEPI.Name = "btnRemoverEPI";
            this.btnRemoverEPI.Size = new System.Drawing.Size(100, 25);
            this.btnRemoverEPI.TabIndex = 2;
            this.btnRemoverEPI.Text = "Remover EPI";
            this.btnRemoverEPI.UseVisualStyleBackColor = true;
            this.btnRemoverEPI.Visible = false;
            // 
            // groupBoxDataEntrega
            // 
            this.groupBoxDataEntrega.Controls.Add(this.lblDataEntrega);
            this.groupBoxDataEntrega.Controls.Add(this.dtpDataEntrega);
            this.groupBoxDataEntrega.Location = new System.Drawing.Point(12, 394);
            this.groupBoxDataEntrega.Name = "groupBoxDataEntrega";
            this.groupBoxDataEntrega.Size = new System.Drawing.Size(760, 60);
            this.groupBoxDataEntrega.TabIndex = 2;
            this.groupBoxDataEntrega.TabStop = false;
            this.groupBoxDataEntrega.Text = "Data de Entrega";
            // 
            // dtpDataEntrega
            // 
            this.dtpDataEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataEntrega.Location = new System.Drawing.Point(100, 25);
            this.dtpDataEntrega.Name = "dtpDataEntrega";
            this.dtpDataEntrega.Size = new System.Drawing.Size(150, 20);
            this.dtpDataEntrega.TabIndex = 1;
            // 
            // lblDataEntrega
            // 
            this.lblDataEntrega.AutoSize = true;
            this.lblDataEntrega.Location = new System.Drawing.Point(15, 28);
            this.lblDataEntrega.Name = "lblDataEntrega";
            this.lblDataEntrega.Size = new System.Drawing.Size(79, 13);
            this.lblDataEntrega.TabIndex = 0;
            this.lblDataEntrega.Text = "Data Entrega:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(597, 465);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 30);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(688, 465);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 30);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // EditorDistribuicaoEPIs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 507);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBoxDataEntrega);
            this.Controls.Add(this.groupBoxEPIs);
            this.Controls.Add(this.groupBoxColaborador);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditorDistribuicaoEPIs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distribuição de EPIs";
            this.groupBoxColaborador.ResumeLayout(false);
            this.groupBoxColaborador.PerformLayout();
            this.groupBoxEPIs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEPIs)).EndInit();
            this.groupBoxDataEntrega.ResumeLayout(false);
            this.groupBoxDataEntrega.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxColaborador;
        private System.Windows.Forms.TextBox txtNomeColaborador;
        private System.Windows.Forms.Label lblNomeColaborador;
        private System.Windows.Forms.Button btnProcurarColaborador;
        private System.Windows.Forms.TextBox txtColaborador;
        private System.Windows.Forms.Label lblColaborador;
        private System.Windows.Forms.GroupBox groupBoxEPIs;
        private System.Windows.Forms.Button btnRemoverEPI;
        private System.Windows.Forms.Button btnAdicionarEPI;
        private System.Windows.Forms.DataGridView dgvEPIs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArtigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnidade;
        private System.Windows.Forms.GroupBox groupBoxDataEntrega;
        private System.Windows.Forms.Label lblDataEntrega;
        private System.Windows.Forms.DateTimePicker dtpDataEntrega;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

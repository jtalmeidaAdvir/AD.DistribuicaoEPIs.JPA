using System;
using System.Data;
using System.Windows.Forms;

namespace AD.DistribuicaoEPIs.JPA
{
    public partial class HistoricoEPIsForm : Form
    {
        private DataTable _dados;

        public HistoricoEPIsForm(DataTable dados, string codigoColaborador, string nomeColaborador)
        {
            InitializeComponent();

            // Salvar DataTable para usar no Load
            _dados = dados;

            // Configurar informação do colaborador
            lblColaboradorInfo.Text = $"Colaborador: {codigoColaborador} - {nomeColaborador}";

            // Adicionar evento Load
            this.Load += HistoricoEPIsForm_Load;
        }

        private void HistoricoEPIsForm_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(_dados);
        }

        private void ConfigurarDataGridView(DataTable dados)
        {
            if (dgvHistorico == null)
            {
                MessageBox.Show("dgvHistorico não foi inicializado.");
                return;
            }

            if (dados == null)
            {
                MessageBox.Show("DataTable enviado está null.");
                return;
            }

            dgvHistorico.DataSource = null;
            dgvHistorico.DataSource = dados;

            // Estilo geral
            dgvHistorico.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            dgvHistorico.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHistorico.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvHistorico.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorico.ColumnHeadersHeight = 35;
            dgvHistorico.EnableHeadersVisualStyles = false;

            dgvHistorico.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dgvHistorico.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            dgvHistorico.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvHistorico.RowTemplate.Height = 30;
            dgvHistorico.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dgvHistorico.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Colunas
            if (dgvHistorico.Columns.Count > 0)
            {
                if (dgvHistorico.Columns.Contains("Documento"))
                {
                    dgvHistorico.Columns["Documento"].HeaderText = "Documento";
                    dgvHistorico.Columns["Documento"].Width = 150;
                }

                if (dgvHistorico.Columns.Contains("DataDocumento"))
                {
                    dgvHistorico.Columns["DataDocumento"].HeaderText = "Data Documento";
                    dgvHistorico.Columns["DataDocumento"].Width = 120;
                }

                if (dgvHistorico.Columns.Contains("DataEntrega"))
                {
                    dgvHistorico.Columns["DataEntrega"].HeaderText = "Data Entrega";
                    dgvHistorico.Columns["DataEntrega"].Width = 120;
                }

                if (dgvHistorico.Columns.Contains("Artigo"))
                {
                    dgvHistorico.Columns["Artigo"].HeaderText = "EPI";
                    dgvHistorico.Columns["Artigo"].Width = 100;
                }

                if (dgvHistorico.Columns.Contains("Descricao"))
                {
                    dgvHistorico.Columns["Descricao"].HeaderText = "Descrição";
                    dgvHistorico.Columns["Descricao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvHistorico.Columns.Contains("Quantidade"))
                {
                    dgvHistorico.Columns["Quantidade"].HeaderText = "Quantidade";
                    dgvHistorico.Columns["Quantidade"].Width = 80;
                    dgvHistorico.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

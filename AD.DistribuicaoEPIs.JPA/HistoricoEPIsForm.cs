
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AD.DistribuicaoEPIs.JPA
{
    public partial class HistoricoEPIsForm : Form
    {
        private DataTable _dadosCompletos;
        private DataTable _dadosAgrupados;
        private bool _modoAgrupado = true;

        public HistoricoEPIsForm(DataTable dados, string codigoColaborador, string nomeColaborador)
        {
            InitializeComponent();

            // Salvar DataTable para usar no Load
            _dadosCompletos = dados.Copy();

            // Configurar informação do colaborador
            lblColaboradorInfo.Text = $"Colaborador: {codigoColaborador} - {nomeColaborador}";

            // Criar dados agrupados
            CriarDadosAgrupados();

            // Adicionar evento Load
            this.Load += HistoricoEPIsForm_Load;
        }

        private void CriarDadosAgrupados()
        {
            _dadosAgrupados = new DataTable();
            _dadosAgrupados.Columns.Add("Documento", typeof(string));
            _dadosAgrupados.Columns.Add("DataDocumento", typeof(string));
            _dadosAgrupados.Columns.Add("QtdItens", typeof(int));
            _dadosAgrupados.Columns.Add("EPIs", typeof(string));

            // Agrupar por documento
            var grupos = _dadosCompletos.AsEnumerable()
                .GroupBy(row => row.Field<string>("Documento"))
                .OrderByDescending(g => g.First().Field<string>("DataDocumento"));

            foreach (var grupo in grupos)
            {
                var primeiraLinha = grupo.First();
                var qtdItens = grupo.Count();
                var epis = string.Join(", ", grupo.Select(r => r.Field<string>("Artigo")).Distinct());

                _dadosAgrupados.Rows.Add(
                    primeiraLinha.Field<string>("Documento"),
                    primeiraLinha.Field<string>("DataDocumento"),
                    qtdItens,
                    epis
                );
            }
        }

        private void HistoricoEPIsForm_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView(_dadosAgrupados);

            // Adicionar evento de duplo clique
            dgvHistorico.CellDoubleClick += DgvHistorico_CellDoubleClick;
        }

        private void DgvHistorico_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Clique no cabeçalho

            if (_modoAgrupado)
            {
                // Expandir para mostrar linhas do documento
                string documento = dgvHistorico.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                MostrarLinhasDocumento(documento);
            }
            else
            {
                // Voltar para modo agrupado
                MostrarModoAgrupado();
            }
        }

        private void MostrarLinhasDocumento(string documento)
        {
            // Filtrar dados para o documento selecionado
            DataTable dadosFiltrados = _dadosCompletos.Clone();

            foreach (DataRow row in _dadosCompletos.Rows)
            {
                if (row.Field<string>("Documento") == documento)
                {
                    dadosFiltrados.ImportRow(row);
                }
            }

            _modoAgrupado = false;
            ConfigurarDataGridView(dadosFiltrados);

            // Atualizar título
            this.Text = $"Histórico de EPIs Distribuídos - Documento: {documento}";
        }

        private void MostrarModoAgrupado()
        {
            _modoAgrupado = true;
            ConfigurarDataGridView(_dadosAgrupados);
            this.Text = "Histórico de EPIs Distribuídos";
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

            // Configurações gerais
            dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorico.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorico.MultiSelect = false;
            dgvHistorico.ReadOnly = true;
            dgvHistorico.AllowUserToAddRows = false;
            dgvHistorico.AllowUserToDeleteRows = false;
            dgvHistorico.RowHeadersVisible = false;

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

            // Configurar colunas baseado no modo
            if (_modoAgrupado)
            {
                ConfigurarColunasAgrupadas();
            }
            else
            {
                ConfigurarColunasDetalhadas();
            }
        }

        private void ConfigurarColunasAgrupadas()
        {
            if (dgvHistorico.Columns.Contains("Documento"))
            {
                dgvHistorico.Columns["Documento"].HeaderText = "Documento";
                dgvHistorico.Columns["Documento"].Width = 150;
                dgvHistorico.Columns["Documento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("DataDocumento"))
            {
                dgvHistorico.Columns["DataDocumento"].HeaderText = "Data Documento";
                dgvHistorico.Columns["DataDocumento"].Width = 120;
                dgvHistorico.Columns["DataDocumento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("QtdItens"))
            {
                dgvHistorico.Columns["QtdItens"].HeaderText = "Qtd. Itens";
                dgvHistorico.Columns["QtdItens"].Width = 100;
                dgvHistorico.Columns["QtdItens"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("EPIs"))
            {
                dgvHistorico.Columns["EPIs"].HeaderText = "EPIs";
                dgvHistorico.Columns["EPIs"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ConfigurarColunasDetalhadas()
        {
            if (dgvHistorico.Columns.Contains("Documento"))
            {
                dgvHistorico.Columns["Documento"].HeaderText = "Documento";
                dgvHistorico.Columns["Documento"].Width = 150;
                dgvHistorico.Columns["Documento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("DataDocumento"))
            {
                dgvHistorico.Columns["DataDocumento"].HeaderText = "Data Documento";
                dgvHistorico.Columns["DataDocumento"].Width = 120;
                dgvHistorico.Columns["DataDocumento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("DataEntrega"))
            {
                dgvHistorico.Columns["DataEntrega"].HeaderText = "Data Entrega";
                dgvHistorico.Columns["DataEntrega"].Width = 120;
                dgvHistorico.Columns["DataEntrega"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("Artigo"))
            {
                dgvHistorico.Columns["Artigo"].HeaderText = "EPI";
                dgvHistorico.Columns["Artigo"].Width = 80;
                dgvHistorico.Columns["Artigo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorico.Columns.Contains("Descricao"))
            {
                dgvHistorico.Columns["Descricao"].HeaderText = "Descrição";
                dgvHistorico.Columns["Descricao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvHistorico.Columns.Contains("Quantidade"))
            {
                dgvHistorico.Columns["Quantidade"].HeaderText = "Quantidade";
                dgvHistorico.Columns["Quantidade"].Width = 100;
                dgvHistorico.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

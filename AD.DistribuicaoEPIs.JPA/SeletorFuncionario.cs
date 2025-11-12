
using StdBE100;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AD.DistribuicaoEPIs.JPA
{
    public partial class SeletorFuncionario : Form
    {
        private DataTable _dadosFuncionarios;

        public string CodigoSelecionado { get; private set; }
        public string NomeSelecionado { get; private set; }

        public SeletorFuncionario(DataTable dadosFuncionarios)
        {
            InitializeComponent();
            _dadosFuncionarios = dadosFuncionarios;
        }

        private void SeletorFuncionario_Load(object sender, EventArgs e)
        {
            CarregarFuncionarios();
        }

        private void CarregarFuncionarios(string filtro = "")
        {
            listViewFuncionarios.Items.Clear();

            if (_dadosFuncionarios == null || _dadosFuncionarios.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum funcionário ativo encontrado.", "Informação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rows = _dadosFuncionarios.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                rows = rows.Where(r =>
                    r.Field<string>("Codigo").ToUpper().Contains(filtro.ToUpper()) ||
                    r.Field<string>("Nome").ToUpper().Contains(filtro.ToUpper()));
            }

            foreach (var row in rows)
            {
                ListViewItem item = new ListViewItem(row.Field<string>("Codigo"));
                item.SubItems.Add(row.Field<string>("Nome"));
                item.Tag = row;
                listViewFuncionarios.Items.Add(item);
            }

            if (listViewFuncionarios.Items.Count > 0)
            {
                listViewFuncionarios.Items[0].Selected = true;
                listViewFuncionarios.Focus();
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarFuncionarios(txtPesquisa.Text);
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            SelecionarFuncionario();
        }

        private void listViewFuncionarios_DoubleClick(object sender, EventArgs e)
        {
            SelecionarFuncionario();
        }

        private void SelecionarFuncionario()
        {
            if (listViewFuncionarios.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um funcionário.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemSelecionado = listViewFuncionarios.SelectedItems[0];
            CodigoSelecionado = itemSelecionado.Text;
            NomeSelecionado = itemSelecionado.SubItems[1].Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

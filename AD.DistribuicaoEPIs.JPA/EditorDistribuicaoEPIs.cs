
using IntBE100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AD.DistribuicaoEPIs.JPA
{
    public partial class EditorDistribuicaoEPIs : CustomForm
    {
        public EditorDistribuicaoEPIs()
        {
            InitializeComponent();
        }

        private void chkEPI_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                // Ativar/desativar o NumericUpDown correspondente
                string epiNumber = chk.Name.Replace("chk", "num");
                NumericUpDown num = this.Controls.Find(epiNumber, true).FirstOrDefault() as NumericUpDown;

                if (num != null)
                {
                    num.Enabled = chk.Checked;
                    if (chk.Checked)
                    {
                        num.Value = 1; // Define quantidade padrão como 1
                        num.Focus();
                    }
                    else
                    {
                        num.Value = 0;
                    }
                }
            }
        }

        private void btnProcurarColaborador_Click(object sender, EventArgs e)
        {
            try
            {
                var queryColaborador = "SELECT [Funcionarios].[Codigo],[Funcionarios].[Nome]\r\nFROM [Funcionarios] WITH (NOLOCK) LEFT JOIN [Situacoes] WITH (NOLOCK) ON  [Funcionarios].[Situacao] = [Situacoes].[Situacao]\r\nWHERE ( ([Situacoes].[Tipo] = 0) ) \r\nORDER BY [Funcionarios].[Codigo]\r\n";
                var resultadoConsulta = BSO.Consulta(queryColaborador);

                // Converter StdBELista para DataTable
                System.Data.DataTable dadosFuncionarosAtivos = new System.Data.DataTable();
                dadosFuncionarosAtivos.Columns.Add("Codigo", typeof(string));
                dadosFuncionarosAtivos.Columns.Add("Nome", typeof(string));

                while (!resultadoConsulta.NoFim())
                {
                    dadosFuncionarosAtivos.Rows.Add(
                        resultadoConsulta.Valor("Codigo"),
                        resultadoConsulta.Valor("Nome")
                    );
                    resultadoConsulta.Seguinte();
                }

                using (SeletorFuncionario seletor = new SeletorFuncionario(dadosFuncionarosAtivos))
                {
                    if (seletor.ShowDialog() == DialogResult.OK)
                    {
                        txtColaborador.Text = seletor.CodigoSelecionado;
                        txtNomeColaborador.Text = seletor.NomeSelecionado;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar funcionários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar se colaborador foi selecionado
                if (string.IsNullOrWhiteSpace(txtColaborador.Text))
                {
                    MessageBox.Show("Por favor, selecione um colaborador.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Construir mensagem com os dados
                StringBuilder mensagem = new StringBuilder();
                mensagem.AppendLine($"Código do Colaborador: {txtColaborador.Text}");
                mensagem.AppendLine($"Nome: {txtNomeColaborador.Text}");
                mensagem.AppendLine($"Data de Entrega: {dtpDataEntrega.Value.ToString("dd/MM/yyyy")}");
                mensagem.AppendLine();
                mensagem.AppendLine("EPIs Selecionados:");
                mensagem.AppendLine(new string('-', 40));

                bool algumEPISelecionado = false;

                // Verificar EPI001 - Capacete
                if (chkEPI001.Checked && numEPI001.Value > 0)
                {
                    mensagem.AppendLine($"EPI001 - Quantidade: {numEPI001.Value}");
                    algumEPISelecionado = true;
                }

                // Verificar EPI002 - Luvas
                if (chkEPI002.Checked && numEPI002.Value > 0)
                {
                    mensagem.AppendLine($"EPI002 - Quantidade: {numEPI002.Value}");
                    algumEPISelecionado = true;
                }

                // Verificar EPI003 - Capa Chuva
                if (chkEPI003.Checked && numEPI003.Value > 0)
                {
                    mensagem.AppendLine($"EPI003 - Quantidade: {numEPI003.Value}");
                    algumEPISelecionado = true;
                }

                // Verificar EPI004 - Botas
                if (chkEPI004.Checked && numEPI004.Value > 0)
                {
                    mensagem.AppendLine($"EPI004 - Quantidade: {numEPI004.Value}");
                    algumEPISelecionado = true;
                }

                // Verificar EPI005 - Colete
                if (chkEPI005.Checked && numEPI005.Value > 0)
                {
                    mensagem.AppendLine($"EPI005 - Quantidade: {numEPI005.Value}");
                    algumEPISelecionado = true;
                }

                // Verificar EPI006 - Óculos
                if (chkEPI006.Checked && numEPI006.Value > 0)
                {
                    mensagem.AppendLine($"EPI006 - Quantidade: {numEPI006.Value}");
                    algumEPISelecionado = true;
                }

                if (!algumEPISelecionado)
                {
                    MessageBox.Show("Por favor, selecione pelo menos um EPI.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                // Criar um Documento Interno
                IntBEDocumentoInterno novoDocumento = new IntBEDocumentoInterno();
                novoDocumento.Tipodoc = "EEPI";
                 BSO.Internos.Documentos.PreencheDadosRelacionados(novoDocumento);
                //data de hoje
                int linha = 0;
                novoDocumento.DataVencimento = DateTime.Now;
                // Adicionar linha para cada EPI selecionado
                if (chkEPI001.Checked && numEPI001.Value > 0)
                {
                    linha++;
                    var linhaPrenchida =  BSO.Internos.Documentos.AdicionaLinha(novoDocumento,  "EPI001" );
                    linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                    linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI001.Value;
                    linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;

                }

                if (chkEPI002.Checked && numEPI002.Value > 0)
                {
                   // BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI002");
                }

                if (chkEPI003.Checked && numEPI003.Value > 0)
                {
                   // BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI003");
                }

                if (chkEPI004.Checked && numEPI004.Value > 0)
                {
                   // BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI004");
                }

                if (chkEPI005.Checked && numEPI005.Value > 0)
                {
                  //  BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI005");
                }

                if (chkEPI006.Checked && numEPI006.Value > 0)
                {
                   // BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI006");
                }

                // Atualizar e gravar o documento
                BSO.Internos.Documentos.Actualiza(novoDocumento);

                // Mostrar mensagem com todos os dados
                MessageBox.Show(mensagem.ToString() + $"\n\nDocumento {novoDocumento.Tipodoc}/{novoDocumento.Serie}/{novoDocumento.NumDoc} criado com sucesso!",
                    "Dados da Distribuição",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

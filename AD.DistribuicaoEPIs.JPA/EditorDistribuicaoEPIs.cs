
using IntBE100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.IO;
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

                        // Habilitar o botão de consultar EPIs e Imprimir
                        btnConsultarEPIs.Enabled = true;
                        btnImprimir.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar funcionários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarEPIs_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtColaborador.Text))
            {
                ConsultarEPIsDistribuidos(txtColaborador.Text);
            }
        }

        private void ConsultarEPIsDistribuidos(string codigoFuncionario)
        {
            try
            {
                // Query para buscar documentos internos do tipo EEPI com linhas do funcionário
                string query = @"
                    SELECT 
                        CabecInternos.TipoDoc,
                        CabecInternos.Serie,
                        CabecInternos.NumDoc,
                        CabecInternos.Data,
                        LinhasInternos.Artigo,
                        LinhasInternos.Descricao,
                        LinhasInternos.Quantidade,
                        LinhasInternos.DataEntrega
                    FROM CabecInternos WITH (NOLOCK)
                    INNER JOIN LinhasInternos WITH (NOLOCK) 
                        ON CabecInternos.Id = LinhasInternos.IdCabecInternos
                    WHERE 
                        CabecInternos.TipoDoc = 'EEPI'
                        AND LinhasInternos.CDU_CodigoFunc = '" + codigoFuncionario + @"'
                    ORDER BY 
                        CabecInternos.Data DESC,
                        LinhasInternos.NumLinha";

                var resultado = BSO.Consulta(query);

                if (resultado == null || resultado.Vazia())
                {
                    MessageBox.Show($"Não foram encontrados EPIs distribuídos para o colaborador {codigoFuncionario}.",
                        "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Criar DataTable para o histórico
                System.Data.DataTable dtHistorico = new System.Data.DataTable();
                dtHistorico.Columns.Add("Documento", typeof(string));
                dtHistorico.Columns.Add("DataDocumento", typeof(string));
                dtHistorico.Columns.Add("DataEntrega", typeof(string));
                dtHistorico.Columns.Add("Artigo", typeof(string));
                dtHistorico.Columns.Add("Descricao", typeof(string));
                dtHistorico.Columns.Add("Quantidade", typeof(string));

                while (!resultado.NoFim())
                {
                    try
                    {
                        string tipoDoc = resultado.Valor("TipoDoc") != null ? resultado.Valor("TipoDoc").ToString() : "";
                        string serie = resultado.Valor("Serie") != null ? resultado.Valor("Serie").ToString() : "";
                        string numDoc = resultado.Valor("NumDoc") != null ? resultado.Valor("NumDoc").ToString() : "";
                        string dataDoc = resultado.Valor("Data") != null ? Convert.ToDateTime(resultado.Valor("Data")).ToString("dd/MM/yyyy") : "";
                        string artigo = resultado.Valor("Artigo") != null ? resultado.Valor("Artigo").ToString() : "";
                        string descricao = resultado.Valor("Descricao") != null ? resultado.Valor("Descricao").ToString() : "";
                        string quantidade = resultado.Valor("Quantidade") != null ? resultado.Valor("Quantidade").ToString() : "0";
                        string dataEntrega = resultado.Valor("DataEntrega") != null ? Convert.ToDateTime(resultado.Valor("DataEntrega")).ToString("dd/MM/yyyy") : "";

                        string documentoID = $"{tipoDoc}/{serie}/{numDoc}";

                        dtHistorico.Rows.Add(documentoID, dataDoc, dataEntrega, artigo, descricao, quantidade);
                    }
                    catch (Exception exLinha)
                    {
                        // Continuar mesmo se uma linha falhar
                        System.Diagnostics.Debug.WriteLine($"Erro ao processar linha: {exLinha.Message}");
                    }

                    resultado.Seguinte();
                }
                if (dtHistorico.Rows.Count == 0)
                {
                    MessageBox.Show($"Não foram encontrados EPIs distribuídos para o colaborador {codigoFuncionario}.",
                        "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mostrar formulário com o histórico
                using (HistoricoEPIsForm formHistorico = new HistoricoEPIsForm(dtHistorico, codigoFuncionario, txtNomeColaborador.Text))
                {
                    formHistorico.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar EPIs distribuídos: {ex.Message}\n\nDetalhes: {ex.StackTrace}", "Erro",
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

                // Mostrar loading antes de processar
                LoadingForm loadingForm = new LoadingForm("A guardar documento");
                loadingForm.Show();
                this.Enabled = false; // Desabilitar o formulário principal
                Application.DoEvents();

                try
                {
                    BSO.Internos.Documentos.PreencheDadosRelacionados(novoDocumento);
                    //data de hoje
                    int linha = 0;
                    novoDocumento.DataVencimento = DateTime.Now;
                    // Adicionar linha para cada EPI selecionado
                    if (chkEPI001.Checked && numEPI001.Value > 0)
                    {
                        linha++;
                        var linhaPrenchida = BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI001");
                        linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI001.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;
                    }

                    if (chkEPI002.Checked && numEPI002.Value > 0)
                    {
                        linha++;
                        var linhaPrenchida = BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI002");
                        linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI002.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;
                    }

                    if (chkEPI003.Checked && numEPI003.Value > 0)
                    {
                        linha++;
                        var linhaPrenchida = BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI003");
                        linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI003.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;
                    }

                    if (chkEPI004.Checked && numEPI004.Value > 0)
                    {
                        linha++;
                        var linhaPrenchida = BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI004");
                        linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI004.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;
                    }

                    if (chkEPI005.Checked && numEPI005.Value > 0)
                    {
                        linha++;
                        var linhaPrenchida = BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI005");
                        linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI005.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;
                    }

                    if (chkEPI006.Checked && numEPI006.Value > 0)
                    {
                        linha++;
                        var linhaPrenchida = BSO.Internos.Documentos.AdicionaLinha(novoDocumento, "EPI006");
                        linhaPrenchida.Linhas.GetEdita(linha).DataEntrega = dtpDataEntrega.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).Quantidade = (double)numEPI006.Value;
                        linhaPrenchida.Linhas.GetEdita(linha).CamposUtil["CDU_CodigoFunc"].Valor = txtColaborador.Text;
                    }

                    // Atualizar e gravar o documento
                    Application.DoEvents(); // Manter o loading visível
                    BSO.Internos.Documentos.Actualiza(novoDocumento);

                    // Fechar loading e reabilitar formulário
                    loadingForm.Close();
                    this.Enabled = true;

                    // Mostrar mensagem com todos os dados
                    MessageBox.Show(mensagem.ToString() + $"\n\nDocumento {novoDocumento.Tipodoc}/{novoDocumento.Serie}/{novoDocumento.NumDoc} criado com sucesso!",
                        "Dados da Distribuição",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    loadingForm.Close();
                    this.Enabled = true;
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar se há colaborador selecionado
                if (string.IsNullOrWhiteSpace(txtColaborador.Text))
                {
                    MessageBox.Show("Por favor, selecione um colaborador primeiro.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Buscar o último documento EEPI do colaborador
                string queryUltimoDoc = @"
            SELECT TOP 1
                CabecInternos.NumDoc,
                CabecInternos.Serie
            FROM CabecInternos WITH (NOLOCK)
            INNER JOIN LinhasInternos WITH (NOLOCK) 
                ON CabecInternos.Id = LinhasInternos.IdCabecInternos
            WHERE 
                CabecInternos.TipoDoc = 'EEPI'
                AND LinhasInternos.CDU_CodigoFunc = '" + txtColaborador.Text + @"'
            ORDER BY 
                CabecInternos.Data DESC,
                CabecInternos.NumDoc DESC";

                var resultadoDoc = BSO.Consulta(queryUltimoDoc);

                if (resultadoDoc == null || resultadoDoc.Vazia())
                {
                    MessageBox.Show("Não foi encontrado nenhum documento de EPIs para este colaborador.", "Informação",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int numDoc = Convert.ToInt32(resultadoDoc.Valor("NumDoc"));
                string serie = resultadoDoc.Valor("Serie").ToString();

                // Mostrar loading
                LoadingForm loadingForm = new LoadingForm("A gerar PDF");
                loadingForm.Show();
                this.Enabled = false;
                Application.DoEvents();

                try
                {
                    // Inicializar impressão
                    this.PSO.Mapas.Inicializar("ERP");
                    this.PSO.Mapas.Destino = StdBSTipos.CRPEExportDestino.edFicheiro;
                    this.PSO.Mapas.TipoFolha = StdBSTipos.CRPETipoFolha.tfA4;

                    // ✅ Definir caminho completo do ficheiro PDF
                    string pasta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string nomeArquivo = Path.Combine(
                        pasta,
                        $"EPI_{txtColaborador.Text}_{serie}_{numDoc}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                    );

                    this.PSO.Mapas.SetFileProp(StdBSTipos.CRPEExportFormat.efPdf, nomeArquivo);
                    this.PSO.Mapas.MostraErros = true;

                    // Query SQL para o mapa
                    var sqlquery = @"
                SELECT 
                    LinhasInternos.Artigo, 
                    LinhasInternos.Descricao, 
                    LinhasInternos.Quantidade, 
                    LinhasInternos.DataEntrega, 
                    CabecInternos.TipoDoc, 
                    CabecInternos.NumDoc, 
                    CabecInternos.Serie, 
                    Funcionarios.Nome, 
                    Funcionarios.Codigo, 
                    Categorias.Descricao AS CategoriaDescricao
                FROM LinhasInternos WITH (NOLOCK) 
                    INNER JOIN CabecInternos WITH (NOLOCK) 
                        ON LinhasInternos.IdCabecInternos = CabecInternos.Id
                    INNER JOIN Funcionarios WITH (NOLOCK) 
                        ON LinhasInternos.CDU_CodigoFunc = Funcionarios.Codigo
                    INNER JOIN Categorias WITH (NOLOCK) 
                        ON Funcionarios.Categoria = Categorias.Categoria
                WHERE 
                    CabecInternos.NumDoc = " + numDoc + @" 
                    AND CabecInternos.Serie = N'" + serie + @"'
                ORDER BY LinhasInternos.NumLinha";

                    this.PSO.Mapas.SetParametro("NumDoc", numDoc);
                    this.PSO.Mapas.SetParametro("serie", serie);

                    int imprimeMapa = this.PSO.Mapas.ImprimeListagem(
                        "EPIS00", "Documento", "P", 1, "N", "", 0, false, false, sqlquery
                    );

                    loadingForm.Close();
                    this.Enabled = true;

                    // ✅ Abrir o PDF automaticamente
                    if (System.IO.File.Exists(nomeArquivo))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = nomeArquivo,
                            UseShellExecute = true
                        });
                    }

                    MessageBox.Show(
                        $"PDF gerado com sucesso!\n\nDocumento: {serie}/{numDoc}\nColaborador: {txtColaborador.Text} - {txtNomeColaborador.Text}\n\nLocal:\n{nomeArquivo}",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch
                {
                    loadingForm.Close();
                    this.Enabled = true;
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao imprimir mapa: {ex.Message}\n\nDetalhes: {ex.StackTrace}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

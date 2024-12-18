using Exercicio_WPF.Controller;
using Exercicio_WPF.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Exercicio_WPF.View
{
    /// <summary>
    /// Lógica interna para CadastroProduto.xaml
    /// </summary>
    public partial class CadastroProduto : Window
    {
        private readonly ProdutoController _produtoController;
        public CadastroProduto()
        {
            InitializeComponent();

            var app = (App)Application.Current;
            _produtoController = app.GetServiceProvider().GetService<ProdutoController>();

            PreencherComboBox();
        }

        private void PreencherComboBox()
        {
            cbxGrupo.ItemsSource = _produtoController.ListarNomesDosGrupos();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidarCampos();

                if (CamposValidos())
                {
                    var novoProduto = new ProdutoModel
                    {
                        Descricao = txtNomeProduto.Text,
                        CodBarra = txtCodigoBarras.Text,
                        PrecoCusto = decimal.Parse(txtPrecoCusto.Text),
                        PrecoVenda = decimal.Parse(txtPrecoVenda.Text),
                        CodGrupo = (int)cbxGrupo.SelectedValue,
                        Ativo = chkAtivo.IsChecked == true,
                        DataHoraCadastro = DateTime.Now
                    };

                    _produtoController.AdicionarProduto(novoProduto);

                    ResetarValidacao();

                    MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    LimparFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar o produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimparFormulario()
        {
            txtNomeProduto.Text = "";
            txtCodigoBarras.Text = "";
            txtPrecoCusto.Text = "";
            txtPrecoVenda.Text = "";
            cbxGrupo.SelectedIndex = -1;
            chkAtivo.IsChecked = false;
        }

        private void ValidarCampos()
        {
            // Resetar bordas antes da validação
            txtNomeProduto.Tag = null;
            txtPrecoCusto.Tag = null;
            txtPrecoVenda.Tag = null;
            cbxGrupo.Tag = null;

            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text) || string.IsNullOrWhiteSpace(txtPrecoCusto.Text) || string.IsNullOrWhiteSpace(txtPrecoVenda.Text) || cbxGrupo.SelectedValue == null)
            {
                MessageBox.Show("Preencha os campos indicados", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Nome do Produto
            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
                txtNomeProduto.Tag = "Error";

            // Preço de Custo
            if (string.IsNullOrWhiteSpace(txtPrecoCusto.Text))
                txtPrecoCusto.Tag = "Error";

            // Preço de Venda
            if (string.IsNullOrWhiteSpace(txtPrecoVenda.Text))
                txtPrecoVenda.Tag = "Error";

            // ComboBox Grupo
            if (cbxGrupo.SelectedValue == null)
                cbxGrupo.Tag = "Error";
        }

        private bool CamposValidos()
        {
            return txtNomeProduto.Tag == null &&
                   txtPrecoCusto.Tag == null &&
                   txtPrecoVenda.Tag == null &&
                   cbxGrupo.Tag == null;
        }

        private void ResetarValidacao()
        {
            // Reseta as Tags
            txtNomeProduto.Tag = null;
            txtPrecoCusto.Tag = null;
            txtPrecoVenda.Tag = null;
            cbxGrupo.Tag = null;
        }
    }
}

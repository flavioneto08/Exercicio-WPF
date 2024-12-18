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
        private ProdutoModel _produto;
        public CadastroProduto()
        {
            InitializeComponent();

            var app = (App)Application.Current;
            _produtoController = app.GetServiceProvider().GetService<ProdutoController>();

            PreencherComboBox();
        }

        public CadastroProduto(ProdutoModel produto)
        {
            InitializeComponent();

            var app = (App)Application.Current;
            _produtoController = app.GetServiceProvider().GetService<ProdutoController>();

            _produto = produto;

            PreencherComboBox();
            PreencherCampos();
        }

        private void PreencherCampos()
        {
            txtNomeProduto.Text = _produto.Descricao;
            txtCodigoBarras.Text = _produto.CodBarra;
            txtPrecoCusto.Text = _produto.PrecoCusto.ToString("N2");
            txtPrecoVenda.Text = _produto.PrecoVenda.ToString("N2");
            cbxGrupo.SelectedValue = _produto.CodGrupo;
            chkAtivo.IsChecked = _produto.Ativo;
        }

        private void PreencherComboBox()
        {
            cbxGrupo.ItemsSource = _produtoController.ListarNomesDosGrupos();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var campos = new List<TextBox> { txtNomeProduto, txtPrecoCusto, txtPrecoVenda };

                if (ValidarCampos(campos))
                {
                    int codNovo = _produto?.Cod ?? _produtoController.CodigoIncremental();
                    var novoProduto = new ProdutoModel
                    {
                        Cod = codNovo,
                        Descricao = txtNomeProduto.Text,
                        CodBarra = txtCodigoBarras.Text,
                        PrecoCusto = decimal.Parse(txtPrecoCusto.Text),
                        PrecoVenda = decimal.Parse(txtPrecoVenda.Text),
                        CodGrupo = (int)cbxGrupo.SelectedValue,
                        Ativo = chkAtivo.IsChecked ?? false,
                        DataHoraCadastro = _produto?.DataHoraCadastro ?? DateTime.Now
                    };

                    if (_produtoController.VerificaProduto(novoProduto.Cod))
                    {
                        _produtoController.AtualizarProduto(novoProduto);
                        MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        var frmConsultaProduto = new ConsultaProduto();
                        frmConsultaProduto.Activate();
                        this.Close();
                    }
                    else
                    {
                        _produtoController.AdicionarProduto(novoProduto);
                        MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

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

        private bool ValidarCampos(List<TextBox> campos)
        {
            bool valido = true;
           
            lblErroGrupo.Visibility = Visibility.Hidden;

            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text) || string.IsNullOrWhiteSpace(txtPrecoCusto.Text) || string.IsNullOrWhiteSpace(txtPrecoVenda.Text) || cbxGrupo.SelectedValue == null)
            {
                MessageBox.Show("Preencha os campos indicados", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (var campo in campos) 
            {
                if (string.IsNullOrWhiteSpace(campo.Text))
                {
                    campo.Tag = "Invalido";
                    valido = false;
                }
                else
                {
                    campo.Tag = null;
                }
            }
            if (cbxGrupo.SelectedIndex == -1)
            {
                lblErroGrupo.Visibility = Visibility.Visible;
                valido = false;
            }

            return valido;
        }
    }
}

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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Exercicio_WPF.View
{
    /// <summary>
    /// Lógica interna para ConsultaProduto.xaml
    /// </summary>
    public partial class ConsultaProduto : Window
    {
        private readonly ProdutoController _produtoController;

        public ConsultaProduto()
        {
            InitializeComponent();

            var app = (App)Application.Current;
            _produtoController = app.GetServiceProvider().GetService<ProdutoController>();

            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            gridProdutos.ItemsSource = _produtoController.ListarProdutos();
        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            var produtoSelecionado = gridProdutos.SelectedItem as ProdutoDTO;

            if (produtoSelecionado != null )
            {
                string mensagem = $"Tem certeza que deseja remover o produto '{produtoSelecionado.Descricao}'?";

                if (MessageBox.Show(mensagem, "Confirmar Remoção", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _produtoController.RemoverProduto(produtoSelecionado.Cod);
                    CarregarProdutos();
                    MessageBox.Show($"Produto '{produtoSelecionado.Descricao}' removido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma linha foi selecionada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            CadastroProduto frmCadastroProduto = new CadastroProduto();
            frmCadastroProduto.Show();
        }
    }
}

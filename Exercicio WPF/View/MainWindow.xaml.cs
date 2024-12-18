using Exercicio_WPF.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercicio_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProduto frmConsultaProduto = new ConsultaProduto();
            frmConsultaProduto.Show();
        }

        private void btnCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroProduto frmCadastroProduto = new CadastroProduto();
            frmCadastroProduto.Show();
        }
    }
}
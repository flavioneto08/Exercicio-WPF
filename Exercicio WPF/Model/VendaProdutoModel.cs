using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_WPF.Model
{
    public class VendaProdutoModel
    {
        [Key]
        public int Cod { get; set; }

        public int CodVenda { get; set; }

        public int CodProduto { get; set; }

        public decimal PrecoVenda { get; set; }

        public decimal Quantidade { get; set; }

    }
}

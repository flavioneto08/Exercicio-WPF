using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_WPF.Model
{
    public class Produto_GrupoModel
    {
        [Key]
        public int Cod { get; set; }

        public string Nome { get; set; }

        public ICollection<ProdutoModel> Produto { get; set; }

    }
}

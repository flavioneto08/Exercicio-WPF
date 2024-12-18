using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_WPF.Model
{
    public class ProdutoDTO
    {
        public int Cod { get; set; }
        public string Descricao { get; set; }
        public string NomeGrupo { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
    }
}

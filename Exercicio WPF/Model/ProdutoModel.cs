using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_WPF.Model
{
    public class ProdutoModel
    {
        [Key]
        public int Cod { get; set; }

        public required string Descricao { get; set; }

        public int CodGrupo { get; set; }

        [AllowNull]
        public string CodBarra { get; set; }

        public decimal PrecoCusto { get; set; }

        public decimal PrecoVenda { get; set; }

        public DateTime DataHoraCadastro { get; set; }

        public bool Ativo { get; set; }

        public Produto_GrupoModel Produto_Grupo {  get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_WPF.Model
{
    public class VendaModel
    {
        [Key]
        public int Cod { get; set; }

        [AllowNull]
        public string ClienteDocumento { get; set; }

        [AllowNull]
        public string ClienteNome { get; set; }

        [AllowNull]
        public string Obs { get; set; }

        public decimal Total { get; set; }

        public DateTime DataHora { get; set; }
    }
}

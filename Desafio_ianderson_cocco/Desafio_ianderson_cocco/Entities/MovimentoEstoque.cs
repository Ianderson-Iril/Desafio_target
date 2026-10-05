using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_ianderson_cocco.Entities
{
    public class MovimentoEstoque
    {
        public int id { get; set; }
        public string tipoMovimento { get; set; } = string.Empty;
        public decimal quantidade { get; set; }
        public int CodItem  { get; set; }
    }
}

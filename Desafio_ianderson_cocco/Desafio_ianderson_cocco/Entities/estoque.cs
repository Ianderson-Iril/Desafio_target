using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_ianderson_cocco.Entities
{
    public class estoque
    {
        public int ID { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal quantidade { get; set; }
    }
}

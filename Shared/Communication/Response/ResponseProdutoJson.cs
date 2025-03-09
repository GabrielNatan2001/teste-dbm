using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Response
{
    public class ResponseProdutoJson
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace UStart.Domain.Commands
{
    public class FormaPagamentoCommand
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public List<string> Dias { get; set; }
        public decimal Desconto { get; set; }
        public string CodigoExterno { get; set; }
    }
}
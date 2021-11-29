using System;
using System.Collections.Generic;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class FormaPagamento
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public ICollection<string> Dias { get; private set; }
        public decimal Desconto { get; private set; }
        public string CodigoExterno { get; private set; }

        public FormaPagamento()
        {            
        }

        public FormaPagamento(FormaPagamentoCommand command)
        {
            Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;         
            AtualizarCampos(command);
        }

        public void Update(FormaPagamentoCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(FormaPagamentoCommand command)
        {
            this.Id = command.Id;        
            Descricao = command.Descricao;                        
            Dias = command.Dias;                        
            Desconto = command.Desconto;                        
            CodigoExterno = command.CodigoExterno;                        
                                   
        }
  
    }
}
using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class Fornecedor
    {
        public Guid Id { get; private set; }
        public string CodigoExterno { get; private set; }
        public bool Ativo { get; private set; }
        public String Nome { get; private set; }
        public String RazaoSocial { get; private set; }
        public String CNPJ { get; private set; }
        public String Rua { get; private set; }
        public String Numero { get; private set; }
        public String Complemento { get; private set; }
        public String Bairro { get; private set; }
        public String EstadoId { get; private set; }
        public String CidadeId { get; private set; }
        public String CEP { get; private set; }
        public String Fone { get; private set; }
        public String Email { get; private set; }
       

        public Fornecedor()
        {
            
        }

        public Fornecedor(FornecedorCommand command)
        {
            Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;   
            AtualizarCampos(command);            
        }

        public void Update(FornecedorCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(FornecedorCommand command)
        {            
            CodigoExterno = command.CodigoExterno;                        
            Ativo = command.Ativo;                        
            Nome = command.Nome;                        
            RazaoSocial = command.RazaoSocial;                        
            CNPJ = command.CNPJ;                                     
            Rua = command.Rua;                        
            Numero = command.Numero;                        
            Complemento = command.Complemento;                        
            Bairro = command.Bairro;                        
            EstadoId = command.EstadoId;
            CidadeId = command.CidadeId;                                              
            CEP = command.CEP;                        
            Fone = command.Fone;                        
            Email = command.Email;                                             
        }

    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public class Pessoa:IItem
    {
        public Pessoa()
        {  
        }

        public virtual String Nome { get; set; }
        public virtual Telefone Telefone { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public virtual Endereco Endereco { get; set; }
        
        public virtual Guid ID
        {
            get;
            set;
        }
    }

}

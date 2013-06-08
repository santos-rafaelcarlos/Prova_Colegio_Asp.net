using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public class Endereco
    {
        public virtual Guid ID { get; set; }

        public virtual String TipoLogradouro { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual int Numero { get; set; }
        public virtual String Complemento { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String Cidade { get; set; }
        public virtual Estado UF { get; set; }
        public virtual int CEP { get; set; }

        public override bool Equals(object obj)
        {
            bool retVal = false;
            Endereco end = obj as Endereco;

            if (end != null)
                retVal = (this.CEP == end.CEP
                    && string.Compare(this.Logradouro, end.Logradouro, true) == 0
                    && this.Numero == end.Numero);


            return retVal;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}

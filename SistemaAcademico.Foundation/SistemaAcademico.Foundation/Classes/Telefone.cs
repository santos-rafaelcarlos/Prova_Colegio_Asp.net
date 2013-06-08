using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
   public class Telefone
    {
        public virtual Guid TitularID { get; set; }

        public virtual int DDD { get; set; }
        public virtual int Numero { get; set; }


        public override bool Equals(object obj)
        {
            bool retVal = false;
            Telefone end = obj as Telefone;

            if (end != null)
                retVal = (this.DDD == end.DDD
                    && this.Numero == end.Numero);
            return retVal;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}) {1}",this.DDD,this.Numero);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public class Aluno : Pessoa
    {
        public Aluno():base()
        {

        }

        public virtual AlunoStatus Status { get; set; }
    }
}

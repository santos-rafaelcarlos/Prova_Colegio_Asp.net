using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public class Professor: Pessoa
    {
        public Professor():base()
        {

        }

        public virtual string Nivel { get; set; }
        public virtual ProfessorStatus Status { get; set; }
    }
}

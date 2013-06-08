using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public class Turma:IItem
    {
        public virtual TurmaStatus Status { get; set; }
        public virtual string Codigo { get; set; }
        public virtual string Nivel { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual List<Aluno> Alunos { get; set; }
        
        public virtual DateTime HoraInicio { get; set; }
        public virtual DateTime HoraFim { get; set; }

        public virtual Guid ID
        {
            get;
            set;
        }
    }   
}

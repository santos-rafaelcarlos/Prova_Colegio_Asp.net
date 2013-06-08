using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.Mapping
{
    public class ProfessorMapping : SubclassMap<Professor>
    {
        public ProfessorMapping()
        {
            Table("Professor");
            //DiscriminatorValue("Professor");
            Map(p => p.Status);
            Map(p => p.Nivel);
            KeyColumn("PessoaID");
        }
    }
}

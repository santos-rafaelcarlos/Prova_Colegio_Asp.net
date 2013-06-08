using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.Mapping
{
    public class AlunoMapping: SubclassMap<Aluno>
    {
        public AlunoMapping()
        {
            Table("Aluno");
            //DiscriminatorValue("Aluno");
            Map(a => a.Status);
            KeyColumn("PessoaID");
        }
    }
}

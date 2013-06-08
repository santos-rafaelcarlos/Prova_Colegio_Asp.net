using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.Mapping
{
    public class PessoaMapping : ClassMap<Pessoa>
    {
        public PessoaMapping()
        {
            Table("Pessoa");
            Id(p => p.ID);

           // DiscriminateSubClassesOnColumn("ClassType").Not.Nullable();

            Map(p => p.Nome);            
            Map(p => p.DataNascimento);

            References(p => p.Telefone).ForeignKey("TitularID").Cascade.All();
            References(p => p.Endereco).ForeignKey("ID").Cascade.All();          
        }
    }
}

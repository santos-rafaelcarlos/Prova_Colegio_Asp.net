using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.Mapping
{
    public class MidiaEmprestadaMapping : ClassMap<MidiaEmprestada>
    {
        public MidiaEmprestadaMapping()
        {
            Table("MidiaEmprestada");
            Id(m => m.ID);
            Map(m => m.Status);
            Map(m => m.DataEmprestimo);
            Map(m => m.DataDevolucao).Nullable();

            References(m => m.Pessoa);
        }
    }
}

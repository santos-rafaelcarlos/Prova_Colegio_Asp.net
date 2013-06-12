using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.Mapping
{
    public class TurmaMapping : ClassMap<Turma>
    {
        public TurmaMapping()
        {
            Table("Turma");
            Id(t => t.ID);

            Map(t => t.Status);
            Map(t => t.Codigo);
            Map(t => t.Nivel);
            Map(t => t.DataInicio);
            Map(t => t.DataFim);
            Map(t => t.HoraInicio);
            Map(t => t.HoraFim);

            References(t => t.Professor);            
                        
            HasMany(t => t.Alunos);
        }
    }
}

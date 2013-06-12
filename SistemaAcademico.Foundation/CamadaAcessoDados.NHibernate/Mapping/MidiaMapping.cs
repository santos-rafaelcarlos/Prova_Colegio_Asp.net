using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.Mapping
{
    public class MidiaMapping : ClassMap<Midia>
    {
        public MidiaMapping()
        {
            Table("Midia");
            Id(m => m.ID);
            Map(m => m.Titulo);
            Map(m => m.Autor);
            Map(m => m.NumerodeCopias);
            Map(m => m.NumerodeCopiasEmprestadas);
            Map(m => m.NumerodeCopiasDisponíveis);
            
            HasMany(m => m.Emprestimos).Cascade.All();
        }

    }
}

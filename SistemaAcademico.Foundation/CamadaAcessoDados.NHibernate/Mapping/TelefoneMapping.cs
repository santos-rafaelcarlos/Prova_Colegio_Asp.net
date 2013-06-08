using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace DataAcess.NHibernate.Mapping
{
    public class TelefoneMapping : ClassMap<Telefone>
    {
        public TelefoneMapping()
        {
            Table("Telefone");
            Id(t => t.TitularID);

            Map(t => t.DDD);
            Map(t => t.Numero);

             
        }
    }
}

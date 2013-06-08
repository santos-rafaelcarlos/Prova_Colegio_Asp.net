using FluentNHibernate.Mapping;
using SistemaAcademico.Foundation;

namespace DataAcess.NHibernate.Mapping
{
    public class EnderecoMapping : ClassMap<Endereco>
    {
        public EnderecoMapping()
        {
            Table("Endereco");
            Id(e => e.ID);

            Map(e => e.Bairro);
            Map(e => e.CEP);
            Map(e => e.Cidade);
            Map(e => e.Complemento);
            Map(e => e.Logradouro);
            Map(e => e.Numero);
            Map(e => e.TipoLogradouro);
            Map(e => e.UF);            
        }
    }
}

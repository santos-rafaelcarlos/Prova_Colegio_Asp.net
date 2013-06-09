using System.Collections.Generic;
using SistemaAcademico.Foundation;

namespace Presentation.WebApp.Models.Comum
{
    public class ListaItemViewModel<T> where T : IItem
    {
            public IEnumerable<T> Items { get; set; }
            public InformacaoDePaginacao InformacaoDePaginacao { get; set; }
    }
}
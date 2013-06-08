using SistemaAcademico.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaAcessoDados
{
    public interface IDAL<T> where T : IItem
    {
        T[] RetornaTudo();
        T[] RetornaPorID(Guid[] ids);
        T RetornaPorID(Guid id);

        void InserirOuAtualizar(T entity);      
        void Excluir(T entity);                
    }
}

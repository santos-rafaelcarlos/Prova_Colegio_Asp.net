using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public interface IRepositorio<T> where T : class,IItem
    {
        T[] RetornaTodos();
        T[] RetornaPorID(Guid[] IDs);
        void Salvar(T item);        
        void Remover(T item);
        T RetornaPorID(Guid id);
    }
}

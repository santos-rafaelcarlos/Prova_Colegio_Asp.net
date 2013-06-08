using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.EntityFramework.DAO
{
    public class DAOGenerico<T> : IDAL<T> where T :class,IItem
    {
        private readonly ColegioContext _context = null;

        public DAOGenerico(ColegioContext context)
        {
            _context = context;
        }


        public T[] RetornaTudo()
        {
            return _context.Set<T>().ToArray();
        }

        public T[] RetornaPorID(Guid[] ids)
        {
            return _context.Set<T>().Where(e => ids.Contains(e.ID)).ToArray();
        }

        public T RetornaPorID(Guid id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.ID == id);
        }

        public void InserirOuAtualizar(T entity)
        {
            if (!_context.Set<T>().Contains(entity))
                _context.Set<T>().Add(entity);
            SalvarTudo();
        }

        public void Excluir(T entity)
        {
            _context.Set<T>().Remove(entity);
            SalvarTudo();
        }

        public void SalvarTudo()
        {
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaAcademico.Foundation;
//using CamadaAcessoDados.EntityFramework;
using CamadaAcessoDados.NHibernate;

namespace CamadaAcessoDados.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class,IItem
    {
        private static Repositorio<T> instancia;
        internal static Repositorio<T> Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new Repositorio<T>();
                return instancia;
            }
        }


        public virtual T[] RetornaTodos()
        {
            return Persistencia<T>.Instancia.DAOGenerico.RetornaTudo();
        }

        public virtual void Salvar(T item)
        {
            Persistencia<T>.Instancia.DAOGenerico.InserirOuAtualizar(item);
        }

        public virtual void Remover(T item)
        {
            Persistencia<T>.Instancia.DAOGenerico.Excluir(item);
        }

        public virtual T RetornaPorID(Guid id)
        {
            return Persistencia<T>.Instancia.DAOGenerico.RetornaPorID(id);
        }

        public T[] RetornaPorID(Guid[] iDs)
        {
            return Persistencia<T>.Instancia.DAOGenerico.RetornaPorID(iDs);
        }
    }
}

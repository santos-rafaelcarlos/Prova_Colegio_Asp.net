using System;
using System.Linq;
using CamadaAcessoDados.NHibernate;
using NHibernate;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate.DAO
{
    public class DAOGenerico<T> : IDAL<T> where T : IItem
    {
        HybridSessionBuilder _builder = new HybridSessionBuilder();

        private ISession _session;
        public virtual ISession Session
        {
            get
            {
                if (_builder == null)
                    _builder = new HybridSessionBuilder();

                return _session ?? (_session = _builder.GetSession());
            }
        }
        
        public T[] RetornaTudo()
        {
            return Session.CreateCriteria(typeof(T)).List<T>().ToArray();
        }

        public T[] RetornaPorID(Guid[] ids)
        {
            return Session.CreateCriteria(typeof(T)).List<T>()
                .Where(i => ids.Contains(i.ID)).ToArray();
        }
        public T RetornaPorID(Guid id)
        {
            return Session.Get<T>(id);
           // return Session.CreateCriteria(typeof(T)).List<T>().Where(t => t.ID == id).FirstOrDefault();
        }

        public void InserirOuAtualizar(T entity)
        {
            using (ITransaction trasacao = Session.BeginTransaction())
            {
                try
                {
                    Session.Clear();
                    Session.SaveOrUpdate(entity);
                    trasacao.Commit();
                    trasacao.Dispose();
                }
                catch { trasacao.Rollback(); }
            }
        }

        public void Excluir(T entity)
        {
            using (ITransaction trasacao = Session.BeginTransaction())
            {
                try
                {
                    Session.Clear();
                    Session.Delete(entity);
                    trasacao.Commit();
                    trasacao.Dispose();
                }
                catch { trasacao.Rollback(); }
            }
        }        
    }
}

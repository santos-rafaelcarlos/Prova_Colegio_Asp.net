using System;
using System.Collections.Generic;
using CamadaAcessoDados.NHibernate.DAO;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.NHibernate
{
    public sealed class Persistencia<T> where T : class,IItem
    {
        private static Dictionary<Type, Persistencia<T>> _instances = new Dictionary<Type, Persistencia<T>>();
                
        private DAOGenerico<T> _DAO = null;

        Persistencia()
        {
            _DAO = new DAOGenerico<T>();
        }

        public static Persistencia<T> Instancia
        {
            get
            {
                if (!_instances.ContainsKey(typeof(T)))
                    _instances.Add(typeof(T), new Persistencia<T>());

                return _instances[typeof(T)];
            }
        }

        public DAOGenerico<T> DAOGenerico
        {
            get { return _DAO; }
        }
    }
}

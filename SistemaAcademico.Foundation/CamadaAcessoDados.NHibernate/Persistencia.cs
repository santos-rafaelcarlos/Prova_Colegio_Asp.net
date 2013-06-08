using System;
using System.Collections.Generic;
using CamadaAcessoDados.NHibernate.DAO;
using SistemaAcademico.Foundation;
using CamadaAcessoDados.DAL;

namespace CamadaAcessoDados.NHibernate
{
    public sealed class Persistencia<T> : IPersistencia<T> where T : class,IItem
    {
        Persistencia()
        {
            _Dao = new DAOGenerico<T>();
        }

        public static Persistencia<T> Instancia
        {
            get
            {
                if (!_instances.ContainsKey(typeof(T)))
                    _instances.Add(typeof(T), new Persistencia<T>());

                return (Persistencia<T>)_instances[typeof(T)];
            }
        }
    }
}

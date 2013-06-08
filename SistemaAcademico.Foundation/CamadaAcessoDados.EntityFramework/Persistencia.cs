using System;
using System.Collections.Generic;
using SistemaAcademico.Foundation;
using CamadaAcessoDados.EntityFramework;
using CamadaAcessoDados.EntityFramework.DAO;
using CamadaAcessoDados.DAL;

namespace CamadaAcessoDados.EntityFramework
{
    public sealed class Persistencia<T> : IPersistencia<T> where T : class,IItem
    {
        private static ColegioContext _context = null;
        
        Persistencia()
        {
            if (_context == null)
            {
                string connString = @"Data Source=FER-PC\SQLEXPRESS;Initial Catalog=ColegioEF;Integrated Security=True";
                _context = new ColegioContext(connString);

                _context.Configuration.LazyLoadingEnabled = true;
                _context.Configuration.ProxyCreationEnabled = true;
            }

            _Dao = new DAOGenerico<T>(_context);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.DAL
{
    public abstract class IPersistencia<T> where T : class,IItem
    {
        protected static Dictionary<Type, IPersistencia<T>> _instances = new Dictionary<Type, IPersistencia<T>>();

        protected IDAL<T> _Dao = null;
                
        public IDAL<T> DAOGenerico
        {
            get { return _Dao; }
        }
    }
}



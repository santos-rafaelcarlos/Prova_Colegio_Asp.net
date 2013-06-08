﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaAcademico.Foundation;
using CamadaAcessoDados.EntityFramework;
//using CamadaAcessoDados.NHibernate;

namespace CamadaAcessoDados.Repositorio
{
    public class RepositorioGenerico<T> : IRepositorio<T> where T : class,IItem
    {
        private static RepositorioGenerico<T> instancia;
        internal static RepositorioGenerico<T> Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new RepositorioGenerico<T>();
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
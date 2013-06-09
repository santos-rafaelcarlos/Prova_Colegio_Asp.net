using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Context;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace CamadaAcessoDados.NHibernate
{
    public class HybridSessionBuilder
    {
        private static ISession _currentSession;
        private static ISessionFactory _sessionFactory;

        public ISession GetSession()
        {
            ISessionFactory factory = GetSessionFactory();

            if (!CurrentSessionContext.HasBind(factory))
                CurrentSessionContext.Bind(GetExistingOrNewSession(factory));

            return factory.GetCurrentSession();
        }

        public Configuration GetConfiguration()
        {
            var configuration = new Configuration();
            configuration.Configure();
            return configuration;
        }

        private ISessionFactory GetSessionFactory()
        {
            return _sessionFactory ?? (_sessionFactory = Build().BuildSessionFactory());
        }

        private ISession GetExistingOrNewSession(ISessionFactory factory)
        {
            if (_currentSession == null)
            {
                _currentSession = factory.OpenSession();
            }
            else if (!_currentSession.IsOpen)
            {
                _currentSession = factory.OpenSession();
            }

            return _currentSession;
        }

        public static FluentConfiguration Build()
        {
           // string connString = @"Data Source=FER-PC\SQLEXPRESS;Initial Catalog=ColegioNH;Integrated Security=True";

            return Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromAppSetting("Colegio")))
                           //.Database(MsSqlConfiguration.MsSql2008.ConnectionString(connString))
                           .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()).Conventions.Add<StringColumnLengthConvention>())
                           .ExposeConfiguration(BuildSchema);
        }

        private static void BuildSchema(Configuration config)
        {
            config.SetProperty("current_session_context_class", "thread_static");

            //new SchemaExport(config)
            //    .Drop(true, true);

            //new SchemaExport(config)
            //    .Create(true, true);
        }

        public static void ResetSession()
        {
            var builder = new HybridSessionBuilder();
            //builder.GetSession().Dispose();

            ISession currentSession = CurrentSessionContext.Unbind(builder.GetSessionFactory());

            if (currentSession == null) return;

            currentSession.Close();
            currentSession.Dispose();
        }
    }

    public class StringColumnLengthConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(string)).Expect(x => x.Length == 0);
        }
        public void Apply(IPropertyInstance instance)
        {
            instance.Length(10000);
        }
    }
}

using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using NHibernate;
using NHibernate.Mapping.Attributes;
using Configuration = NHibernate.Cfg.Configuration;

namespace NHibernateExample
{
    public class NHibernateConfigurator
    {
        public NHibernateConfigurator(string connectionName)
        {
            string conectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            var configurationProperties = new Dictionary<string, string>
                {
                    {"dialect", typeof(NHibernate.Dialect.PostgreSQL82Dialect).FullName},
                    {"connection.driver_class", typeof(NHibernate.Driver.NpgsqlDriver).FullName},
                    {"connection.connection_string", conectionString},
                };

            var assemblyWithEntities = Assembly.GetExecutingAssembly();

            var serializer = HbmSerializer.Default;
            serializer.Validate = true;

            var config = new Configuration()
                .SetProperties(configurationProperties)
                .AddInputStream(serializer.Serialize(assemblyWithEntities));

            SessionFactory = config.BuildSessionFactory();
        }
        
        public ISessionFactory SessionFactory { get; private set; }

        public PersonStorage ProducePersonStorage()
        {
            return new PersonStorage(SessionFactory);
        }

        public ParentsStorage ProduceParentsStorage()
        {
            return new ParentsStorage(SessionFactory);
        }
    }
}
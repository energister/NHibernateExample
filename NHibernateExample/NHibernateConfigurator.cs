using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
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
                    {NHibernate.Cfg.Environment.Dialect, typeof (NHibernate.Dialect.PostgreSQL82Dialect).FullName},
                    {NHibernate.Cfg.Environment.ConnectionDriver, typeof (NHibernate.Driver.NpgsqlDriver).FullName},
                    {NHibernate.Cfg.Environment.ConnectionString, conectionString},
                };

            var assemblyWithEntities = Assembly.GetExecutingAssembly();

            var serializer = HbmSerializer.Default;
            serializer.Validate = true;

            MemoryStream hbmStream = serializer.Serialize(assemblyWithEntities);
            string hbmXml;
            using (var reader = new StreamReader(hbmStream))
            {
                hbmXml = reader.ReadToEnd();
            }
            
            var config = new Configuration()
                .SetProperties(configurationProperties)
                .Configure()  // add properties from app.config
                .AddXml(hbmXml);

            SessionFactory = config.BuildSessionFactory();
        }
        
        public ISessionFactory SessionFactory { get; private set; }
        
        public PersonsStorage ProducePersonsStorage()
        {
            return new PersonsStorage(SessionFactory);
        }

        public PassportsStorage ProducePassportsStorage()
        {
            return new PassportsStorage(SessionFactory);
        }
    }
}
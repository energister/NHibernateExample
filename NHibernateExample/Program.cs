using System;
using System.Collections.Generic;
using System.Reflection;

namespace NHibernateExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            var configurator = new NHibernateConfigurator();
            
            PersonsStorage storage = configurator.ProducePersonsStorage();
        }
    }
}

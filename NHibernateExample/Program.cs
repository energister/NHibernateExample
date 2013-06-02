using System;
using System.Collections.Generic;
using System.Reflection;

namespace NHibernateExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            var storage = new NHibernateConfigurator("postgres").ProduceParentStorage();
        }
    }
}

using System;
using NHibernate.Mapping.Attributes;

namespace NHibernateExample.Entities
{
    [Class(Table = "passport")]
    public class Passport
    {
        [Id(Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        /*
         uncomment to disable lazy loading at all
         * [ManyToOne(NotNull = true, Column = "person_id", Unique = true, Lazy = Laziness.False, Fetch = FetchMode.Join)]
         */
        [ManyToOne(NotNull = true, Column = "person_id", Unique = true)]  // add , Lazy = Laziness.NoProxy to disable relation loading at all
        public virtual Person Person { get; set; }

        [Property(NotNull = true)]
        public virtual int Number { get; set; }

        [Property(NotNull = true)]
        public virtual DateTime Issued { get; set; }
    }
}

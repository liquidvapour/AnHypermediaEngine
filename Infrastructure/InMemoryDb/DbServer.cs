using System;
using System.Collections.Generic;
using Core.Domain;

namespace InMemoryDb
{
    public static class DbServer
    {
        public static readonly IDictionary<Type, IList<IAggregateRoot>> Remittances = new Dictionary<Type, IList<IAggregateRoot>>();
    }
}

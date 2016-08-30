using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core.Persistency
{
    public interface IRepository
    {
        IList<T> List<T>() where T : IAggregateRoot;
        IList<T> List<T>(Func<T, bool> predicate) where T : IAggregateRoot;

        T SingleOrDefault<T>() where T : IAggregateRoot;
        T SingleOrDefault<T>(Func<T, bool> predicate) where T : IAggregateRoot;

        bool Any<T>(Func<T, bool> predicate);

        void SaveOrUpdate<T>(T aggregateRoot) where T : IAggregateRoot;
        
        void Remove<T>(T user) where T : IAggregateRoot;
    }
}
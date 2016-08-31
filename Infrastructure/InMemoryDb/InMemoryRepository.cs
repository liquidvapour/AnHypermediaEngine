using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.Persistency;
using Domain;

namespace InMemoryDb
{
    public class InMemoryRepository : IRepository
    {
        static InMemoryRepository()
        {
            foreach (var corridor in CorridorFactory.Create())
                Save(corridor);

            Save(new PaymentMethod("Bank Transfer"));

            Save(new SendingReason("Family or friend support"));
            Save(new SendingReason("Purchase of services"));
            Save(new SendingReason("Property payment"));
            Save(new SendingReason("Sending fund to self"));
        }

        public IList<T> List<T>() where T : IAggregateRoot
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)) == false)
                return new List<T>();

            var collection = (IList<T>)DbServer.Remittances[typeof(T)].Select(x => (T)x).ToList();
            return collection.ToList();
        }

        public IList<T> List<T>(Func<T, bool> predicate) where T : IAggregateRoot
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)) == false)
                return new List<T>();

            var collection = (IList<T>)DbServer.Remittances[typeof(T)].Select(x => (T)x).ToList();
            return collection.Where(predicate).ToList();
        }

        public T SingleOrDefault<T>() where T : IAggregateRoot
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)) == false)
                return default(T);

            var collection = (IList<T>)DbServer.Remittances[typeof(T)].Select(x => (T)x).ToList();
            return collection.SingleOrDefault();
        }

        public T SingleOrDefault<T>(Func<T, bool> predicate) where T : IAggregateRoot
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)) == false)
                return default(T);

            var collection = (IList<T>)DbServer.Remittances[typeof(T)].Select(x => (T)x).ToList();
            return collection.SingleOrDefault(predicate);
        }

        public bool Any<T>(Func<T, bool> predicate)
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)) == false)
                return false;

            var collection = (IList<T>)DbServer.Remittances[typeof(T)].Select(x => (T)x).ToList();
            return collection.Any(predicate);
        }

        public void SaveOrUpdate<T>(T aggregateRoot) where T : IAggregateRoot
        {
            Save(aggregateRoot);
        }

        public void Remove<T>(T aggregateRoot) where T : IAggregateRoot
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)))
            {
                var collection = DbServer.Remittances[typeof(T)];

                if (collection.Contains(aggregateRoot))
                    collection.Remove(aggregateRoot);
            }
        }

        public static void Save<T>(T aggregateRoot) where T : IAggregateRoot
        {
            if (DbServer.Remittances.ContainsKey(typeof(T)) == false)
            {
                DbServer.Remittances.Add(typeof(T), new List<IAggregateRoot> { aggregateRoot });
            }
            else
            {
                var collection = DbServer.Remittances[typeof(T)];

                var persistedAggregateRoot = collection.SingleOrDefault(x => x.Id == aggregateRoot.Id);

                if (persistedAggregateRoot != null)
                    collection.Remove(persistedAggregateRoot);

                collection.Add(aggregateRoot);
            }
        }
    }
}
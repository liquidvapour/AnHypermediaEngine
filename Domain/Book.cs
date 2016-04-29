using System;
using Core.Domain;

namespace Domain
{
    public class Book : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string Isbn { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Book()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public Book(string isbn, string name, string description) : this()
        {
            Name = name;
            Isbn = isbn;
            Description = description;
        }
    }
}
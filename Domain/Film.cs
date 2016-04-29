using System;
using Core.Domain;

namespace Domain
{
    public class Film : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string ImageThumbnail { get; private set; }

        private Film()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public Film(string name, string description, string image) : this()
        {
            Name = name;
            Description = description;
            Image = image;
            ImageThumbnail = image + "?thumbnail=true";
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
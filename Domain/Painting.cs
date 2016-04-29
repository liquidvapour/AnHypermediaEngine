using System;
using Core.Domain;

namespace Domain
{
    public class Painting : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string ImageThumbnail { get; private set; }

        private Painting()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public Painting(string name, string description, string image) : this()
        {
            Name = name;
            Description = description;
            Image = image;
            ImageThumbnail = image + "?thumbnail=true";
        }
    }
}
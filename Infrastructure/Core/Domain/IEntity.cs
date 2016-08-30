using System;

namespace Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedOn { get; }
    }
}
using System;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Authenticated.Users.Delete
{
    public class DeleteUser : ApiCommand
    {
        public Guid Id { get; set; }

        public DeleteUser() : base("Delete", "DELETE", "/api/users/{id}", Claim.Administrator)
        {
        }
    }
}
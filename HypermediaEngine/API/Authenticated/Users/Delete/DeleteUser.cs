using System;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Users.Delete
{
    public class DeleteUser : ApiAction
    {
        public Guid Id { get; set; }

        public DeleteUser() : base("Delete", "DELETE", "/api/users/{id}", Claim.Administrator)
        {
        }
    }
}
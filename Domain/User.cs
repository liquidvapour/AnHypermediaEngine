using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Core.Domain;
using Core.Primitives;

namespace Domain
{
    public class User : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Guid Salt { get; private set; }
        public IList<Guid> AccessTokens { get; private set; }

        public string Username { get; private set; }
        public string Password { get; private set; }

        public IList<Claim> Claims { get; private set; }

        private User()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;

            Salt = Guid.NewGuid();
            AccessTokens = new List<Guid>();

            Claims = new List<Claim>();
        }

        public User(string username, string password) : this()
        {
            Username = username;
            Password = GeneratePasswordHash(password);
        }


        public bool Validate(string password)
        {
            return Password == GeneratePasswordHash(password, Salt);
        }

        private string GeneratePasswordHash(string password)
        {
            return GeneratePasswordHash(password, Salt);
        }

        private static string GeneratePasswordHash(string password, Guid salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt.ToByteArray());
            return Convert.ToBase64String(pbkdf2.GetBytes(64));
        }

        public Guid IssueAccessToken()
        {
            var accessToken = Guid.NewGuid();
            AccessTokens.Add(accessToken);

            return accessToken;
        }

        public void RevokeAccessToken(Guid accessToken)
        {
            var issuedAccessToken = AccessTokens.SingleOrDefault(x => x == accessToken);
            if (issuedAccessToken != null)
                AccessTokens.Remove(issuedAccessToken);
        }

        public void SetClaims(IList<Claim> claims)
        {
            Claims = claims;
        }
    }
}
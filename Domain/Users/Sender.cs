using System.Collections.Generic;

namespace Domain.Users
{
    public class Sender : User
    {
        public IList<Recipient> Recipients { get; private set; }

        public Sender(string username, string password) : base(username, password)
        {
            Recipients = new List<Recipient>();
        }

        public void AddRecipient(Recipient recipient)
        {
            Recipients.Add(recipient);
        }
    }
}
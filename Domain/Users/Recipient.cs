using System;

namespace Domain.Users
{
    public class Recipient : User
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Postcode { get; private set; }
        public string Address { get; private set; }
        public string Name { get; private set; }
        public Guid SendingReasonId { get; private set; }

        public Recipient(string country, string city, string postcode, string address,
                            string name, Guid sendingReasonId)
        {
            Country = country;
            City = city;
            Postcode = postcode;
            Address = address;

            Name = name;

            SendingReasonId = sendingReasonId;
        }

        public void Update(string country, string city, string postcode, string address,
                            string name, Guid sendingReasonId)
        {
            Country = country;
            City = city;
            Postcode = postcode;
            Address = address;

            Name = name;

            SendingReasonId = sendingReasonId;
        }
    }
}
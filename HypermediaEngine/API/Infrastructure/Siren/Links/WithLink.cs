using System;
using HypermediaEngine.API.Infrastructure.Requests.Links;
using Siren;

namespace HypermediaEngine.API.Infrastructure.Siren.Links
{
    public class WithLink<T> where T : ApiLink
    {
        private readonly Action<Link> _override;

        private WithLink(Action<Link> @override)
        {
            _override = @override;
        }

        public static WithLink<T> Property(Action<Link> @override)
        {
            return new WithLink<T>(@override);
        }

        public void Apply(Link field)
        {
            _override.Invoke(field);
        }
    }
}
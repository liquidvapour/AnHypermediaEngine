using System;
using Siren;

namespace HypermediaEngine.API.Infrastructure.Exceptions
{
    public class HypermediaEngineException : Exception
    {
        public Entity Response { get; private set; }

        public HypermediaEngineException(Entity response)
        {
            Response = response;
        }
    }
}
using System.Collections.Generic;

namespace Acceptance.Infrastructure.Hypermedia
{
    public class ErrorResponse
    {
        public IDictionary<string, dynamic> Errors { get; set; }
    }
}

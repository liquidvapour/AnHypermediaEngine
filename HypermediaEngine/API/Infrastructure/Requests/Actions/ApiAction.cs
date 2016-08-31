namespace HypermediaEngine.API.Infrastructure.Requests.Actions
{
    public abstract class ApiAction : ApiRequest
    {
        public string Method { get; private set; }

        protected ApiAction(string title, string method, string href, bool requiresAuthentication = true) : base(title, href, requiresAuthentication)
        {
            Method = method;
        }
    }
}
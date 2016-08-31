namespace HypermediaEngine.API.Infrastructure.Requests
{
    public abstract class ApiRequest
    {
        public string Title { get; private set; }
        public string Href { get; private set; }
        public bool RequiresAuthentication { get; private set; }

        protected ApiRequest(string title, string href, bool requiresAuthentication)
        {
            Title = title;
            Href = href;
            RequiresAuthentication = requiresAuthentication;
        }
    }
}
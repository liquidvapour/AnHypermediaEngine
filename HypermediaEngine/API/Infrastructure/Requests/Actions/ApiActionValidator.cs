using FluentValidation;

namespace HypermediaEngine.API.Infrastructure.Requests.Actions
{
    public abstract class ApiActionValidator<T> : AbstractValidator<T> where T : ApiAction
    {
    }
}
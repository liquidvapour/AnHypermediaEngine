using FluentValidation;

namespace HypermediaEngine.API.Infrastructure.Requests.Commands
{
    public abstract class ApiCommandValidator<T> : AbstractValidator<T> where T : ApiRequest
    {
    }
}
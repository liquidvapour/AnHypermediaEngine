using System.Linq;
using Core.Persistency;
using HypermediaEngine.API.Authenticated.Books.Get;
using HypermediaEngine.API.Authenticated.Books.List;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.Books
{
    public class BooksModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;

        public BooksModule(IRepository repository)
        {
            _repository = repository;

            Get["/api/books"] = _ => this.Query<GetBooks, BooksCollection>(Handle);
            Get["/api/books/{id}"] = _ => this.Query<GetBook, Book>(Handle);
        }

        private BooksCollection Handle(GetBooks request)
        {
            var books = _repository.List<Domain.Book>().OrderBy(x => x.CreatedOn);
            return new BooksCollection(Context, books);
        }

        private Book Handle(GetBook request)
        {
            var book = _repository.SingleOrDefault<Domain.Book>(x => x.Id == request.Id);
            if (book == null)
                throw new HypermediaEngineException(new NotFoundResponse(Context));

            return new Book(Context, book);
        }
    }
}
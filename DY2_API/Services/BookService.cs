using DY2_API.Domain;
using DY2_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DY2_API.Services
{
    public class BookService : IBookService
    {
        private readonly LibContext _libContext;
        public BookService(LibContext context)
        {
            _libContext = context;
        }

        public async Task<BookDto?> AddBook(BookDto? dto)
        {
            Author? author = await _libContext.Authors.SingleOrDefaultAsync(a => a.Id == dto.AuthorId);
            if (author == null) return null;

            Book book = new Book()
            {
                Name = dto.Name,
                Publisher = dto.Publisher,
                Author = author
            };

            _libContext.Books.Add(book);
            _libContext.SaveChanges();

            return new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                Publisher = book.Publisher,
                AuthorId = book.AuthorId,   
                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName
            };
        }

        public async Task<bool> Delete(int bookId)
        {
            Book? book = _libContext.Books.SingleOrDefault(a => a.Id == bookId);
            if (book == null) return false;

            _libContext.Books.Remove(book);
            await _libContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDto?>> GetAllBooks()
        {
            var books = await _libContext.Books.Include(b=>b.Author).ToListAsync();

            List<BookDto?> result = new List<BookDto?>();
            foreach (var book in books)
            {
                result.Add(new BookDto()
                {
                    Id = book.Id,
                    Name = book.Name,
                    Publisher = book.Publisher,
                    AuthorId = book.Author.Id, 
                    AuthorFirstName = book.Author.FirstName,
                    AuthorLastName = book.Author.LastName
                });
            }

            return result;
        }

        public async Task<BookDto?> GetBook(int id)
        {
            var book = await _libContext.Books
                .Include(b => b.Author)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            return new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                Publisher = book.Publisher,
                AuthorId = book.Author.Id, 
                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName
            };
        }

        public async Task<BookDto?> Replace(int bookId, BookDto? dto)
        {
            Book? book = await _libContext.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book is null) return null;

            if (dto.Name != null) return null;
            if (dto.Publisher != null) return null;
            if (dto.AuthorId != null) return null;

            await _libContext.SaveChangesAsync();

            return new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                Publisher = book.Publisher,
                AuthorId = book.Author.Id,
                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName
            };
        }

        public async Task<List<BookDto?>> Search(string? name, string? publisher, string? authorFirst, string? authorLast)
        {
            IQueryable<Book> results = _libContext.Books.Include(b => b.Author);
            
            if (name is not null) results = results.Where(b => b.Name == name);
            if (publisher is not null) results = results.Where(b => b.Publisher == publisher);
            if (authorFirst is not null) results = results.Where(b => b.Author.FirstName == authorFirst);
            if (authorFirst is not null) results = results.Where(b => b.Author.LastName == authorLast);

            List<Book> books = await results.ToListAsync();
            List<BookDto?> bookDtos = new List<BookDto?>();
            foreach (var book in books)
            {
                bookDtos.Add(new BookDto()
                {
                    Id = book.Id,
                    Name = book.Name,
                    Publisher = book.Publisher,
                    AuthorId = book.Author.Id,
                    AuthorFirstName = book.Author.FirstName,
                    AuthorLastName = book.Author.LastName
                });
            }

            return bookDtos;
        }

        public async Task<BookDto?> Update(int bookId, BookDto dto)
        {
            Book? book = await _libContext.Books.Include(b=>b.Author).SingleOrDefaultAsync(b => b.Id == bookId);
            if (book is null) return null;
           

            if (dto.Name != null) book.Name = dto.Name;
            if (dto.Publisher != null) book.Publisher = dto.Publisher;
            if (dto.AuthorId != null) book.Author = _libContext.Authors.Find(dto.AuthorId);

            await _libContext.SaveChangesAsync();

            return new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                Publisher = book.Publisher,
                AuthorId = book.AuthorId,
                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName
            };
        }

    }
}

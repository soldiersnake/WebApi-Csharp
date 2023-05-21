using Dapper;
using FirstAPI.Models;
using System.Data;

namespace FirstAPI.Data
{
    public class BookRepository : IBookRepository
    {

        private readonly IDbConnection _dbConnection;

        public BookRepository(IDbConnection dbConnection) {
            _dbConnection = dbConnection;
        }

        public async Task Delete(int id)
        {
            var sql = @" DELETE FROM Books
                    WHERE Id = @Id ";

            await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Id = id,
                });
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var sql = @" SELECT id
                        ,Title
                        ,Autor
                        ,IsAvailable
                        FROM Books";

            return await _dbConnection.QueryAsync<Book>(sql, new { });
        }

        public async Task<Book> GetDetails(int id)
        {
            var sql = @" SELECT id,
                        Title,
                        Autor,
                        IsAvailable
                        FROM Books
                        WHERE Id = @id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Book>(sql, new { Id = id });
        }

        public async Task Insert(Book book)
        {
            var sql = @" INSERT INTO Books (Id, Title, Autor, IsAvailable)
                        VALUES(@Id, @Title, @Autor, @IsAvailable) ";

            await _dbConnection.ExecuteAsync(sql,
                new { 
                    book.Id,
                    book.Title,
                    book.Autor,
                    book.IsAvailable

                });
        }

        public async Task Update(Book book)
        {
            var sql = @" UPDATE Books
                    SET Title = @Title,
                        Autor = @Autor,
                        IsAvailable = @IsAvailable
                    WHERE Id = @Id ";

            await _dbConnection.ExecuteAsync(sql,
                new
                {
                    book.Id,
                    book.Title,
                    book.Autor,
                    book.IsAvailable
                });
        }
    }
}

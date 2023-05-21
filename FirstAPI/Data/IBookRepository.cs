using FirstAPI.Models;

namespace FirstAPI.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll(); // se le inserta Task para hacerlo asincronico debido a las consultas
        Task<Book> GetDetails (int id);

        Task Insert(Book book);
        Task Update(Book book);
        Task Delete(int id);
    }
}

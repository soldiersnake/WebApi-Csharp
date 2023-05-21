using FirstAPI.Data;
using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //localhost:port/api/books
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository; //sera una variable global x eso se declara con "_"
        public BooksController(IBookRepository repository)
        {
            _repository = repository;  //aqui realizamos la inyeccion de dependencias
        }

        //GET: api/books
        [HttpGet] //decorador, indica el metodo para generar su request
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _repository.GetAll());
        }

        //GET: api/books/:id
        [HttpGet("{id}")] //decorador indicando parametros, en este caso el id recibido
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            //var book = await _context.Books.FindAsync(id); //busca en en DB utilizando entityFramework
            var book = await _repository.GetDetails(id); //ya utilizando SQL x medio de repositorio

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        //POST: api/books/
        [HttpPost] //decorador indicando metodo POST
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            //_context.Books.Add(book); //primero se realizan todos los cambios con entityframework
            //await _context.SaveChangesAsync(); //aqui se guardan los cambios

            await _repository.Insert(book);

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        //PUT: api/books/:id
        [HttpPut("{id}")] //decorador metodo PUT, indicando el id a actualizar
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            //var bookInDb = await _context.Books.FindAsync(id); // con entityframework

            var bookInDb = await _repository.GetDetails(id);

            if (bookInDb == null)
                return NotFound();
             
            //bookInDb.Title = book.Title;  //con entityframework
            //bookInDb.Autor = book.Autor;
            //bookInDb.IsAvailable = book.IsAvailable;
            //await _context.SaveChangesAsync(); // con entityframework

            await _repository.Update(book);

            return NoContent();
        }

        //DELETE: api/books/:id
        [HttpDelete("{id}")] //decorador metodo DELETE, indicando el id a eliminar
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            //var book = await _context.Books.FindAsync(id); //busca en en DB utilizando entityFramework, verificando su xistencia

            var book = await _repository.GetDetails(id);

            if (book == null)
                return NotFound();

            //_context.Books.Remove(book); // con entityframework
            //await _context.SaveChangesAsync();

            await _repository.Delete(id);

            return book;

        }
    }
}

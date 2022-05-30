
using Microsoft.AspNetCore.Mvc;
using PeliculaItem;
using Peliculas.Data;

namespace PeliculasController;





[ApiController]
[Route("[Controller]")]

public class PeliculasController : ControllerBase
{

    private readonly DataContext _context;

    public PeliculasController(DataContext dataContext)
    {
        _context = dataContext;
    }

    [HttpGet]
    public ActionResult<List<PeliculaItems>> Get(){
    return Ok (_context.PeliculaItem);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<PeliculaItems> Get(int id)
    {
    var PeliculaItems = _context.PeliculaItem.Find(id);
        return PeliculaItems == null? NotFound()
            : Ok(PeliculaItems);
    }
    /*
    [HttpGet]
    public ActionResult<List<CarritoItems>> Get()
    {
        List<CarritoItems> carrito = _context.CarritoItem.ToList();
        return carrito == null? NoContent()
            : Ok(carrito);
    }
*/

    [HttpPost]
    public ActionResult<PeliculaItems> Post([FromBody] PeliculaItems categoria)
    {
         PeliculaItems existingPeliculaItems= _context.PeliculaItem.Find(categoria.id);
        if (existingPeliculaItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.PeliculaItem.Add(categoria);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + categoria.id;
        return Created(resourceUrl, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PeliculaItems> Update([FromBody] PeliculaItems titulo)
    {
        PeliculaItems peliculaItemToUpdate = _context.PeliculaItem.Find(titulo);
        if (peliculaItemToUpdate == null)
        {
            return NotFound("Elemento del Peliculas no encontrado");
        }
        peliculaItemToUpdate.categoria = titulo.categoria;
        peliculaItemToUpdate.id = titulo.id;
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + titulo.id;

        return Created(resourceUrl, titulo);
    }
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        PeliculaItems peliculaItemToDelete = _context.PeliculaItem.Find(id);
        if (peliculaItemToDelete == null)
        {
            return NotFound("Elemento del peliculas no encontrado");
        }
        _context.PeliculaItem.Remove(peliculaItemToDelete);
        _context.SaveChanges();
        return Ok(peliculaItemToDelete);
    }

}

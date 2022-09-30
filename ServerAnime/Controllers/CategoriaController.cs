using Microsoft.AspNetCore.Mvc;
using ServerAnime.Data.Repositories;
using ServerAnime.Model;
using ServerAnime.Model.ModelDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAnime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IGenericRepository<Categorium> _categoriaRepo;

        public CategoriaController(IGenericRepository<Categorium> categoriaRepo) => _categoriaRepo = categoriaRepo;

        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? page)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    pages = CategoriaRepository.Total_pages,
                    data = await _categoriaRepo.GetAllAsync(page),
                    currentPage = CategoriaRepository._Page
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Categorium modelo = await _categoriaRepo.GetOneAsync(id);
                if (modelo != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { etado = true, data = modelo });
                }
                return StatusCode(StatusCodes.Status404NotFound, new { etado = false, mesagge = $"Categoria con Id: {id} no existe" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categorium modelo)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await _categoriaRepo.CreateAsync(modelo));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Categorium modelo)
        {
            try
            {
                bool status = await _categoriaRepo.UpdateOneAsync(id, modelo);
                return this.ValidIfExistElement(status, $"Categoria con Id: {id} no existe", $"Categoria con Id: {id} se actualizo con exito");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool status = await _categoriaRepo.DeleteOneAsync(id);
                return this.ValidIfExistElement(status, $"Categoria con Id: {id} no existe", $"Categoria con Id: {id} fue eliminado con exito");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        //metodo para valiodar las respuestas de la peticiones
        private ObjectResult ValidIfExistElement(bool itemStatus, string msgError, string msgOk)
        {
            if (itemStatus)
            {
                return StatusCode(StatusCodes.Status200OK, new { etado = itemStatus, mesagge = msgOk });
            }
            return StatusCode(StatusCodes.Status404NotFound, new { etado = itemStatus, mesagge = msgError });
        }
    }
}

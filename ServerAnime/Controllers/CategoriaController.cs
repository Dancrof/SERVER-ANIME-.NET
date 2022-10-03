using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ServerAnime.Data.Repositories;
using ServerAnime.Model;
using ServerAnime.Model.ModelDto;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAnime.Controllers
{
    [EnableCors("Reglascors")]
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IGenericRepository<Categorium> _categoriaRepo;
        private readonly IMapper _mapper;

        public CategoriaController(IGenericRepository<Categorium> categoriaRepo, IMapper mapper)
        {
            this._categoriaRepo = categoriaRepo;
            this._mapper = mapper;
        }

        // GET: api/categoria
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

        // GET: api/categoria/filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterByName([FromQuery][Required] string filter)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _categoriaRepo.GetAllByName(filter));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // GET api/categoria/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById(int id)
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

        // POST api/categoria
        [HttpPost]
        public async Task<IActionResult> PostNewCtegoria([FromBody] CategoriaDto modelo)
        {
            try
            {              
                return this.ValidIfExistElementByEntity(await _categoriaRepo.CreateAsync(this.RequesCategoria(modelo)), $"Categoria con Nombre: {modelo.Nombre} ya existe", $"Categoria con Nombre: {modelo.Nombre} se creo con exito");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // PUT api/categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([Required] int id, [FromBody] CategoriaDto modelo)
        {
            try
            {
                bool status = await _categoriaRepo.UpdateOneAsync(id, this.RequesCategoria(modelo));
                return this.ValidIfExistByBoll(status, $"Categoria con Id: {id} no existe", $"Categoria con Id: {id} se actualizo con exito");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // DELETE api/categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool status = await _categoriaRepo.DeleteOneAsync(id);
                return this.ValidIfExistByBoll(status, $"Categoria con Id: {id} no existe", $"Categoria con Id: {id} fue eliminado con exito");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        //metodo para valiodar las respuestas de la peticiones put y delete
        private ObjectResult ValidIfExistByBoll(bool itemStatus, string msgError, string msgOk)
        {
            if (itemStatus)
            {
                return StatusCode(StatusCodes.Status200OK, new { etado = itemStatus, mesagge = msgOk });
            }
            return StatusCode(StatusCodes.Status404NotFound, new { etado = itemStatus, mesagge = msgError });
        }

        //metodo para valiodar las respuestas de la peticiones post
        private ObjectResult ValidIfExistElementByEntity(Categorium itemStatus, string msgError, string msgOk)
        {
            if (itemStatus != null)
            {
                return StatusCode(StatusCodes.Status201Created, new { mesagge = msgOk, etado = itemStatus, });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { mesagge = msgError, etado = itemStatus });
        }

        //convietre el json a objeto categoria
        private Categorium RequesCategoria(CategoriaDto categoriaDto)
        {
            return new Categorium() { Nombre = categoriaDto.Nombre };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ServerAnime.Data.Repositories;
using ServerAnime.Model;
using ServerAnime.Model.ModelDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAnime.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IGenericRepository<Role> _roleRepository;

        public RolController(IGenericRepository<Role> roleRepository) => _roleRepository = roleRepository;
        

        // GET: api/<RolController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pag)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new {
                    pages = RolRepository.Total_pages,
                    data = await _roleRepository.GetAllAsync(pag),
                    current_page = RolRepository._Page
                });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Role rol = await _roleRepository.GetOneAsync(id);
                return this.ValidIfExistElementByEntity(rol,$"Este Rol con id: {id} no existe","Ok");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // POST api/<RolController>
        [HttpPost]
        public async Task<IActionResult> PostNewRol([FromBody] RolDto modelo)
        {
            try
            {
                return this.ValidIfExistElementByEntity(await _roleRepository.CreateAsync(this.MapperRol(modelo)), $"Rol con nombre: {modelo.Rol} ya existe", $"Rol creado");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }
        }

        // PUT api/<RolController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        //mapper del modelo rol
        private Role MapperRol(RolDto modelo) 
        {
            return new Role() { Rol = modelo.Rol };
        }

        //metodo para valiodar las respuestas de la peticiones post
        private ObjectResult ValidIfExistElementByEntity(Role itemStatus, string msgError, string msgOk)
        {
            if (itemStatus != null)
            {
                return StatusCode(StatusCodes.Status200OK, new { mesagge = msgOk, etado = itemStatus, });
            }
            return StatusCode(StatusCodes.Status404NotFound, new { mesagge = msgError, etado = itemStatus });
        }

        //metodo para valiodar las respuestas de la peticiones put y delete
        private ObjectResult ValidIfExistByBool(bool itemStatus, string msgError, string msgOk)
        {
            if (itemStatus)
            {
                return StatusCode(StatusCodes.Status200OK, new { etado = itemStatus, mesagge = msgOk });
            }
            return StatusCode(StatusCodes.Status404NotFound, new { etado = itemStatus, mesagge = msgError });
        }
    }
}

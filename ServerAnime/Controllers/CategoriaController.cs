using Microsoft.AspNetCore.Mvc;
using ServerAnime.Data.Repositories;
using ServerAnime.Model;

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
        public async Task<IActionResult> Get()
        {
            //await _categoriaRepo.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, await _categoriaRepo.GetAllAsync());
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categorium modelo)
        {
            // await _categoriaRepo.CreateAsync(modelo);
            return StatusCode(StatusCodes.Status201Created, await _categoriaRepo.CreateAsync(modelo));
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

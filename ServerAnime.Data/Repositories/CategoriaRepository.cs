using Microsoft.EntityFrameworkCore;
using ServerAnime.Data.DataContext;
using ServerAnime.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAnime.Data.Repositories
{
    public class CategoriaRepository : IGenericRepository<Categorium>
    {
        private readonly serveranimedbContext _dbContext;

        public CategoriaRepository(serveranimedbContext context) => _dbContext = context;

        public async Task<Categorium> CreateAsync(Categorium modelo)
        {
            await _dbContext.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();

            return modelo;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync();          
            return true;
        }

        public async Task<IEnumerable<Categorium>> GetAllAsync()
        {
            return  _dbContext.Categoria.Include(e=> e.Catalogos).ToList();
        }

        public async Task<Categorium> GetOneAsync(int id)
        {
           return  _dbContext.Categoria.First(e => e.Id == id);
        }

        public async Task<bool> UpdateOneAsync(int id, Categorium modelo)
        {          
            _dbContext.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

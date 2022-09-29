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

        //cantidad de elementos por pagina
        private readonly int quantityShow = 10;
        //VAriables publicas de paginacion
        public static int _page;
        public static decimal total_quantityShow;
        public static int total_pages;
        
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

        public async Task<IEnumerable<Categorium>> GetAllAsync(int? page)
        {
            _page = page ?? 1;
            total_quantityShow = await _dbContext.Categoria.CountAsync();
            total_pages = (int)Math.Ceiling(total_quantityShow / quantityShow);
            
            return  await _dbContext.Categoria.Skip((_page - 1) * quantityShow).Take(quantityShow).Include(e=> e.Catalogos).ToListAsync();
        }

        public async Task<Categorium> GetOneAsync(int id)
        {
           return  _dbContext.Categoria.First(e => e.Id == id);
        }

        public async Task<bool> UpdateOneAsync(int id, Categorium modelo)
        {
            modelo.UpdateAt = new DateTime();
            _dbContext.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

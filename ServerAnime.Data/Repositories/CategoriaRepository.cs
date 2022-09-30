﻿using Microsoft.EntityFrameworkCore;
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

        //propiedades de paginacion
        public static int _Page { get; set; }
        public static decimal Total_quantityShow { get; set; }
        public static int Total_pages { get; set; }

        public CategoriaRepository(serveranimedbContext context) => _dbContext = context;

        public async Task<Categorium> CreateAsync(Categorium modelo)
        {
            Categorium filter = await GetOneAsync(modelo.Id);
            
            if(filter == null)
            {
                await _dbContext.AddAsync(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            return null; 
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            Categorium filter = await GetOneAsync(id);
            if (filter != null)
            {
                _dbContext.Categoria.Remove(filter);
                await _dbContext.SaveChangesAsync();
                return true;
            }              
            return false;
        }

        public async Task<IEnumerable<Categorium>> GetAllAsync(int? page)
        {
            _Page = page ?? 1;
            Total_quantityShow = await _dbContext.Categoria.CountAsync();
            Total_pages = (int)Math.Ceiling(Total_quantityShow / quantityShow);
            
            return  _dbContext.Categoria.Skip((_Page - 1) * quantityShow).Take(quantityShow).Include(e => e.Catalogos);
        }

        public async Task<Categorium> GetOneAsync(int id)
        {
           return await _dbContext.Categoria.Include(e=>e.Catalogos).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> UpdateOneAsync(int id, Categorium modelo)
        {
            Categorium filter = await GetOneAsync(id);
            if (filter != null)
            {
                modelo.UpdateAt = new DateTime();
                _dbContext.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

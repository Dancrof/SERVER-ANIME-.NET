using Microsoft.EntityFrameworkCore;
using ServerAnime.Data.DataContext;
using ServerAnime.Model;
using System.Collections.Generic;

namespace ServerAnime.Data.Repositories
{
    public class RolRepository : IGenericRepository<Role>
    {
        private readonly serveranimedbContext _context;

        //cantidad de elementos por pagina
        private readonly int quantityShow = 10;

        //propiedades de paginacion
        public static int _Page { get; set; }
        public static decimal Total_quantityShow { get; set; }
        public static int Total_pages { get; set; }

        public RolRepository(serveranimedbContext context) => this._context = context;

        public async Task<Role> CreateAsync(Role modelo)
        {
            IEnumerable<Role> role = await GetAllByName(modelo.Rol);
            if (role.Any())
            {
                return null;
            }          
            await _context.AddAsync(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            Role role = await GetOneAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Role>> GetAllAsync(int? page)
        {
            _Page = page ?? 1;
            Total_quantityShow = await _context.Roles.CountAsync();
            Total_pages = (int)Math.Ceiling(Total_quantityShow / quantityShow);

            return await _context.Roles.Skip((_Page - 1) * quantityShow).Take(quantityShow).Include(e => e.Usuarios).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllByName(string filter)
        {
            return await _context.Roles.Where(e => e.Rol.Contains(filter)).Include(e => e.Usuarios).ToListAsync();
        }

        public async Task<Role> GetOneAsync(int id)
        {
            return _context.Roles.FirstOrDefault(e => e.Id == id);
        }

        public async Task<bool> UpdateOneAsync(int id, Role modelo)
        {
            Role role = await GetOneAsync(id);
            if (role != null)
            {
                modelo.UpdateAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss"));
                role.Rol = modelo.Rol;
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

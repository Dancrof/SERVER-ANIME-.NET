using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAnime.Data.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync(int? page);
        Task<Entity> CreateAsync(Entity modelo);
        Task<Entity> GetOneAsync(int id);
        Task<bool> UpdateOneAsync(int id, Entity modelo);
        Task<bool> DeleteOneAsync(int id);
        Task<IEnumerable<Entity>> GetAllByName(string filter);
    }
}

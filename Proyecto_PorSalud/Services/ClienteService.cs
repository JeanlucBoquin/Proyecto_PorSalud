using Proyecto_PorSalud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Proyecto_PorSalud.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _db;
        public ClienteService(AppDbContext db) => _db = db;

        public async Task<PagedResult<Cliente>> GetPagedAsync(int page, int pageSize, string search)
        {
            var q = _db.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c => c.NombreCompleto.Contains(search) || c.Identificacion.Contains(search));

            var total = await q.CountAsync();
            var items = await q.OrderBy(c => c.Id)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

            return new PagedResult<Cliente> { Items = items, Page = page, PageSize = pageSize, TotalItems = total };
        }

        public Task<Cliente> GetByIdAsync(int id) => _db.Clientes.Include(c => c.Documentos).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> CreateAsync(Cliente c)
        {
            _db.Clientes.Add(c);
            await _db.SaveChangesAsync();
            return c.Id;
        }

        public async Task UpdateAsync(Cliente c)
        {
            _db.Entry(c).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c != null) { _db.Clientes.Remove(c); await _db.SaveChangesAsync(); }
        }
    }
}
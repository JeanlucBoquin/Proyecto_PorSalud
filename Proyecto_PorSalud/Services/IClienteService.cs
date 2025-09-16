using Proyecto_PorSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PorSalud.Services
{
    public interface IClienteService
    {
        Task<PagedResult<Cliente>> GetPagedAsync(int page, int pageSize, string search);
        Task<Cliente> GetByIdAsync(int id);
        Task<int> CreateAsync(Cliente c);
        Task<bool> UpdateAsync(Cliente c);
        Task DeleteAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tostao.Domain.Entities;

namespace Tostao.Application.Interfaces
{
    public interface IDocumentoRepository
    {
        Task<Documento> CreateAsync(Documento documento);
        Task<Documento> GetByIdAsync(Guid id);
        Task<IEnumerable<Documento>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Documento> UpdateAsync(Documento documento);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Documento>> SearchAsync(string autor, string tipo, string estado);
        Task<bool> ExistsAsync(Guid id);
    }
}

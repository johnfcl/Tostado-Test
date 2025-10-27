using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tostao.Application.DTOs;

namespace Tostao.Application.Interfaces
{
    public interface IDocumentoService
    {
        Task<DocumentoReadDto> CreateAsync(DocumentoCreateDto dto);
        Task<DocumentoReadDto> GetByIdAsync(Guid id);
        Task<IEnumerable<DocumentoReadDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<DocumentoReadDto> UpdateAsync(Guid id, DocumentoCreateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<DocumentoReadDto>> SearchAsync(string autor, string tipo, string estado);
    }
}

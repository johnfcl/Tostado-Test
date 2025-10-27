using Microsoft.EntityFrameworkCore;
using Tostao.Application.Interfaces;
using Tostao.Domain.Entities;
using Tostao.Infraestructure.Context;

namespace Tostao.Infraestructure.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly TostaoAppDbContext _context;

        public DocumentoRepository(TostaoAppDbContext context) => _context = context;

        public async Task<Documento> CreateAsync(Documento documento)
        {
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task<Documento> GetByIdAsync(Guid id)
        {
            return await _context.Documentos
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Documento>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Documentos
                .AsNoTracking()
                .OrderByDescending(d => d.FechaRegistro)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Documento> UpdateAsync(Documento documento)
        {
            _context.Documentos.Update(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null) return false;

            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Documento>> SearchAsync(string autor, string tipo, string estado)
        {
            var query = _context.Documentos.AsNoTracking();

            if (!string.IsNullOrEmpty(autor))
                query = query.Where(d => d.Autor.Contains(autor));

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(d => d.Tipo == tipo);

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(d => d.Estado == estado);

            return await query
                .OrderByDescending(d => d.FechaRegistro)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Documentos.AnyAsync(d => d.Id == id);
        }
    }
}

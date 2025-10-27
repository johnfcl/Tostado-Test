using Tostao.Application.DTOs;
using Tostao.Application.Interfaces;
using Tostao.Domain.Entities;

namespace Tostao.Infrastructure
{

        public class DocumentoService : IDocumentoService
        {
            private readonly IDocumentoRepository _documentoRepository;

            public DocumentoService(IDocumentoRepository documentoRepository)
                => _documentoRepository = documentoRepository;

            public async Task<DocumentoReadDto> CreateAsync(DocumentoCreateDto dto)
            {
                var documento = new Documento
                {
                    Titulo = dto.Titulo,
                    Autor = dto.Autor,
                    Tipo = dto.Tipo,
                    Estado = "Registrado",
                    RutaArchivo = dto.RutaArchivo
                };

                var created = await _documentoRepository.CreateAsync(documento);

                return new DocumentoReadDto(
                    created.Id, created.Titulo, created.Autor, created.Tipo,
                    created.Estado, created.FechaRegistro, created.FechaValidacion, created.RutaArchivo
                );
            }

            public async Task<DocumentoReadDto> GetByIdAsync(Guid id)
            {
                var documento = await _documentoRepository.GetByIdAsync(id);
                if (documento == null) throw new KeyNotFoundException("Documento no encontrado");

                return new DocumentoReadDto(
                    documento.Id, documento.Titulo, documento.Autor, documento.Tipo,
                    documento.Estado, documento.FechaRegistro, documento.FechaValidacion, documento.RutaArchivo
                );
            }

            public async Task<IEnumerable<DocumentoReadDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
            {
                var documentos = await _documentoRepository.GetAllAsync(pageNumber, pageSize);

                return documentos.Select(d => new DocumentoReadDto(
                    d.Id, d.Titulo, d.Autor, d.Tipo, d.Estado,
                    d.FechaRegistro, d.FechaValidacion, d.RutaArchivo
                ));
            }

            public async Task<DocumentoReadDto> UpdateAsync(Guid id, DocumentoCreateDto dto)
            {
                var exists = await _documentoRepository.ExistsAsync(id);
                if (!exists) throw new KeyNotFoundException("Documento no existe");

                var documento = new Documento
                {
                    Id = id,
                    Titulo = dto.Titulo,
                    Autor = dto.Autor,
                    Tipo = dto.Tipo,
                    RutaArchivo = dto.RutaArchivo,
                    Estado = dto.Estado,
                };

                var updated = await _documentoRepository.UpdateAsync(documento);

                return new DocumentoReadDto(
                    updated.Id, updated.Titulo, updated.Autor, updated.Tipo,
                    updated.Estado, updated.FechaRegistro, updated.FechaValidacion, updated.RutaArchivo
                );
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                return await _documentoRepository.DeleteAsync(id);
            }

            public async Task<IEnumerable<DocumentoReadDto>> SearchAsync(string autor, string tipo, string estado)
            {
                var documentos = await _documentoRepository.SearchAsync(autor, tipo, estado);

                return documentos.Select(d => new DocumentoReadDto(
                    d.Id, d.Titulo, d.Autor, d.Tipo, d.Estado,
                    d.FechaRegistro, d.FechaValidacion, d.RutaArchivo
                ));
            }
        }
    }

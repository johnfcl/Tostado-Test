using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tostao.Application.DTOs;
using Tostao.Application.Interfaces;

namespace Tostao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsApi")]
    public class DocumentosController : ControllerBase
    {

        private readonly IDocumentoService _documentoService;

        public DocumentosController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }


        [HttpPost]
        public async Task<ActionResult<DocumentoReadDto>> CreateDocumento(DocumentoCreateDto dto)
        {
            try
            {
                var documento = await _documentoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetDocumento), new { id = documento.Id }, documento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentoReadDto>>> GetDocumentos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var documentos = await _documentoService.GetAllAsync(pageNumber, pageSize);
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentoReadDto>> GetDocumento(Guid id)
        {
            try
            {
                var documento = await _documentoService.GetByIdAsync(id);
                return Ok(documento);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentoReadDto>> UpdateDocumento(Guid id, DocumentoCreateDto dto)
        {
            try
            {
                var documento = await _documentoService.UpdateAsync(id, dto);
                return Ok(documento);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocumento(Guid id)
        {
            try
            {
                var result = await _documentoService.DeleteAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<DocumentoReadDto>>> SearchDocumentos(
            [FromQuery] string autor = null,
            [FromQuery] string tipo = null,
            [FromQuery] string estado = null)
        {
            try
            {
                var documentos = await _documentoService.SearchAsync(autor, tipo, estado);
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("checkHealth")]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "API is running",
                timestamp = DateTime.UtcNow,
                message = "Gestión Documental Tostao API"
            });

        }
    }
}


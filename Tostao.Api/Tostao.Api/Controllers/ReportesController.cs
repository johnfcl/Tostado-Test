using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tostao.Api.Controllers
{
    [ApiController]
    [Route("api/reportes")]
    public class ReportesController : ControllerBase
    {
        //private readonly ReporteService _reporteService;

        //public ReportesController(IConfiguration config)
        //{
        //    _reporteService = new ReporteService(config.GetConnectionString("GetionDocumentalTostaoBD"));
        //}

        //[HttpGet("autor-tipo")]
        //public IActionResult GetAutorTipo()
        //{
        //    var result = _reporteService.ObtenerReporteAutorTipo();
        //    return Ok(result);
        //}
    }
}

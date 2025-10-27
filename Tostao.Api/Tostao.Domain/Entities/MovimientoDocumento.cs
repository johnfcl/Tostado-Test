using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tostao.Domain.Entities
{
    public class MovimientoDocumento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DocumentoId { get; set; }
        public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public string Observaciones { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tostao.Domain.Entities
{
    public class Documento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaValidacion { get; set; }
        public string RutaArchivo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tostao.Application.DTOs
{
    public class DocumentoCreateDto
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Tipo { get; set; }
        public string RutaArchivo { get; set; }
        public string Estado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tostao.Application.DTOs
{
    public class DocumentoReadDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public string RutaArchivo { get; set; }

        public DocumentoReadDto(Guid id, string titulo, string autor, string tipo,
                              string estado, DateTime fechaRegistro, DateTime? fechaValidacion, string rutaArchivo)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Tipo = tipo;
            Estado = estado;
            FechaRegistro = fechaRegistro;
            FechaValidacion = fechaValidacion;
            RutaArchivo = rutaArchivo;
        }
    }
}

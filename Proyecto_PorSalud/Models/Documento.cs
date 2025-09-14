using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_PorSalud.Models
{
    public class Documento
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public virtual Cliente Cliente { get; set; }

        [Required, MaxLength(200)]
        public string Titulo { get; set; }

        [MaxLength(260)]
        public string RutaRelativa { get; set; } // ej: ~/App_Data/Uploads/abc.pdf

        [MaxLength(128)]
        public string ContentType { get; set; } // application/pdf

        public long Length { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
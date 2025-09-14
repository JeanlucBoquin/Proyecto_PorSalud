using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Proyecto_PorSalud.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Identificacion { get; set; }

        [Required, MaxLength(150)]
        public string NombreCompleto { get; set; }

        [MaxLength(250)]
        public string Direccion { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(20)]
        public string Celular { get; set; }

        [EmailAddress, MaxLength(120)]
        public string Correo { get; set; }

        [Required, MaxLength(1)] // "M" o "F"
        public string Sexo { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}
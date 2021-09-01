//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP_DS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.compra = new HashSet<compra>();
            this.usuariorol = new HashSet<usuariorol>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio") ]
        [DataType (DataType.Text)]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.Text)]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.Date)]
        public System.DateTime fecha_nacimiento { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [StringLength(60, ErrorMessage = "Maximo 60 caracteres")]
        public string email { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Maximo 80 caracteres")]
        public string password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra> compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuariorol> usuariorol { get; set; }
    }
}

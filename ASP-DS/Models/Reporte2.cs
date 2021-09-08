using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_DS.Models
{
    public class Reporte2
    {
        public DateTime fechaCompra { get; set; }
        public int totalCompra { get; set; }
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
        public int idCliente { get; set; }
        public int cantidad { get; set; }
        
    }
}
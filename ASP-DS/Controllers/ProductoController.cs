using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;
using Rotativa;

namespace ASP_DS.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto.ToList());

            }

        }

        public static string nombreProveedor(int idProveedor)
        {
            using (var db = new inventario2021Entities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto Producto)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto.Add(Producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error " + e);
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto productoEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(productoEdit);
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error " + e);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(producto productoEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldProducto = db.producto.Find(productoEdit.id);
                    oldProducto.nombre = productoEdit.nombre;
                    oldProducto.descripcion = productoEdit.descripcion;
                    oldProducto.percio_unitario = productoEdit.percio_unitario;
                    oldProducto.cantidad = productoEdit.cantidad;
                    oldProducto.id_proveedor = productoEdit.id_proveedor;

                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error " + e);
                return View();
            }

        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto producto = db.producto.Find(id);
                    db.producto.Remove(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error " + e);
                return View();
            }
        }
        public ActionResult Reporte()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario

                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }


        }
        public ActionResult PdfReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "reporte.pdf" };
        }
    }
}
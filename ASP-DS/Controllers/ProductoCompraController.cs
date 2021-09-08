using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;

namespace ASP_DS.Controllers
{
    [Authorize]
    public class ProductoCompraController : Controller
    {
        
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.ToList());

            }

        }

        public static string nombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }
        public static DateTime FechaCompra(int idCompra)
        {
            using (var db = new inventario2021Entities())
            {
                return db.compra.Find(idCompra).fecha;
            }
        }



        public ActionResult ListarProductos()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }
        public ActionResult ListarCompra()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.compra.ToList());
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_compra producto_Compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_compra.Add(producto_Compra);
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
                return View(db.producto_compra.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var productoCompraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(productoCompraEdit);
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

        public ActionResult Edit(producto_compra producto_CompraEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldProductoCompra = db.producto_compra.Find(producto_CompraEdit.id);
                    oldProductoCompra.cantidad = producto_CompraEdit.cantidad;
                    oldProductoCompra.id_compra = producto_CompraEdit.id_compra;
                    oldProductoCompra.id_producto = producto_CompraEdit.id_producto; 


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
                    producto_compra producto_Compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(producto_Compra);
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
    }
}
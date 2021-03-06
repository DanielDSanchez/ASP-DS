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
    public class CompraController : Controller
    {
        
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.compra.ToList());

            }

        }

        public static string nombreUsuario(int idUsuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }
        public static string nombreCliente(int idCliente)
        {
            using (var db = new inventario2021Entities())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }
        public static string nombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }
        


        public ActionResult ListarUsuarios()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public ActionResult ListarClientes()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra Compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.compra.Add(Compra);
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
                return View(db.compra.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var CompraEdit = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(CompraEdit);
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

        public ActionResult Edit(compra CompraEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var OldCompra = db.compra.Find(CompraEdit.id);
                    OldCompra.id_cliente = CompraEdit.id_cliente;
                    OldCompra.id_usuario = CompraEdit.id_usuario;
                    OldCompra.fecha = CompraEdit.fecha;
                    OldCompra.total = CompraEdit.total;

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
                    compra Compra = db.compra.Find(id);
                    db.compra.Remove(Compra);
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
        

        public ActionResult Reporte2()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from tabCompra in db.compra 
                            join tabProCompra in db.producto_compra on tabCompra.id equals tabProCompra.id_compra
                            select new Reporte2
                            {
                                fechaCompra = tabCompra.fecha,
                                idUsuario = tabCompra.id_usuario,
                                totalCompra = tabCompra.total,
                                idCliente = tabCompra.id_cliente,
                                idProducto = tabProCompra.id_producto,
                                cantidad = tabProCompra.cantidad
                            };
                return View(query);
                
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "error "+ex);
                return View();
            }

        }
        public ActionResult PdfReporte()
        {
            return new ActionAsPdf("Reporte2") { FileName = "reporte2.pdf" };
        }

    }
}   

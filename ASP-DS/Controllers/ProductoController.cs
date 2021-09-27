using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;
using Rotativa;
using System.IO;
using System.Web.Routing;

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
        public ActionResult UploadCSV(HttpPostedFileBase fileForm)
        {
            try
            {
                string filePath = string.Empty;

                if (fileForm != null)
                {
                    string path = Server.MapPath("~/uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(fileForm.FileName);
                    string extension = Path.GetExtension(fileForm.FileName);
                    fileForm.SaveAs(filePath);
                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {

                            var newProducto = new producto
                            {
                                nombre = row.Split(';')[0],
                                percio_unitario = int.Parse(row.Split(';')[1]),
                                descripcion = row.Split(';')[2],
                                cantidad = int.Parse(row.Split(';')[3]),
                                id_proveedor = int.Parse(row.Split(';')[4])

                            };

                            using (var db = new inventario2021Entities())
                            {

                                db.producto.Add(newProducto);
                                db.SaveChanges();
                            }
                        }


                    }

                }
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }


        }
        public ActionResult PaginadorIndex(int pagina = 1)
        {
            try
            {
                int cantidadRegistros = 5;
                using (var db = new inventario2021Entities())
                {
                    var productos = db.producto.OrderBy(x => x.id).Skip((pagina - 1) * cantidadRegistros).Take(cantidadRegistros).ToList();

                    int totalRegistros = db.producto.Count();
                    var modelo = new ProductoIndex();
                    modelo.productos = productos;
                    modelo.ActualPage = pagina;

                    modelo.total = totalRegistros;
                    modelo.RecordsPage = cantidadRegistros;
                    modelo.valueQueryString = new RouteValueDictionary();

                    return View(modelo);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }

        }
    }
}
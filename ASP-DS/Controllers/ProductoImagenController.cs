using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;
using System.IO;

namespace ASP_DS.Controllers
{
    [Authorize]
    public class ProductoImagenController : Controller
    {
        
        // GET: ProductoImagen
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.ToList());

            }

        }

        public static string nombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }
      

        public ActionResult ListarProductos()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_imagen Producto_I)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_imagen.Add(Producto_I);
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
                return View(db.producto_imagen.Find(id));

                
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var ProductoImagenEdit = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(ProductoImagenEdit);
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

        public ActionResult Edit(producto_imagen Producto_I)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldProductoImagen = db.producto_imagen.Find(Producto_I.id);
                    oldProductoImagen.id_producto = Producto_I.id_producto;
                    oldProductoImagen.imagen = Producto_I.imagen;
                    
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
                    producto_imagen Producto_Imagen = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(Producto_Imagen);
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

        public ActionResult CargarImagen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CargarImagen(int producto, HttpPostedFileBase imagen)
        {
            try
            {
                string filePath = string.Empty;
                string nameFile = "";

                if (imagen != null)
                {
                    string path = Server.MapPath("~/uploads/Imagenes/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    nameFile = Path.GetFileName(imagen.FileName);
                    filePath = path + Path.GetFileName(imagen.FileName);
                    string extension = Path.GetExtension(imagen.FileName);
                    imagen.SaveAs(filePath);
                }

                using (var db = new inventario2021Entities())
                {
                    var imagenProducto = new producto_imagen();
                    imagenProducto.id_producto = producto;
                    imagenProducto.imagen = "/Uploads/Imagenes/" + nameFile;
                    db.producto_imagen.Add(imagenProducto);
                    db.SaveChanges();

                }

                return View();

            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "error " + e);
                return View();

            }
        }

    }
}
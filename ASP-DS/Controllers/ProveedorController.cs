using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;

namespace ASP_DS.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Consulta()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.proveedor.ToList());

            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();

                    return RedirectToAction("Consulta");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error " + e);
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findUser = db.proveedor.Find(id);
                return View(findUser);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findUser = db.proveedor.Find(id);
                    db.proveedor.Remove(findUser);
                    db.SaveChanges();

                    return RedirectToAction("Consulta");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error " + e);
                return View();
            }


        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor findProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProveedor);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error " + e);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult edit(proveedor editProveedor)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor Proveedor = db.proveedor.Find(editProveedor.id);

                    Proveedor.nombre = editProveedor.nombre;
                    Proveedor.direccion = editProveedor.direccion;
                    Proveedor.telefono = editProveedor.telefono;
                    Proveedor.nombre_contacto = editProveedor.nombre_contacto;

                    db.SaveChanges();
                    return RedirectToAction("Consulta");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error " + e);
                return View();
            }
        }


    }


}

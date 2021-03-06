using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;

namespace ASP_DS.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        
        // GET: Rol
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.roles.ToList());

            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(roles rol)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.roles.Add(rol);
                    db.SaveChanges();

                    return RedirectToAction("Index");
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
                var findRol = db.roles.Find(id);
                return View(findRol);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findRol = db.roles.Find(id);
                    db.roles.Remove(findRol);
                    db.SaveChanges();

                    return RedirectToAction("Index");
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
                    roles findRol = db.roles.Where(a => a.id == id).FirstOrDefault();
                    return View(findRol);
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

        public ActionResult edit(roles editRol)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    roles Rol = db.roles.Find(editRol.id);

                    Rol.descripcion = editRol.descripcion;


                    db.SaveChanges();
                    return RedirectToAction("Index");
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
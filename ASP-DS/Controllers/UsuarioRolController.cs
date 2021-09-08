using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;

namespace ASP_DS.Controllers
{
    [Authorize]
    public class UsuarioRolController : Controller
    {
        
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuariorol.ToList());

            }

        }

        public static string nombreUsuario(int idUsuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }
        public static string nombreRol(int idRol)
        {
            using (var db = new inventario2021Entities())
            {
                return db.roles.Find(idRol).descripcion;
            }
        }

        public ActionResult ListarUsuarios()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public ActionResult ListarRol()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.roles.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(usuariorol usuariorol)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.usuariorol.Add(usuariorol);
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
                return View(db.usuariorol.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var UsuarioRolEdit = db.usuariorol.Where(a => a.id == id).FirstOrDefault();
                    return View(UsuarioRolEdit);
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

        public ActionResult Edit(usuariorol UsuarioRolEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldUsuarioRol = db.usuariorol.Find(UsuarioRolEdit.id);
                    oldUsuarioRol.idUsuario = UsuarioRolEdit.idUsuario;
                    oldUsuarioRol.idRol = UsuarioRolEdit.idRol;
                    

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
                    usuariorol Usuariorol = db.usuariorol.Find(id);
                    db.usuariorol.Remove(Usuariorol);
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
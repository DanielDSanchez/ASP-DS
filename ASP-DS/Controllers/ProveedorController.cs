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
        public ActionResult uploadCSV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
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
                            var newProveedor = new proveedor
                            {
                                nombre = row.Split(';')[0],
                                telefono = row.Split(';')[2],
                                direccion = row.Split(';')[1],
                                nombre_contacto = row.Split(';')[3]
                            };
                            using (var db = new inventario2021Entities())
                            {
                                db.proveedor.Add(newProveedor);
                                db.SaveChanges();
                            }
                        }
                        

                    }

                }
                return View();
                
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }

        
        }

    }


}

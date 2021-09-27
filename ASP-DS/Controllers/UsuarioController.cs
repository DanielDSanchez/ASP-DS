using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_DS.Models;
using System.Web.Security;
using System.Text;
using System.IO;
using System.Web.Routing;


namespace ASP_DS.Controllers
{
    
    public class UsuarioController : Controller
    {
        [Authorize]
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuario.ToList());

            }


        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    db.usuario.Add(usuario);
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
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findUser = db.usuario.Find(id);
                return View(findUser);
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findUser = db.usuario.Find(id);
                    db.usuario.Remove(findUser);
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
        [Authorize]
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
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
        [Authorize]
        public ActionResult edit(usuario editUser)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario user = db.usuario.Find(editUser.id);

                    user.nombre = editUser.nombre;
                    user.apellido = editUser.apellido;
                    user.email = editUser.email;
                    user.fecha_nacimiento = editUser.fecha_nacimiento;
                    user.password = editUser.password;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            } catch (Exception e)
            {
                ModelState.AddModelError("", "Error " + e);
                return View();
            }
        }

        public ActionResult Login(string mensaje = "")
        {
            ViewBag.Message = mensaje;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(string user, string password)
        {
            try
            {
                string passEncrip = UsuarioController.HashSHA1(password);
                using (var db = new inventario2021Entities())
                {
                    var userLogin = db.usuario.FirstOrDefault(e => e.email == user && e.password == passEncrip);
                    if (userLogin != null)
                    {
                        FormsAuthentication.SetAuthCookie(userLogin.email, true);
                        Session["user"] = userLogin;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Login("Verifique sus Datos");
                    }
                }
            } catch (Exception e)
            {
                ModelState.AddModelError("", "error " + e);
                return View();
            }

        }
        
        public ActionResult CloseSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UploadCSV()
        {
            return View();
        }
        [HttpPost]
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
                            
                            var newUsuario = new usuario
                            {
                                nombre = row.Split(';')[0],
                                apellido = row.Split(';')[1],
                                fecha_nacimiento = DateTime.Parse(row.Split(';')[2]),
                                email = row.Split(';')[3],
                                password = row.Split(';')[4]
                                
                            };
                            
                            using (var db = new inventario2021Entities())
                            {
                               
                                db.usuario.Add(newUsuario);
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
                    var usuarios = db.usuario.OrderBy(x => x.id).Skip((pagina - 1) * cantidadRegistros).Take(cantidadRegistros).ToList();

                    int totalRegistros = db.usuario.Count();
                    var modelo = new UsuarioIndex();
                    modelo.usuarios = usuarios;
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
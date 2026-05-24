using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpStatusCodesMVC.Controllers
{
    public class StatushttpController : Controller
    {
        
        public ActionResult Informativos()
        {
            return View();
        }
        public ActionResult Exito()
        {
            return View();
        }
        public ActionResult Redireccion()
        {
            return View();
        }
        public ActionResult Errcliente()
        {
            return View();
        }
        public ActionResult Errservidor()
        {
            return View();
        }
    }
}

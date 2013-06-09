using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAcademico.WebApp.Controllers
{
    public class NavegacaoController : Controller
    {
        //
        // GET: /Navegacao/

        public PartialViewResult Menu()
        {
            return PartialView("Menu");
        }
    }
}

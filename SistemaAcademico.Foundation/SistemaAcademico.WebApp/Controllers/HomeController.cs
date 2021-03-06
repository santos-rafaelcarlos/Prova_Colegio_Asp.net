﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAcademico.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ListarAluno()
        {
            return RedirectToAction("Index","Aluno");
        }

        [Authorize]
        public ActionResult ListarProfessor()
        {
            return RedirectToAction("Index", "Professor");
        }
       
        [Authorize]
        public ActionResult ListarTurma()
        {
            return RedirectToAction("Index", "Turma");
        }

        [Authorize]
        public ActionResult ListarMidias()
        {
            return RedirectToAction("Index", "Midia");        
        }
    }
}

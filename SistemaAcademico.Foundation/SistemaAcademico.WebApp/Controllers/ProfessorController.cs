using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaAcademico.Foundation;
using Presentation.WebApp.Models.Comum;

namespace SistemaAcademico.WebApp.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        IRepositorio<Professor> repositorio = null;
        public ProfessorController(IRepositorio<Professor> _repositorio)
        {
            repositorio = _repositorio;
        }

        //
        // GET: /Professor/
        public int TamanhoDaPagina = 20;

        public PartialViewResult Index(int pagina = 1)
        {
            IEnumerable<Professor> Professors = repositorio.RetornaTodos()
              .OrderBy(a => a.Nome)
              .Skip((pagina - 1) * TamanhoDaPagina)
              .Take(TamanhoDaPagina);

            ListaItemViewModel<Professor> ProfessorViewModel = new ListaItemViewModel<Professor>()
            {
                Items = Professors,
                InformacaoDePaginacao = new InformacaoDePaginacao()
                {
                    ItensPorPagina = TamanhoDaPagina,
                    PaginaAtual = pagina,
                    TotalDeItems = repositorio.RetornaTodos().Count(),
                },
            };
            return PartialView("Index", ProfessorViewModel);
        }

        [HttpGet]
        public PartialViewResult Criar()
        {
            CarregaEstados();

            return PartialView("Criar");
        }

        private void CarregaEstados(Estado uf = Estado.Nenhum)
        {
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(Estado)),uf);
        }


        [HttpPost]
        public ActionResult Criar(Professor Professor,FormCollection form)
        {
            Professor.Endereco.UF = RecuperaEstado(form["Estados"]);
            repositorio.Salvar(Professor);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Editar(Guid ID)
        {
            Professor Professor=repositorio.RetornaPorID(ID);
            CarregaEstados(Professor.Endereco.UF);
            return PartialView("Editar",Professor);
        }

        [HttpPost]
        public ActionResult Editar(Professor Professor, FormCollection form)
        {
            Professor.Endereco.UF = RecuperaEstado(form["Estados"]);
            repositorio.Salvar(Professor);

            return RedirectToAction("Index");
        }

        private static Estado RecuperaEstado(string estado)
        {
            Estado uf = Estado.Nenhum;

            Estado valor = 0;
            if (Enum.TryParse<Estado>(estado,false, out valor))
                uf = valor;
            
            return uf;
        }

        [HttpGet]
        public PartialViewResult Delete(Guid Id)
        {
            return PartialView("Delete", repositorio.RetornaPorID(Id));
        }

        [HttpPost]
        public ActionResult Delete(Professor Professor)
        {
            repositorio.Remover(Professor);

            return RedirectToAction("Index");
        }


        public PartialViewResult Detalhes(Guid Id)
        {
            Professor Professor = repositorio.RetornaPorID(Id);

            return PartialView("Detalhes", Professor);
        }

        
    }
}

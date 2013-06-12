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
    public class AlunoController : Controller
    {
        IRepositorio<Aluno> repositorio = null;
        public AlunoController(IRepositorio<Aluno> _repositorio)
        {
            repositorio = _repositorio;
        }

        //
        // GET: /Aluno/
        public int TamanhoDaPagina = 20;

        public PartialViewResult Index(int pagina = 1)
        {
            IEnumerable<Aluno> alunos = repositorio.RetornaTodos()
              .OrderBy(a => a.Nome)
              .Skip((pagina - 1) * TamanhoDaPagina)
              .Take(TamanhoDaPagina);

            ListaItemViewModel<Aluno> alunoViewModel = new ListaItemViewModel<Aluno>()
            {
                Items = alunos,
                InformacaoDePaginacao = new InformacaoDePaginacao()
                {
                    ItensPorPagina = TamanhoDaPagina,
                    PaginaAtual = pagina,
                    TotalDeItems = repositorio.RetornaTodos().Count(),
                },
            };
            return PartialView("Index", alunoViewModel);
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
        public ActionResult Criar(Aluno aluno,FormCollection form)
        {
            aluno.Endereco.UF = RecuperaEstado(form["Estados"]);
            repositorio.Salvar(aluno);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Editar(Guid ID)
        {
            Aluno aluno=repositorio.RetornaPorID(ID);
            CarregaEstados(aluno.Endereco.UF);
            return PartialView("Editar",aluno);
        }

        [HttpPost]
        public ActionResult Editar(Aluno aluno, FormCollection form)
        {
            aluno.Endereco.UF = RecuperaEstado(form["Estados"]);
            repositorio.Salvar(aluno);

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
        public ActionResult Delete(Aluno aluno)
        {
            repositorio.Remover(aluno);

            return RedirectToAction("Index");
        }


        public PartialViewResult Detalhes(Guid Id)
        {
            Aluno aluno = repositorio.RetornaPorID(Id);

            return PartialView("Detalhes", aluno);
        }

        
    }
}

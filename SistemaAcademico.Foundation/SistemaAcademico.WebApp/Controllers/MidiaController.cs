using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Presentation.WebApp.Models.Comum;
using SistemaAcademico.Foundation;
using SistemaAcademico.WebApp.Models;

namespace SistemaAcademico.WebApp.Controllers
{
    [Authorize]
    public class MidiaController : Controller
    {
        //
        // GET: /Midia/

        IRepositorio<Midia> repositorioMidia = null;
        IRepositorio<Pessoa> repositorioPessoa = null;        
        public MidiaController(IRepositorio<Midia> _repositorioMidia,
            IRepositorio<Pessoa> _repositorioPessoa)
        {
            repositorioMidia = _repositorioMidia;            
            repositorioPessoa = _repositorioPessoa;

        }

        public int TamanhoDaPagina = 20;

        public PartialViewResult Index(int pagina = 1)
        {
            IEnumerable<Midia> midias = repositorioMidia.RetornaTodos()
              .OrderBy(a => a.Titulo)
              .Skip((pagina - 1) * TamanhoDaPagina)
              .Take(TamanhoDaPagina);

            ListaItemViewModel<Midia> midiaViewModel = new ListaItemViewModel<Midia>()
            {
                Items = midias,
                InformacaoDePaginacao = new InformacaoDePaginacao()
                {
                    ItensPorPagina = TamanhoDaPagina,
                    PaginaAtual = pagina,
                    TotalDeItems = repositorioMidia.RetornaTodos().Count(),
                },
            };
            return PartialView("Index", midiaViewModel);
        }

        [HttpGet]
        public PartialViewResult Criar()
        {            
            return PartialView();
        }

        [HttpPost]
        public ActionResult Criar(Midia model)
        {
            model.NumerodeCopiasDisponíveis = model.NumerodeCopias;
            repositorioMidia.Salvar(model);

            return RedirectToAction("Index");
        }

        public PartialViewResult Detalhes(Guid id)
        {
            Midia midia = repositorioMidia.RetornaPorID(id);
            
            return PartialView(midia);
        }


        [HttpGet]
        public PartialViewResult Editar(Guid id)
        {
            Midia midia = repositorioMidia.RetornaPorID(id);
            
            return PartialView(midia);
        }

        [HttpPost]
        public ActionResult Editar(Midia model)
        {
            repositorioMidia.Salvar(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Delete(Guid id)
        {
            Midia midia = repositorioMidia.RetornaPorID(id);
            return PartialView(midia);
        }

        [HttpPost]
        public ActionResult Delete(Midia model)
        {
            repositorioMidia.Remover(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Emprestar(Guid id)
        {
            Midia midia = repositorioMidia.RetornaPorID(id);
            Session["Midia"] = midia;
            CarregaPessoas();
            return PartialView(midia);
        }

        [HttpPost]
        public ActionResult Emprestar(FormCollection form)
        {
            Guid pessoaID = Guid.Empty;
            Pessoa pessoa = null;

            if (Guid.TryParse(form["Pessoas"], out pessoaID))
                pessoa = repositorioPessoa.RetornaPorID(pessoaID);

            Midia midia = (Midia)Session["Midia"];

            if (pessoa != null)
            {
                midia.EmprestarMidia(pessoa);

                repositorioMidia.Salvar(midia);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Devolver(Guid Id)
        {
            Midia midia = repositorioMidia.RetornaPorID(Id);
            Session["Midia"] = midia;
            return PartialView(midia);
        }

        [HttpPost]
        public ActionResult Devolver(MidiaEmprestada midiaEmprestada)
        {
            Midia midia = (Midia)Session["Midia"];

            midia.DevolverMidia(midiaEmprestada.ID);

            repositorioMidia.Salvar(midia);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult ConfirmarDevolucao(Guid Id)
        {
            Midia midia = (Midia)Session["Midia"];
            MidiaEmprestada midiaEmprestada=  midia.Emprestimos.Where(m => m.ID == Id).FirstOrDefault();

            return PartialView(midiaEmprestada);
        }

        [HttpPost]
        public ActionResult ConfirmarDevolucao(MidiaEmprestada midiaEmprestada)
        {
            Midia midia = (Midia)Session["Midia"];

            midia.DevolverMidia(midiaEmprestada.ID);

            repositorioMidia.Salvar(midia);

            return RedirectToAction("Index");
        }
                
        public PartialViewResult RelatorioEmprestimo()
        {
            var midias = from m in repositorioMidia.RetornaTodos()
                         from mm in m.Emprestimos.AsQueryable()
                         where mm.Status == MidiaStatus.Emprestado
                         select new RelatorioEmprestimoViewModel()
                         {
                             Titulo = m.Titulo,
                             Autor = m.Autor,
                             DataEmprestimo = mm.DataEmprestimo,                            
                             Status = mm.Status,
                             Pessoa = mm.Pessoa.Nome,
                         };
            return PartialView(midias);
        }


        private void CarregaPessoas(Pessoa p = null)
        {
            ViewBag.Pessoas = new SelectList(repositorioPessoa.RetornaTodos(), "ID", "Nome", p);
        }
    }
}

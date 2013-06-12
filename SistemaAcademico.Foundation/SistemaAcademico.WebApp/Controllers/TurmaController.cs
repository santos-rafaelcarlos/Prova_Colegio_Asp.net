using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaAcademico.Foundation;
using Presentation.WebApp.Models.Comum;
using SistemaAcademico.WebApp.Models;

namespace SistemaAcademico.WebApp.Controllers
{
    [Authorize]
    public class TurmaController : Controller
    {
        //
        // GET: /Turma/
        
        IRepositorio<Turma> repositorioTurma = null;
        IRepositorio<Professor> repositorioProfessor = null;
        IRepositorio<Aluno> repositorioAluno = null;
        
        public TurmaController(IRepositorio<Turma> _repositorioTurma,
            IRepositorio<Professor> _repositorioProfessor, IRepositorio<Aluno> _repositorioAluno)
        {
            repositorioTurma = _repositorioTurma;
            repositorioProfessor = _repositorioProfessor;
            repositorioAluno = _repositorioAluno;
            
        }

        public int TamanhoDaPagina = 20;

        public PartialViewResult Index(int pagina = 1)
        {
            IEnumerable<Turma> turmas = repositorioTurma.RetornaTodos()
              .OrderBy(a => a.Codigo)
              .Skip((pagina - 1) * TamanhoDaPagina)
              .Take(TamanhoDaPagina);

            ListaItemViewModel<Turma> turmaViewModel = new ListaItemViewModel<Turma>()
            {
                Items = turmas,
                InformacaoDePaginacao = new InformacaoDePaginacao()
                {
                    ItensPorPagina = TamanhoDaPagina,
                    PaginaAtual = pagina,
                    TotalDeItems = repositorioTurma.RetornaTodos().Count(),
                },
            };
            return PartialView("Index", turmaViewModel);
        }

        [HttpGet]
        public PartialViewResult Criar()
        {
            CarregaProfessores();
            return PartialView();
        }
                
        [HttpPost]
        public ActionResult Criar(Turma model,FormCollection form)
        {
            Guid profId = Guid.Empty;

            if (Guid.TryParse(form["Professores"], out profId))
                model.Professor = repositorioProfessor.RetornaPorID(profId);
            model.Professor.Status = ProfessorStatus.Alocado;

            repositorioTurma.Salvar(model);

            return RedirectToAction("Index");
        }


        public PartialViewResult Detalhes(Guid id)
        {
            Turma turma = repositorioTurma.RetornaPorID(id);

            return PartialView(turma);
        }

        [HttpGet]
        public PartialViewResult Editar(Guid id)
        {            
            Turma turma = repositorioTurma.RetornaPorID(id);
            CarregaProfessores(turma.Professor);
            return PartialView(turma);
        }

        [HttpPost]
        public ActionResult Editar(Turma model, FormCollection form)
        {
            Guid profId = Guid.Empty;

            if (Guid.TryParse(form["Professores"], out profId) && model.Professor.ID != profId)
                model.Professor = repositorioProfessor.RetornaPorID(profId);

            repositorioTurma.Salvar(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Delete(Guid id)
        {
            Turma turma = repositorioTurma.RetornaPorID(id);
            return PartialView(turma);
        }

        [HttpPost]
        public ActionResult Delete(Turma model)
        {
            repositorioTurma.Remover(model);

            return RedirectToAction("Index");
        }
         
        [HttpGet]
        public PartialViewResult Matricular(Guid id)
        {
            Turma turma = repositorioTurma.RetornaPorID(id);
            
            ViewBag.Alunos = repositorioAluno.RetornaTodos()
                .Where(a => !turma.Alunos.Contains(a));

            return PartialView(turma);
        }

        [HttpPost]
        public ActionResult Matricular(Turma model)
        {
            repositorioTurma.Salvar(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Incluir(Guid alunoId, Guid turmaId)
        {
            Aluno aluno = repositorioAluno.RetornaPorID(alunoId);
            Turma turma = repositorioTurma.RetornaPorID(turmaId);
                        
            Session["turma"] = turma;
            Session["Aluno"] = aluno;
            return PartialView(aluno);
        }

        [HttpPost]
        public ActionResult Incluir()
        {
            Turma turma = (Turma)Session["turma"];
            Aluno aluno = (Aluno)Session["Aluno"];

            turma.Alunos.Add(aluno);
            aluno.Status = AlunoStatus.Matriculado;

            repositorioTurma.Salvar(turma);
            repositorioAluno.Salvar(aluno);

            return RedirectToAction("Matricular", new {Id = turma.ID });
        }

        [HttpGet]
        public PartialViewResult Remover(Guid alunoId, Guid turmaId)
        {
            Aluno aluno = repositorioAluno.RetornaPorID(alunoId);
            Turma turma = repositorioTurma.RetornaPorID(turmaId);

            Session["turma"] = turma;
            Session["Aluno"] = aluno;
            return PartialView(aluno);
        }

        [HttpPost]
        public ActionResult Remover()
        {
            Turma turma = (Turma)Session["turma"];
            Aluno aluno = (Aluno)Session["Aluno"];
           
            if (turma.Alunos.Contains(aluno))
                turma.Alunos.Remove(aluno);
            aluno.Status = AlunoStatus.Cadastrado;

            repositorioTurma.Salvar(turma);
            repositorioAluno.Salvar(aluno);

            return RedirectToAction("Matricular", new { Id = turma.ID });
        }


        private void CarregaProfessores(Professor prof = null)
        {
            ViewBag.Professores = new SelectList(repositorioProfessor.RetornaTodos(),"ID","Nome", prof);
        }

    }
}

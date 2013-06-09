using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaAcademico.Foundation;
using Presentation.WebApp.Models.Comum;

namespace SistemaAcademico.WebApp.Controllers
{
    public class TurmaController : Controller
    {
        //
        // GET: /Turma/
        
        IRepositorio<Turma> repositorioTurma = null;
        IRepositorio<Professor> repositorioProfessor = null;
        IRepositorio<Aluno> repositorioAluno = null;
        public TurmaController(IRepositorio<Turma> _repositorioTurma,
            IRepositorio<Professor> _repositorioProfessor,
            IRepositorio<Aluno> _repositorioAluno)
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

    }
}

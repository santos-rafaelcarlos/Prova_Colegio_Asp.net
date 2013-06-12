using System;
using SistemaAcademico.Foundation;

namespace SistemaAcademico.WebApp.Models
{
    public class RelatorioEmprestimoViewModel
    {
        public String Titulo { get; set; }
        public string Autor { get; set; }
        public String Pessoa { get; set; }
        public DateTime DataEmprestimo { get; set; }        
        public MidiaStatus Status { get; set; }
    }
}
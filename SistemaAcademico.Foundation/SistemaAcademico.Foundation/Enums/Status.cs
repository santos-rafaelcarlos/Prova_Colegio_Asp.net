using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Foundation
{
    public enum AlunoStatus
    {
        Cadastrado = 0,
        Matriculado = 1,
    }

    public enum ProfessorStatus
    {
        Disponivel = 0,
        Alocado = 1,
    }

    public enum TurmaStatus
    {
        Aberta = 0,
        EmAndamento = 1,
        Encerrada = 2,
    }
}


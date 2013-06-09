using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CamadaAcessoDados.Repositorio;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.Teste
{
    [TestClass]
    public class Teste
    {
        [TestMethod]
        public void CriarEntityFramework()
        {
            Repositorio<Aluno> rep = new Repositorio<Aluno>();

            Aluno al = rep.RetornaPorID(new Guid("f229c003-0a68-49d6-89c8-a1d70152824b"));

            rep.Remover(al);

           // Aluno aluno = new Aluno();
           //// aluno.ID = Guid.NewGuid();
           // aluno.Nome = "Rafael Santos";
           // aluno.Status = AlunoStatus.Cadastrado;
           // aluno.DataNascimento = new DateTime(1986, 04, 23);
           // aluno.Endereco = new Endereco()
           // {
           // //    ID = Guid.NewGuid(),
           //     Logradouro = "Belmiro Vivas",
           //     TipoLogradouro = "Rua",
           //     Numero = 168,
           //     UF = Estado.RJ,
           //     Cidade = "Guapimirim",
           //     Bairro = "Centro",
           //     CEP = 25940000,
           //     Complemento = string.Empty,
           // };
           // aluno.Telefone = new Telefone()
           // {
           //  ///   TitularID = aluno.ID,
           //     Numero = 87942615,
           //     DDD = 21,
           // };

           // rep.Salvar(aluno);

            Aluno[] array = rep.RetornaTodos();

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaAcademico.Foundation
{
    public class Midia:IItem
    {
        public Midia()
        {
            this.Emprestimos = new List<MidiaEmprestada>();
        }
        public virtual Guid ID { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Autor { get; set; }
        public virtual int NumerodeCopias { get; set; }
        public virtual int NumerodeCopiasEmprestadas { get; set; }
        public virtual int NumerodeCopiasDisponíveis { get; set; }
        
        public virtual IList<MidiaEmprestada> Emprestimos { get; set; }

        public virtual bool EmprestarMidia(Pessoa pessoa)
        {
            bool retVal = false;
            if (this.NumerodeCopiasDisponíveis > 0
                && !VerificaSeAlunoJaTemUmaCopia(pessoa))
            {
                MidiaEmprestada midiaEmprestada = new MidiaEmprestada()
                {
                    Pessoa = pessoa,
                    DataEmprestimo = DateTime.Now,
                    Status = MidiaStatus.Emprestado,
                };

                this.Emprestimos.Add(midiaEmprestada);
                this.NumerodeCopiasEmprestadas++;
                this.NumerodeCopiasDisponíveis--;
                retVal = true;
            }
            return retVal;
        }

        private bool VerificaSeAlunoJaTemUmaCopia(Pessoa pessoa)
        {
            return this.Emprestimos.Where(m => m.Pessoa.ID == pessoa.ID).Count() > 0;
        }

        public virtual void DevolverMidia(Guid midiaEmprestadaID)
        {
            MidiaEmprestada midia = this.Emprestimos.Where(e => e.ID == midiaEmprestadaID).FirstOrDefault();

            if (midia != null)
            {
                midia.Status = MidiaStatus.Devolvido;
                midia.DataEmprestimo = DateTime.Now;
                this.NumerodeCopiasEmprestadas--;
                this.NumerodeCopiasDisponíveis++;
            }
        }

        public override bool Equals(object obj)
        {
            return this.ID.Equals(((Midia)obj).ID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class MidiaEmprestada
    {
        public virtual Guid ID { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual DateTime DataEmprestimo { get; set; }
        public virtual DateTime? DataDevolucao { get; set; }
        public virtual MidiaStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            return this.ID.Equals(((MidiaEmprestada)obj).ID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum MidiaStatus
    {
        Emprestado = 0,
        Devolvido = 1,
    }
}
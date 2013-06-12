using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SistemaAcademico.Foundation;

namespace CamadaAcessoDados.EntityFramework
{
    public class ColegioContext:DbContext
    {
        public ColegioContext(string connectionString)
            : base(connectionString)
        {
            
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
               .HasRequired(a => a.Telefone)
               .WithOptional()
               .Map(a => a.ToTable("Telefone")).WillCascadeOnDelete(true);

            modelBuilder.Entity<Pessoa>()
                .HasRequired(p => p.Endereco)
                .WithOptional()
                .Map(e => e.ToTable("Endereco")).WillCascadeOnDelete(true);


            modelBuilder.Entity<Pessoa>()
                .HasKey(a => a.ID)
                .ToTable("Pessoa");
            
            modelBuilder.Entity<Endereco>()
               .HasKey(a => a.ID)
               .ToTable("Endereco");

            modelBuilder.Entity<Telefone>()
               .HasKey(a => a.TitularID)
               .ToTable("Telefone");
                       
            modelBuilder.Entity<Aluno>()
                .HasKey(a => a.ID)
                .ToTable("Aluno"); 
                      
            modelBuilder.Entity<Professor>()
                .HasKey(a => a.ID)
                .ToTable("Professor");

            modelBuilder.Entity<Telefone>()
                .HasKey(a => a.TitularID)
                .ToTable("Telefone");

            modelBuilder.Entity<Midia>()
                .HasKey(m => m.ID).ToTable("Midia");
            modelBuilder.Entity<Midia>().HasMany(m => m.Emprestimos).WithOptional();
                

            base.OnModelCreating(modelBuilder);
        }
    }
}

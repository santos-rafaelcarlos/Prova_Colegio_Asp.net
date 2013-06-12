using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using SistemaAcademico.Foundation;
using CamadaAcessoDados.Repositorio;

namespace SistemaAcademico.WebApp
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<IRepositorio<Aluno>, Repositorio<Aluno>>(new ContainerControlledLifetimeManager());
        container.RegisterType<IRepositorio<Professor>, Repositorio<Professor>>(new ContainerControlledLifetimeManager());
        container.RegisterType<IRepositorio<Turma>, Repositorio<Turma>>(new ContainerControlledLifetimeManager());
        container.RegisterType<IRepositorio<Pessoa>, Repositorio<Pessoa>>(new ContainerControlledLifetimeManager());
        container.RegisterType<IRepositorio<Midia>, Repositorio<Midia>>(new ContainerControlledLifetimeManager()); 
    }
  }
}
using Microsoft.Practices.Unity;
using Navigation.Interfaces;
using Navigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Services
{
    public class ServiceLocator
    {
        private readonly IUnityContainer Unity;
        private static readonly ServiceLocator _instance = new ServiceLocator();

        public static ServiceLocator Instance
        {
            get { return _instance; }
        }

        protected ServiceLocator()
        {
            Unity = new UnityContainer();

            //Services
            RegisterSingleton<INavigationService, NavigationService>();

            //ViewModels
            Unity.RegisterType<WelcomeViewModel>();
            Unity.RegisterType<MainViewModel>();
        }


        public T Resolve<T>()
        {
            return Unity.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return Unity.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            Unity.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            Unity.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            Unity.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}

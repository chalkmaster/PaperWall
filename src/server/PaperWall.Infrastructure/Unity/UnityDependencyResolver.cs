using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PaperWall.Core.Infrastructure;
using PaperWall.Core.Repository;
using PaperWall.Repository.InMemory;
using PaperWall.Repository.MySQL;

namespace PaperWall.Infrastructure.Unity
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion

        #region Constructors

        public UnityDependencyResolver()
            : this(new UnityContainer())
        {
            //TODO: Initialize Container
            //var configSection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //configSection.Containers.Default.Configure(_container);

            //_container.RegisterInstance(typeof(IMessageRepository), new InMemoryMessageRepository());
            _container.RegisterInstance(typeof(IMessageRepository), new MySQLMessageRepository());
        }

        public UnityDependencyResolver(IUnityContainer unityContainer)
        {
            _container = unityContainer;
        }

        #endregion

        #region IDependencyResolver Members

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return (T)_container.Resolve(type);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
            }
        }

        #endregion
    }
}

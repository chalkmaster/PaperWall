using System;

namespace PaperWall.Core.Infrastructure
{
    /// <summary>
    /// Resolve dependências
    /// </summary>
    public static class IoC
    {
        #region Fields

        private static IDependencyResolver _resolver;

        #endregion

        #region Methods

        /// <summary>
        /// Inicializa a inversão de controle.
        /// </summary>
        /// <param name="dependencyResolver">Classe concreta a ser utilizada para resolver as dependências</param>
        public static void Initialize(IDependencyResolver dependencyResolver)
        {
            Initialize(() => dependencyResolver);
        }

        /// <summary>
        /// Inicializa a inversão de controle de forma que o container não será criado caso 
        /// já tenha sido inicializado anteriormente, economizando o tempo do load e parse 
        /// da sessão do Unity no arquivo de configuração. Este método foi criado para
        /// atender uma necessidade no ClientIntegrator do OSMobile V2.
        /// </summary>
        /// <param name="dependencyResolverFactory">Função para a criação da classe concreta a ser utilizada para resolver as dependências</param>
        public static void Initialize(Func<IDependencyResolver> dependencyResolverFactory)
        {
            _resolver = _resolver ?? dependencyResolverFactory();
        }

        /// <summary>
        /// Resolve uma dependência.
        /// </summary>
        /// <typeparam name="T">Tipo para resolver</typeparam>
        /// <returns>Objeto concreto.</returns>
        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        /// <summary>
        /// Resolve uma dependência.
        /// </summary>
        /// <typeparam name="T">Tipo a ser retornado.</typeparam>
        /// <param name="type">Tipo para resolver.</param>
        /// <returns>Objeto concreto.</returns>
        public static T Resolve<T>(Type type)
        {
            return _resolver.Resolve<T>(type);
        }

        /// <summary>
        /// Resolve uma dependência.
        /// </summary>
        /// <param name="type">Tipo a ser resolvido</param>
        /// <returns>Objeto concreto</returns>
        public static object Resolve(Type type)
        {
            return Resolve<object>(type);
        }

        #endregion
    }
}

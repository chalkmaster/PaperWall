using System;

namespace PaperWall.Core.Infrastructure
{
    /// <summary>
    /// Responsável por resolver as dependencias.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// Resolve uma dependência.
        /// </summary>
        /// <typeparam name="T">Tipo para resolver.</typeparam>
        /// <returns>Objeto concreto.</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve uma dependência.
        /// </summary>
        /// <typeparam name="T">Tipo a ser retornado.</typeparam>
        /// <param name="type">Tipo para resolver.</param>
        /// <returns>Objeto concreto.</returns>
        T Resolve<T>(Type type);
    }
}

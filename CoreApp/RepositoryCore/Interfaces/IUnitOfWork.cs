using Domain.Models;

namespace RepositoryCore.Interfaces
{
    /// <summary>
    /// Classe de unidade de trabalho
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repositório de cliente
        /// </summary>
        IMongoRepository<Cliente> ClienteRepository { get; }
    }
}

using Domain.Models;
using RepositoryCore.Interfaces;

namespace RepositoryCore.UnitOfWork
{
    /// <inheritdoc />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="clienteRepository"></param>
        public UnitOfWork(IMongoRepository<Cliente> clienteRepository)
        {
            ClienteRepository = clienteRepository;
        }

        /// <inheritdoc />
        public IMongoRepository<Cliente> ClienteRepository { get; }

    }
}

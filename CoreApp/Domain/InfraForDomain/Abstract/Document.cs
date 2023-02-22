using Domain.InfraForDomain.Interface;
using MongoDB.Bson;

namespace Domain.InfraForDomain.Abstract
{
    /// <inheritdoc />
    public abstract class Document : IDocument
    {
        /// <inheritdoc />
        public ObjectId Id { get; set; }

        /// <inheritdoc />
        public DateTime DataHoraCriacao => Id.CreationTime;
    }
}

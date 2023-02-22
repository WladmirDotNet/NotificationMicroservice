using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.InfraForDomain.Interface
{
    /// <summary>
    /// Documento base para criação de classes de domínio
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Id de documento (padrão para MongoDB)
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        /// <summary>
        /// Data e hora da criação do documento
        /// </summary>
        DateTime DataHoraCriacao { get; }
    }
}

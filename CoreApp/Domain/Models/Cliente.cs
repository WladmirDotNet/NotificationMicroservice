using Domain.InfraForDomain.Abstract;
using Domain.InfraForDomain.Attribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    /// <summary>
    /// Classe DTO para cliente
    /// </summary>
    [BsonCollection("Cliente")]
    public class Cliente : Document
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        [BsonElement("Nome")]
        public string Nome { get; set; }
        
        /// <summary>
        /// Telefone do cliente
        /// </summary>
        public string Telefone { get; set; }
        
        /// <summary>
        /// Email do cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Código do usuário da inclusão
        /// </summary>
        public int CodUsuarioInclusao { get; set; }

    }
}

namespace Domain.InfraForDomain.Attribute
{
    /// <summary>
    /// Classe de atributo que define o nome da coleção utilizada
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : System.Attribute
    {
        /// <summary>
        /// Nome da coleção utilizada
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        /// Attriburo de nome de coleção
        /// </summary>
        /// <param name="collectionName"></param>
        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}

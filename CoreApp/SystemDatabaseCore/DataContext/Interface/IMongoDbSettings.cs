namespace SystemDatabaseCore.DataContext.Interface
{
    /// <summary>
    /// Classe de configuração do MongoDB
    /// </summary>
    public interface IMongoDbSettings
    {
        /// <summary>
        /// Nome do banco de dados MongoDB
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// String de conexão ao MongoDB
        /// </summary>
        public string ConnectionString { get; set; }
    }
}

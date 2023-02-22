using SystemDatabaseCore.DataContext.Interface;

namespace SystemDatabaseCore.DataContext.Service
{
    /// <inheritdoc />
    public class MongoDbSettings : IMongoDbSettings
    {
        /// <inheritdoc />
        public string DatabaseName { get; set; }

        /// <inheritdoc />
        public string ConnectionString { get; set; }
    }
}

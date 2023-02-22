using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using SystemDatabaseCore.DataContext.Interface;
using Domain.InfraForDomain.Interface;
using Domain.InfraForDomain.Attribute;
using RepositoryCore.Interfaces;

namespace RepositoryCore.RepositoryAccsess
{
    /// <inheritdoc />
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="settings"></param>
        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        /// <summary>
        /// Recupera coleção pelo nome
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        private protected string GetCollectionName(Type documentType)
        {
            var custonAttr = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true);

            if (custonAttr == null)
                throw new NullReferenceException("GetCollectionName:GetCustomAttributes");

            var custonAttrItem = custonAttr.FirstOrDefault();

            if (custonAttrItem == null)
                throw new NullReferenceException("GetCollectionName:CustomAttributeItem");

            var bsonCollectionAttribute = (BsonCollectionAttribute)custonAttrItem;

            if (bsonCollectionAttribute == null)
                throw new NullReferenceException("GetCollectionName:BsonCollectionAttribute");

            var collectionName = bsonCollectionAttribute.CollectionName;

            return collectionName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        /// <inheritdoc />
        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        /// <inheritdoc />
        public virtual TDocument FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }


        /// <inheritdoc />
        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        /// <inheritdoc />
        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        /// <inheritdoc />
        public void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }


        /// <inheritdoc />
        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        /// <inheritdoc />
        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        /// <inheritdoc />
        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        /// <inheritdoc />
        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        /// <inheritdoc />
        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        /// <inheritdoc />
        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        /// <inheritdoc />
        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        /// <inheritdoc />
        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        /// <inheritdoc />
        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}

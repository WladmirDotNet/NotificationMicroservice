using Domain.InfraForDomain.Interface;
using System.Linq.Expressions;

namespace RepositoryCore.Interfaces
{
    /// <summary>
    /// Repositório genérico para MongoDB
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Método que retorna um objeto como Queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TDocument> AsQueryable();

        /// <summary>
        /// Método de filtro
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Método de filtro
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="projectionExpression"></param>
        /// <typeparam name="TProjected"></typeparam>
        /// <returns></returns>
        IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);

        /// <summary>
        /// Método de busca para apenas um documento
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Método de busca para apenas um documento (Tipo Async)
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Busca documento por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TDocument FindById(string id);

        /// <summary>
        /// Busca documento por Id (Tipo Async)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDocument> FindByIdAsync(string id);

        /// <summary>
        /// Adiciona um documento
        /// </summary>
        /// <param name="document"></param>
        void InsertOne(TDocument document);

        /// <summary>
        /// Adiciona um documento (Tipo Async)
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task InsertOneAsync(TDocument document);

        /// <summary>
        /// Adiciona documentos
        /// </summary>
        /// <param name="documents"></param>
        void InsertMany(ICollection<TDocument> documents);

        /// <summary>
        /// Adiciona documentos (Tipo Async)
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        Task InsertManyAsync(ICollection<TDocument> documents);

        /// <summary>
        /// Atualiza um documento
        /// </summary>
        /// <param name="document"></param>
        void ReplaceOne(TDocument document);

        /// <summary>
        /// Atualiza um documento (Tipo Async)
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task ReplaceOneAsync(TDocument document);

        /// <summary>
        /// Deleta um documento 
        /// </summary>
        /// <param name="filterExpression"></param>
        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Deleta um documento (Tipo Async)
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Deleta um documento por id
        /// </summary>
        /// <param name="id"></param>
        void DeleteById(string id);

        /// <summary>
        /// Deleta um documento por Id (Tipo Async)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);

        /// <summary>
        /// Deleta documentos
        /// </summary>
        /// <param name="filterExpression"></param>
        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Deleta documentos (Tipo Async)
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}

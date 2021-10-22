using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using Marten.Events;
using Marten.Internal.Operations;
using Marten.Linq;
using Marten.Services;
using Marten.Services.BatchQuerying;
using Marten.Storage;
using Marten.Storage.Metadata;
using Npgsql;

namespace Milou.Deployer.Web.Tests.Integration
{
    public sealed class TestDocumentSession : IDocumentSession
    {
        public void Dispose()
        {
            // in-memory
        }

        public Task<T> LoadAsync<T>(string id, CancellationToken token = new()) => Task.FromResult(default(T)!);

        public Task<T> LoadAsync<T>(int id, CancellationToken token = new()) => throw new NotSupportedException();

        public Task<T> LoadAsync<T>(long id, CancellationToken token = new()) => throw new NotSupportedException();

        public Task<T> LoadAsync<T>(Guid id, CancellationToken token = new()) => throw new NotSupportedException();

        public T Load<T>(string id) => throw new NotSupportedException();

        public T Load<T>(int id) => throw new NotSupportedException();

        public T Load<T>(long id) => throw new NotSupportedException();

        public T Load<T>(Guid id) => throw new NotSupportedException();

        public IMartenQueryable<T> Query<T>() => throw new NotSupportedException();

        public TOut Query<TDoc, TOut>(ICompiledQuery<TDoc, TOut> query) => throw new NotSupportedException();

        public IReadOnlyList<T> Query<T>(string sql, params object[] parameters) => throw new NotSupportedException();
        public Task<int> StreamJson<T>(Stream destination, CancellationToken token, string sql, params object[] parameters) => throw new NotImplementedException();

        public Task<int> StreamJson<T>(Stream destination, string sql, params object[] parameters) => throw new NotImplementedException();

        public Task<IReadOnlyList<T>> QueryAsync<T>(string sql, params object[] parameters) => throw new NotImplementedException();

        public IBatchedQuery CreateBatchQuery() => throw new NotSupportedException();

        public Task<IReadOnlyList<T>> QueryAsync<T>(string sql,
            CancellationToken token = new(),
            params object[] parameters) => throw new NotSupportedException();

        public Task<TOut> QueryAsync<TDoc, TOut>(ICompiledQuery<TDoc, TOut> query, CancellationToken token = new()) =>
            throw new NotSupportedException();

        public Task<bool> StreamJsonOne<TDoc, TOut>(ICompiledQuery<TDoc, TOut> query,
            Stream destination,
            CancellationToken token = new CancellationToken()) =>
            throw new NotImplementedException();

        public Task<int> StreamJsonMany<TDoc, TOut>(ICompiledQuery<TDoc, TOut> query,
            Stream destination,
            CancellationToken token = new CancellationToken()) =>
            throw new NotImplementedException();

        public Task<string?> ToJsonOne<TDoc, TOut>(ICompiledQuery<TDoc, TOut> query, CancellationToken token = new CancellationToken()) => throw new NotImplementedException();

        public Task<string> ToJsonMany<TDoc, TOut>(ICompiledQuery<TDoc, TOut> query, CancellationToken token = new CancellationToken()) => throw new NotImplementedException();

        public IReadOnlyList<T> LoadMany<T>(params string[] ids) => throw new NotSupportedException();
        public IReadOnlyList<T> LoadMany<T>(IEnumerable<string> ids) => throw new NotSupportedException();

        public IReadOnlyList<T> LoadMany<T>(params Guid[] ids) => throw new NotSupportedException();
        public IReadOnlyList<T> LoadMany<T>(IEnumerable<Guid> ids) => throw new NotSupportedException();

        public IReadOnlyList<T> LoadMany<T>(params int[] ids) => throw new NotSupportedException();
        public IReadOnlyList<T> LoadMany<T>(IEnumerable<int> ids) => throw new NotSupportedException();

        public IReadOnlyList<T> LoadMany<T>(params long[] ids) => throw new NotSupportedException();
        public IReadOnlyList<T> LoadMany<T>(IEnumerable<long> ids) => throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(params string[] ids) => throw new NotSupportedException();
        public Task<IReadOnlyList<T>> LoadManyAsync<T>(IEnumerable<string> ids) => throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(params Guid[] ids) => throw new NotSupportedException();
        public Task<IReadOnlyList<T>> LoadManyAsync<T>(IEnumerable<Guid> ids) => throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(params int[] ids) => throw new NotSupportedException();
        public Task<IReadOnlyList<T>> LoadManyAsync<T>(IEnumerable<int> ids) => throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(params long[] ids) => throw new NotSupportedException();
        public Task<IReadOnlyList<T>> LoadManyAsync<T>(IEnumerable<long> ids) => throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, params string[] ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, IEnumerable<string> ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, params Guid[] ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, IEnumerable<Guid> ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, params int[] ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, IEnumerable<int> ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, params long[] ids) =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<T>> LoadManyAsync<T>(CancellationToken token, IEnumerable<long> ids) =>
            throw new NotSupportedException();

        public Guid? VersionFor<TDoc>(TDoc entity) => throw new NotSupportedException();

        public IReadOnlyList<TDoc> Search<TDoc>(string queryText, string regConfig = "english") =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<TDoc>> SearchAsync<TDoc>(string queryText,
            string regConfig = "english",
            CancellationToken token = new()) => throw new NotSupportedException();

        public IReadOnlyList<TDoc> PlainTextSearch<TDoc>(string searchTerm, string regConfig = "english") =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<TDoc>> PlainTextSearchAsync<TDoc>(string searchTerm,
            string regConfig = "english",
            CancellationToken token = new()) => throw new NotSupportedException();

        public IReadOnlyList<TDoc> PhraseSearch<TDoc>(string searchTerm, string regConfig = "english") =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<TDoc>> PhraseSearchAsync<TDoc>(string searchTerm,
            string regConfig = "english",
            CancellationToken token = new()) => throw new NotSupportedException();

        public IReadOnlyList<TDoc> WebStyleSearch<TDoc>(string searchTerm, string regConfig = "english") =>
            throw new NotSupportedException();

        public Task<IReadOnlyList<TDoc>> WebStyleSearchAsync<TDoc>(string searchTerm,
            string regConfig = "english",
            CancellationToken token = new()) => throw new NotSupportedException();

        public DocumentMetadata? MetadataFor<T>(T entity) where T : notnull => throw new NotImplementedException();

        public Task<DocumentMetadata> MetadataForAsync<T>(T entity, CancellationToken token = new CancellationToken()) where T : notnull => throw new NotImplementedException();

        ITenantOperations IDocumentSession.ForTenant(string tenantId) => throw new NotImplementedException();

        public object? GetHeader(string key) => throw new NotImplementedException();

        ITenantQueryOperations IQuerySession.ForTenant(string tenantId) => throw new NotImplementedException();

        public NpgsqlConnection Connection { get; } = default!;

        public IMartenSessionLogger Logger { get; set; } = default!;

        public int RequestCount { get; } = default!;

        public IDocumentStore DocumentStore { get; } = default!;
        IQueryEventStore IQuerySession.Events => Events;

        public IJsonLoader Json { get; } = default!;
        public string? CausationId { get; set; }
        public string? CorrelationId { get; set; }

        public ITenant Tenant { get; } = default!;

        public ISerializer Serializer { get; } = default!;

        public void Delete<T>(T entity) => throw new NotSupportedException();

        public void Delete<T>(int id) => throw new NotSupportedException();

        public void Delete<T>(long id) => throw new NotSupportedException();

        public void Delete<T>(Guid id) => throw new NotSupportedException();

        public void Delete<T>(string id) => throw new NotSupportedException();

        public void DeleteWhere<T>(Expression<Func<T, bool>> expression) => throw new NotSupportedException();

        public void SaveChanges() => throw new NotSupportedException();

        public Task SaveChangesAsync(CancellationToken token = new()) => Task.CompletedTask;
        public void Store<T>(IEnumerable<T> entities) => throw new NotSupportedException();

        public void Store<T>(params T[] entities)
        {
            // in-memory
        }

        public void Store<T>(string tenantId, IEnumerable<T> entities) => throw new NotSupportedException();

        public void Store<T>(string tenantId, params T[] entities) => throw new NotSupportedException();

        public void Store<T>(T entity, Guid version) => throw new NotSupportedException();
        public void QueueOperation(IStorageOperation storageOperation) => throw new NotImplementedException();

        public void Insert<T>(IEnumerable<T> entities) => throw new NotSupportedException();

        public void Insert<T>(params T[] entities) => throw new NotSupportedException();
        public void Update<T>(IEnumerable<T> entities) => throw new NotSupportedException();

        public void Update<T>(params T[] entities) => throw new NotSupportedException();

        public void InsertObjects(IEnumerable<object> documents) => throw new NotSupportedException();
        public void HardDelete<T>(T entity) where T : notnull => throw new NotImplementedException();

        public void HardDelete<T>(int id) where T : notnull => throw new NotImplementedException();

        public void HardDelete<T>(long id) where T : notnull => throw new NotImplementedException();

        public void HardDelete<T>(Guid id) where T : notnull => throw new NotImplementedException();

        public void HardDelete<T>(string id) where T : notnull => throw new NotImplementedException();

        public void HardDeleteWhere<T>(Expression<Func<T, bool>> expression) where T : notnull => throw new NotImplementedException();

        public void UndoDeleteWhere<T>(Expression<Func<T, bool>> expression) where T : notnull => throw new NotImplementedException();

        public void StoreObjects(IEnumerable<object> documents) => throw new NotSupportedException();

        public void Eject<T>(T document) => throw new NotSupportedException();

        public void EjectAllOfType(Type type) => throw new NotSupportedException();
        public void SetHeader(string key, object value) => throw new NotImplementedException();

        public IUnitOfWork PendingChanges { get; } = default!;

        public IEventStore Events { get; } = default!;

        public ConcurrencyChecks Concurrency { get; } = default!;

        public IList<IDocumentSessionListener> Listeners { get; } = default!;
        public string? LastModifiedBy { get; set; }

        public ValueTask DisposeAsync() => throw new NotImplementedException();
    }
}
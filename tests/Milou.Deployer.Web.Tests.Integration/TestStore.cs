using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using Marten.Events;
using Marten.Events.Daemon;
using Marten.Events.Projections;
using Marten.Schema;
using Marten.Services;
using Marten.Storage;
using Microsoft.Extensions.Logging;
using Weasel.Core.Migrations;

namespace Milou.Deployer.Web.Tests.Integration
{
    public sealed class TestStore : IDocumentStore
    {
        private AdvancedOperations _advanced;
        private IDatabase _schema;

        public void Dispose()
        {
            // in-memory
        }

        public void BulkInsert<T>(IReadOnlyCollection<T> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000) => throw new NotSupportedException();

        public void BulkInsert<T>(string tenantId,
            IReadOnlyCollection<T> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000) => throw new NotSupportedException();

        public Task BulkInsertAsync<T>(IReadOnlyCollection<T> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000,
            CancellationToken cancellation = new CancellationToken()) =>
            throw new NotSupportedException();

        public Task BulkInsertAsync<T>(string tenantId,
            IReadOnlyCollection<T> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000,
            CancellationToken cancellation = new CancellationToken()) =>
            throw new NotSupportedException();

        public IDocumentSession OpenSession(DocumentTracking tracking = DocumentTracking.IdentityOnly,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => new TestDocumentSession();

        public IDocumentSession OpenSession(string tenantId,
            DocumentTracking tracking = DocumentTracking.IdentityOnly,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => throw new NotSupportedException();

        public IDocumentSession OpenSession(SessionOptions options) => new TestDocumentSession();
        public Task<IDocumentSession> OpenSessionAsync(SessionOptions options, CancellationToken token = new CancellationToken()) => throw new NotSupportedException();

        public IDocumentSession LightweightSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) =>
            throw new NotSupportedException();

        public IDocumentSession LightweightSession(string tenantId,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => throw new NotSupportedException();

        public IDocumentSession DirtyTrackedSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) =>
            throw new NotSupportedException();

        public IDocumentSession DirtyTrackedSession(string tenantId,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => throw new NotSupportedException();

        public IQuerySession QuerySession() => throw new NotSupportedException();

        public IQuerySession QuerySession(string tenantId) => throw new NotSupportedException();

        public IQuerySession QuerySession(SessionOptions options) => throw new NotSupportedException();

        public void BulkInsertDocuments(IEnumerable<object> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000) => throw new NotSupportedException();

        public void BulkInsertDocuments(string tenantId,
            IEnumerable<object> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000) => throw new NotSupportedException();

        public Task BulkInsertDocumentsAsync(IEnumerable<object> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000,
            CancellationToken cancellation = new CancellationToken()) =>
            throw new NotSupportedException();

        public Task BulkInsertDocumentsAsync(string tenantId,
            IEnumerable<object> documents,
            BulkInsertMode mode = BulkInsertMode.InsertsOnly,
            int batchSize = 1000,
            CancellationToken cancellation = new CancellationToken()) =>
            throw new NotSupportedException();

        public IProjectionDaemon BuildProjectionDaemon(string? tenantIdOrDatabaseIdentifier = null, ILogger? logger = null) => throw new NotSupportedException();

        public ValueTask<IProjectionDaemon> BuildProjectionDaemonAsync(string? tenantIdOrDatabaseIdentifier = null, ILogger? logger = null) => throw new NotSupportedException();

        public IProjectionDaemon BuildProjectionDaemon(ILogger? logger = null) => throw new NotSupportedException();

        public IReadOnlyStoreOptions Options { get; }

        IDatabase IDocumentStore.Schema => _schema;

        public IMartenStorage Storage { get; }

        AdvancedOperations IDocumentStore.Advanced => _advanced;

        public AdvancedOptions Advanced { get; } = default!;

        public IDiagnostics Diagnostics { get; } = default!;

        public EventGraph Events { get; } = default!;

        public ITenancy Tenancy { get; } = default!;
    }
}
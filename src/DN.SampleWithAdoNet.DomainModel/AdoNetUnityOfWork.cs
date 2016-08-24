using System;
using System.Data;

namespace DN.SampleWithAdoNet.DomainModel
{
    public class AdoNetUnityOfWork : IUnityOfWork
    {
        private IDbTransaction _transaction;
        private readonly Action<AdoNetUnityOfWork> _rolledBack;
        private readonly Action<AdoNetUnityOfWork> _committed;
        public IDbTransaction Transaction { get; private set; }

        public AdoNetUnityOfWork(IDbTransaction transaction, Action<AdoNetUnityOfWork> rolledBack, Action<AdoNetUnityOfWork> committed)
        {
            Transaction = transaction;
            _transaction = transaction;
            _rolledBack = rolledBack;
            _committed = committed;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("May not call save changes twice.");

            _transaction.Commit();
            _committed(this);
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction == null)
                return;

            _transaction.Rollback();
            _transaction.Dispose();
            _rolledBack(this);
            _transaction = null;
        }
    }
}

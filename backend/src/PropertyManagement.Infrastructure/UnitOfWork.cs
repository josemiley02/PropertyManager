using System;
using System.Data;
using System.Data.Common;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PropertyManagement.Infrastructure.Abstractions;
using PropertyManagement.Infrastructure.Concrete;
namespace PropertyManagement.Infrastructure;

[RegisterService<IUnitOfWork>(LifeTime.Scoped)]
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, ICommitable> _repositories;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, ICommitable>();
        _transaction = null;
    }

    protected bool IsDisposed { get; private set; }

    public void BeginTransaction()
    {
        DbConnection connection = _context.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        _transaction = _context.Database.BeginTransaction(IsolationLevel.Unspecified);
    }

    public int CommitChanges(bool autoRollbackOnError = true)
    {
        int result;
        try
        {
            result = _repositories.Values.Sum((ICommitable factory) => factory.Commit());
        }
        catch (Exception)
        {
            if (autoRollbackOnError)
            {
                this.RollBack();
            }
            throw;
        }
        return result;
    }

    public virtual async Task<int> CommitChangesAsync(bool autoRollbackOnError = true)
        {
            int result;
            try
            {
                int num = 0;
                foreach (ICommitable current in this._repositories.Values)
                {
                    num += await current.CommitAsync();
                }
                result = num;
            }
            catch (Exception)
            {
                if (autoRollbackOnError)
                {
                    this.RollBack();
                }
                throw;
            }
            return result;
        }

    public bool CommitTransaction()
    {
        _transaction?.Commit();
        return true;
    }

    public IGenericCoreRepositoryAsync<TEntity> DbRepository<TEntity>() where TEntity : class
    {
        Type type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            Type implementationType = typeof(GenericCoreRepository<>).MakeGenericType(typeof(TEntity));
            object? instance = Activator.CreateInstance(implementationType, _context);
            if (instance != null)
            {
                _repositories.Add(type, (ICommitable)instance);
            }
        }
        return (IGenericCoreRepositoryAsync<TEntity>)_repositories[type];
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
                _context.Dispose();
                _transaction?.Dispose();
            }
            IsDisposed = true;
        }
    }

    public void RollBack() => _transaction?.Rollback();

    public void Save() => _context.SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
}

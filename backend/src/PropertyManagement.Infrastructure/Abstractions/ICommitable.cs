using System;

namespace PropertyManagement.Infrastructure.Abstractions;

public interface ICommitable : IDisposable
{
    bool IsDisposed { get; }
    int Commit();
    Task<int> CommitAsync();
}
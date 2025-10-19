using System;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Application.Abstractions;

public interface ICheckUniqueService
{
    public Task<bool> CheckUniqueNameAsync<T>(T entity) where T : class , IEntity;
}

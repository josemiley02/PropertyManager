using System;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Application.Abstractions;
using PropertyManagement.Domain.Entities.Identity;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Services;

public class CheckUniqueService : ICheckUniqueService
{
    readonly IUnitOfWork UnitOfWork;
    public CheckUniqueService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    public async Task<bool> CheckUniqueNameAsync<T>(T entity) where T : class , IEntity
    {
        var entityType = typeof(T);
        var idProp = entityType.GetProperty("Id");
        var nameProp = entityType.GetProperty("Name");
        if (nameProp == null)
        {
            throw new InvalidOperationException($"La entidad {entityType.Name} no tiene una propiedad \"Name\" ");
        }
        var entityRepository = UnitOfWork!.DbRepository<T>();
        var entities = await entityRepository.GetAll(useInactive: true).ToListAsync();
        var result = entities.FirstOrDefault(e => nameProp.GetValue(entity)?.ToString()?.ToLower() == nameProp.GetValue(e)?.ToString()?.ToLower());
        var same = false;
        if(result != null && idProp != null)
        {
            var entityId = idProp.GetValue(entity);
            var resultId = idProp.GetValue(result);
            same = entityId != null && resultId != null ? entityId.Equals(resultId) : false;
        }        
        return result == null || same;
    }
}
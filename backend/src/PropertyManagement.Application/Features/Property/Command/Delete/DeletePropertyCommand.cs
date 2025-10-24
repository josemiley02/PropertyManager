using System;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.Property.Command.Delete;

public record DeletePropertyCommand : ICommand<Response<NoContentData>>
{
    public long Id { get; init; }
}   

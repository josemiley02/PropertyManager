using System;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.AppUser.Delete;

public record DeleteUserCommand : ICommand<Response<NoContentData>>
{
    public long Id { get; set; }
}
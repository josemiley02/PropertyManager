using System;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.DTOs.Paged;

public class PagedResponse<T> : Response<IEnumerable<T>>
{
    public virtual int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalPages, int totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;
        Data = data;
        Message = null;
    }
}
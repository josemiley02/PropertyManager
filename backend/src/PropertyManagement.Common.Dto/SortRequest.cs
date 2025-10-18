using System;

namespace PropertyManagement.Common.Dto;

public record SortRequest(string Field, bool IsAsc);
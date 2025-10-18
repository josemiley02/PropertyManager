using System;

namespace PropertyManagement.Infrastructure;

public interface IDateTimeService
{
    DateTime NowUtc { get; set; }
}

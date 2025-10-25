namespace PropertyManagement.Domain.Enums;

public enum EventType
{
    PropertyCreated = 1,
    PropertyUpdated,
    PropertyDeleted,

    BookingCreated,
    BookingCancelled,

    HostRegistered,
    HostUpdated
}

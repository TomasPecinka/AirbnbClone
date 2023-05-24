using AirbnbClone.Application.Common.Interfaces.Services;

namespace AirbnbClone.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
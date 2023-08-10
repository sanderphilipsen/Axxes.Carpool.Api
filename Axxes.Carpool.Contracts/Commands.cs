namespace Axxes.Carpool.Contracts;

public record RegisterCommand(Guid EventId, string PersonName, bool OpenToCarpool, bool CanDrive, string? LicensePlateNumber = null);

public record EventCommand(string Name, string Description, DateTime StartDateTime, DateTime EnDateTime, string Location);



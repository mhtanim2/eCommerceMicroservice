
namespace BusinessLoginLayer.RabbitMQ;

public record ProductNameUpdateMessage(Guid ProductID, string? NewName);


namespace BusinessLoginLayer.RabbitMQ;

public record ProductDeletionMessage(Guid ProductID, string? ProductName);

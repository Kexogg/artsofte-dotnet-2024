using DataExchange.Identity.Models;
using MassTransit;

namespace Services.Saga;

public abstract class SagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public Guid UserId { get; set; }

    public required UserModel UserInfo { get; set; }

    public required State CurrentState { get; set; }
}
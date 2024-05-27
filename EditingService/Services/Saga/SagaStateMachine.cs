using MassTransit;
using Services.Saga.Models;

namespace Services.Saga;

public class SagaStateMachine : MassTransitStateMachine<SagaState>
{
    public State? CreatingTask { get; private set; }
    public State? Success { get; private set; }
    public State? Failed { get; private set; }

    public Event<UserInfoRequest>? UserInfoRequested { get; private set; }
    public Event<UserInfoResponse>? UserFound { get; private set; }
    public Event<UserInfoFail>? UserNotFound { get; private set; }

    public SagaStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UserInfoRequested, x => x.CorrelateById(context => context.Message.UserId));
        Event(() => UserNotFound, x => x.CorrelateById(context => context.Message.UserId));
        Event(() => UserFound, x => x.CorrelateById(context => context.Message.UserId));

        Initially(
            When(UserInfoRequested)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.UserInfo = context.Message.UserInfo;
                })
                .Publish(ctx => new UserInfoRequest(ctx.Saga.UserId, ctx.Saga.UserInfo))
                .TransitionTo(CreatingTask)
        );

        During(CreatingTask,
            When(UserFound)
                .TransitionTo(Success)
        );

        DuringAny(
            When(UserNotFound)
                .Then(_ => throw new Exception("Not found"))
                .TransitionTo(Failed)
        );

        SetCompletedWhenFinalized();
    }
}
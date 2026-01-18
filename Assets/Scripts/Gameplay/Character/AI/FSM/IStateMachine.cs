using R3;

namespace Gameplay.Character.AI.FSM
{
    public interface IStateMachine
    {
        ReadOnlyReactiveProperty<IState> CurrentState { get; }

        void GoTo<TState>() where TState : IState;
        void Update();
    }
}
using System;
using System.Collections.Generic;
using R3;

namespace Gameplay.Character.AI.FSM
{
    public class StateMachine : IStateMachine
    {
        private readonly ReactiveProperty<IState> currentState = new();
        private readonly Dictionary<Type, IState> states = new();

        public ReadOnlyReactiveProperty<IState> CurrentState => currentState;

        public bool TryAdd<TState>(TState state)
            where TState : IState
        {
            var stateType = typeof(TState);
            return states.TryAdd(stateType, state);
        }

        public void GoTo<TState>()
            where TState : IState
        {
            currentState.Value?.Exit();

            var nextStateType = typeof(TState);
            var nextState = states[nextStateType];

            currentState.Value = nextState;
            nextState.Enter();
        }

        public void Update()
        {
            currentState.Value.Update();
        }
    }
}
using System;
using System.Collections.Generic;

namespace TatRat.API
{
    public sealed class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;

        public void Add(IState state)
        {
            var type = state.GetType();
            if (!_states.TryAdd(type, state))
            {
                throw new ArgumentException($"State of type {type} already exists");
            }
        }

        public void ChangeState<TState>() where TState : class, IEnterableState
        {
            if (_currentState is TState)
            {
                return;
            }

            TryToExitFromCurrentState();
            _currentState = GetState<TState>();
            (_currentState as IEnterableState)?.Enter();
        }

        public void ChangeState<TState, TParam>(TParam data) where TState : class, IEnterableState<TParam>
        {
            if (_currentState is TState)
            {
                return;
            }

            TryToExitFromCurrentState();
            _currentState = GetState<TState>();
            (_currentState as IEnterableState<TParam>)?.Enter(data);
        }

        public TState GetCurrentState<TState>() where TState : class, IState =>
            (_currentState as TState);

        private void TryToExitFromCurrentState()
            => (_currentState as IExitableState)?.Exit();

        private IState GetState<TState>() where TState : IState
        {
            var type = typeof(TState);
            if (!_states.TryGetValue(type, out var result))
            {
                throw new ArgumentException($"State of type {type} is not found");
            }

            return result;
        }
    }
}
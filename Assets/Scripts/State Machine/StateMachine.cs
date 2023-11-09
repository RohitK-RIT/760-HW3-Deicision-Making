using System;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public abstract class StateMachine<TState> : MonoBehaviour where TState : Enum
    {
        protected Dictionary<TState, BaseState<TState>> States = new();

        protected BaseState<TState> CurrentState
        {
            get => _currentState;
            set
            {
                _isSwitchingStates = true;
                _currentState?.ExitState();
                _currentState = value;
                _currentState?.EnterState();
                _isSwitchingStates = false;
            }
        }

        private BaseState<TState> _currentState;
        private bool _isSwitchingStates;


        protected virtual void Update()
        {
            if (_isSwitchingStates) return;

            var nextStateKey = CurrentState.GetNextState();
            if (nextStateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else
                CurrentState = States[nextStateKey];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CurrentState.OnTriggerEnter2D(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            CurrentState.OnTriggerStay2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            CurrentState.OnTriggerExit2D(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CurrentState.OnCollisionEnter2D(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            CurrentState.OnCollisionStay2D(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            CurrentState.OnCollisionExit2D(other);
        }
    }
}
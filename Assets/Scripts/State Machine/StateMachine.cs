using System;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    /// <summary>
    /// Generic state machine
    /// </summary>
    /// <typeparam name="TState">Enum for state</typeparam>
    public abstract class StateMachine<TState> : MonoBehaviour where TState : Enum
    {
        /// <summary>
        /// Dictionary for to store the states.
        /// </summary>
        protected Dictionary<TState, BaseState<TState>> States = new();

        /// <summary>
        /// Property to access the current state the state machine is in.
        /// </summary>
        protected BaseState<TState> CurrentState
        {
            get => _currentState;
            set
            {
                // Set switching states flag to true.
                _isSwitchingStates = true;
                // Exit the current state.
                _currentState?.ExitState();
                // Assign the new state to current state.
                _currentState = value;
                // Enter the current state aka the new state.
                _currentState?.EnterState();
                // Set switching states flag to false.
                _isSwitchingStates = false;
            }
        }

        /// <summary>
        /// Variable to store the current state.
        /// </summary>
        private BaseState<TState> _currentState;

        /// <summary>
        /// Flag to check if state machine is switching states.
        /// </summary>
        private bool _isSwitchingStates;
        
        protected virtual void Update()
        {
            // If switching states return.
            if (_isSwitchingStates) return;

            // Else...
            // Get next state key from the current state.
            var nextStateKey = CurrentState.GetNextState();
            // If current state key and next state key is same, then update state.
            if (nextStateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            // Else set the next state as current state.
            else
                CurrentState = States[nextStateKey];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Pass on the trigger enter 2d function to current state.
            CurrentState.OnTriggerEnter2D(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            // Pass on the trigger stay 2d function to current state.
            CurrentState.OnTriggerStay2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Pass on the trigger exit 2d function to current state.
            CurrentState.OnTriggerExit2D(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Pass on the collision enter 2d function to current state.
            CurrentState.OnCollisionEnter2D(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            // Pass on the collision stay 2d function to current state.
            CurrentState.OnCollisionStay2D(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            // Pass on the collision exit 2d function to current state.
            CurrentState.OnCollisionExit2D(other);
        }
    }
}
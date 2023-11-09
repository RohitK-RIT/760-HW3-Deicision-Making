using System;
using State_Machine;
using UnityEngine;

namespace Characters.Base.State_Machine
{
    /// <summary>
    /// Generic base state for the agent.
    /// </summary>
    /// <typeparam name="TState">Agent's state enum</typeparam>
    /// <typeparam name="TAgent">Agent type</typeparam>
    /// <typeparam name="TStateMachine">Agent's state machine</typeparam>
    public abstract class AgentBaseState<TState, TAgent, TStateMachine> : BaseState<TState>
        where TState : Enum
        where TStateMachine : AgentStateMachine<TState, TAgent, TStateMachine>
        where TAgent : Agent<TState, TStateMachine, TAgent>
    {
        /// <summary>
        /// Property override to access the state manager.
        /// </summary>
        protected new TStateMachine StateMachine { get; }

        /// <summary>
        /// Property to access the agent of the state manager
        /// </summary>
        protected TAgent Agent => StateMachine.Agent;

        /// <summary>
        /// Constructor of the Agent base state. 
        /// </summary>
        /// <param name="key">The state key</param>
        /// <param name="gameObject">Game Object of the agent</param>
        /// <param name="stateMachine">State machine of the agent</param>
        protected AgentBaseState(TState key, GameObject gameObject, TStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
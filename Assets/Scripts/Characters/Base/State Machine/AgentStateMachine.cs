using System;
using State_Machine;
using TMPro;
using UnityEngine;

namespace Characters.Base.State_Machine
{
    /// <summary>
    /// Generic state machine for the agent.
    /// </summary>
    /// <typeparam name="TState">Generic parameter for the states of the agent</typeparam>
    /// <typeparam name="TAgent">Generic parameter for the agent type</typeparam>
    /// <typeparam name="TStateMachine">Generic parameter for the agent's state machine</typeparam>
    public abstract class AgentStateMachine<TState, TAgent, TStateMachine> : StateMachine<TState>
        where TState : Enum
        where TAgent : Agent<TState, TStateMachine, TAgent>
        where TStateMachine : AgentStateMachine<TState, TAgent, TStateMachine>
    {
        /// <summary>
        /// Debug text for the display agent's state.
        /// </summary>
        [SerializeField] private TMP_Text stateText;

        /// <summary>
        /// Reference of the agent.
        /// </summary>
        public TAgent Agent { get; private set; }

        /// <summary>
        /// Initialization process for the agent.
        /// </summary>
        /// <param name="agent">Agent for which the state machine is</param>
        public virtual void Init(TAgent agent)
        {
            Agent = agent;
        }

        protected override void Update()
        {
            base.Update();

            // If there is state text assigned, display the current state.
            if (stateText)
                stateText.SetText(CurrentState.StateKey.ToString());
        }
    }
}
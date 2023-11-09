using System;
using State_Machine;
using UnityEngine;

namespace Characters.Base.State_Machine
{
    public abstract class AgentBaseState<TState, TAgent, TStateMachine> : BaseState<TState>
        where TState : Enum
        where TStateMachine : AgentStateMachine<TState, TAgent, TStateMachine>
        where TAgent : Agent<TState, TStateMachine, TAgent>
    {
        protected new TStateMachine StateMachine { get; private set; }

        protected TAgent Agent => StateMachine.Agent;

        protected AgentBaseState(TState key, GameObject gameObject, TStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
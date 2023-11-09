using System;
using State_Machine;
using TMPro;
using UnityEngine;

namespace Characters.Base.State_Machine
{
    public abstract class AgentStateMachine<TState, TAgent, TStateMachine> : StateMachine<TState>
        where TState : Enum
        where TAgent : Agent<TState, TStateMachine, TAgent>
        where TStateMachine : AgentStateMachine<TState, TAgent, TStateMachine>
    {
        [SerializeField] private TMP_Text stateText;
        public TAgent Agent { get; private set; }

        public virtual void Init(TAgent agent)
        {
            Agent = agent;
        }

        protected override void Update()
        {
            base.Update();

            if (stateText)
                stateText.SetText(CurrentState.StateKey.ToString());
        }
    }
}
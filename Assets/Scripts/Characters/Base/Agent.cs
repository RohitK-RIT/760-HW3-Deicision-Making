using System;
using Characters.Base.State_Machine;
using UnityEngine;

namespace Characters.Base
{
    public abstract class Agent<TState, TStateMachine, TAgent> : MonoBehaviour
        where TStateMachine : AgentStateMachine<TState, TAgent, TStateMachine>
        where TState : Enum
        where TAgent : Agent<TState, TStateMachine, TAgent>
    {
        [SerializeField] private float damageOnCollision;
        [SerializeField] private AgentMovement movement;
        [SerializeField] private AgentHealth health;
        [SerializeField] private TStateMachine stateMachine;

        public float Damage => damageOnCollision;
        public AgentMovement Movement => movement;
        public AgentHealth Health => health;
        public TStateMachine StateMachine => stateMachine;

        public event Action OnAgentDestroyed;

        public virtual void Init()
        {
            StateMachine.Init((TAgent)this);
        }

        public virtual bool HasEnemyInRange()
        {
            return false;
        }

        private void OnDestroy()
        {
            OnAgentDestroyed?.Invoke();
        }

        protected void DrawGizmos()
        {
            if(!Application.isPlaying) return;

            Gizmos.color = HasEnemyInRange() ? Color.red : Color.black;
            
            Gizmos.DrawWireSphere(transform.position, GameManager.Instance.EnemySearchRadius);
        }
    }
}
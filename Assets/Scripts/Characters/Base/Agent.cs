using System;
using Characters.Base.State_Machine;
using UnityEngine;

namespace Characters.Base
{
    /// <summary>
    /// Abstract class for the agent.
    /// </summary>
    /// <typeparam name="TState">Generic state enum for Agent's state machine</typeparam>
    /// <typeparam name="TStateMachine">Generic State Machine for the Agent</typeparam>
    /// <typeparam name="TAgent">Generic Type for the Agent</typeparam>
    public abstract class Agent<TState, TStateMachine, TAgent> : MonoBehaviour
        where TStateMachine : AgentStateMachine<TState, TAgent, TStateMachine>
        where TState : Enum
        where TAgent : Agent<TState, TStateMachine, TAgent>
    {
        /// <summary>
        /// Damage dealt on collision.
        /// </summary>
        [SerializeField] private float damageOnCollision;
        /// <summary>
        /// Movement component of the agent.
        /// </summary>
        [SerializeField] private AgentMovement movement;
        /// <summary>
        /// Health component of the agent.
        /// </summary>
        [SerializeField] private AgentHealth health;
        /// <summary>
        /// State machine component of the agent.
        /// </summary>
        [SerializeField] private TStateMachine stateMachine;

        /// <summary>
        /// Property to access the damage dealt on collision.
        /// </summary>
        public float Damage => damageOnCollision;
        /// <summary>
        /// Property to access the Movement component.
        /// </summary>
        public AgentMovement Movement => movement;
        /// <summary>
        /// Property to access the Health component.
        /// </summary>
        public AgentHealth Health => health;
        /// <summary>
        /// Property to access the State Machine component.
        /// </summary>
        public TStateMachine StateMachine => stateMachine;

        /// <summary>
        /// Event called when the agent dies.
        /// </summary>
        public event Action OnAgentDestroyed;

        /// <summary>
        /// Function to initialize the Agent.
        /// </summary>
        public void Init()
        {
            // Initialize the State Machine component.
            StateMachine.Init((TAgent)this);
        }

        /// <summary>
        /// Function to check if the enemy is in Range.
        /// </summary>
        /// <returns>If enemy is in range</returns>
        public virtual bool HasEnemyInRange()
        {
            return false;
        }

        private void OnDestroy()
        {
            // Call the On Agent Destroyed event. 
            OnAgentDestroyed?.Invoke();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Draw Gizmos functions for the agent.
        /// </summary>
        protected void DrawGizmos()
        {
            if(!Application.isPlaying) return;

            Gizmos.color = HasEnemyInRange() ? Color.red : Color.black;
            
            Gizmos.DrawWireSphere(transform.position, GameManager.Instance.EnemySearchRadius);
        }
#endif
    }
}
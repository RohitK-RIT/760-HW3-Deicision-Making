using Characters.Base.State_Machine;
using UnityEngine;

namespace Characters.Enemy.State_Machine
{
    /// <summary>
    /// Enemy's base state.
    /// </summary>
    public abstract class EnemyBaseState : AgentBaseState<EnemyStates, Enemy, EnemyStateMachine>
    {
        /// <summary>
        /// Constructor for the enemy base state.
        /// </summary>
        /// <param name="key">enum key for the state</param>
        /// <param name="gameObject">gameObject on which the state machine is attached</param>
        /// <param name="stateMachine">owner state machine component for the state</param>
        protected EnemyBaseState(EnemyStates key, GameObject gameObject, EnemyStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }
    }
}
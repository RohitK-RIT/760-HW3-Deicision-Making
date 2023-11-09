using System.Collections.Generic;
using Characters.Base.State_Machine;
using State_Machine;

namespace Characters.Enemy.State_Machine
{
    /// <summary>
    /// Enemy's state machine component.
    /// </summary>
    public class EnemyStateMachine : AgentStateMachine<EnemyStates, Enemy, EnemyStateMachine>
    {
        /// <summary>
        /// Initialization function for the enemy state machine.
        /// </summary>
        /// <param name="enemy">owner enemy agent component</param>
        public override void Init(Enemy enemy)
        {
            // Initialize the base class.
            base.Init(enemy);
            var playerGameObject = gameObject;
            // Create states for the enemy state machine.
            States = new Dictionary<EnemyStates, BaseState<EnemyStates>>
            {
                { EnemyStates.Wander, new EnemyWanderState(EnemyStates.Wander, playerGameObject, this) },
                { EnemyStates.Attack, new EnemyAttackState(EnemyStates.Attack, playerGameObject, this) },
            };

            // Set current state to wander.
            CurrentState = States[EnemyStates.Wander];
        }
    }

    /// <summary>
    /// Enum for the enemy states.
    /// </summary>
    public enum EnemyStates
    {
        /// <summary>
        /// Wander around the level.
        /// </summary>
        Wander,
        /// <summary>
        /// Attack the player.
        /// </summary>
        Attack
    }
}
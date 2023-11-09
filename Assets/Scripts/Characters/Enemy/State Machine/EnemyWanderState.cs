using UnityEngine;

namespace Characters.Enemy.State_Machine
{
    /// <summary>
    /// Enemy wander state.
    /// </summary>
    public class EnemyWanderState : EnemyBaseState
    {
        /// <summary>
        /// Constructor for the enemy wander state.
        /// </summary>
        /// <param name="key">key of the state</param>
        /// <param name="gameObject">gameObject the state machine is attached to</param>
        /// <param name="stateMachine">owner state machine component</param>
        public EnemyWanderState(EnemyStates key, GameObject gameObject, EnemyStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            // Set a random target point.
            SetRandomTarget();
        }

        /// <summary>
        /// Function to set random target point on the map.
        /// </summary>
        private void SetRandomTarget()
        {
            // Get a random target position from the ground system.
            var targetPosition = GameManager.Instance.GroundSystem.GetRandomPoint();
            // Set the target for the movement component.
            Agent.Movement.SetTarget(targetPosition);
        }

        public override void UpdateState()
        {
            // If agent doesn't have a target then set a random one.
            if (!Agent.Movement.HasTarget)
                SetRandomTarget();
        }

        public override EnemyStates GetNextState()
        {
            // If enemy has a player in range then transition to attack state.
            if (Agent.HasEnemyInRange())
                return EnemyStates.Attack;

            // Else stay in the same state.
            return StateKey;
        }
    }
}
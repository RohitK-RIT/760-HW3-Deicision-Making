using Environment;
using UnityEngine;

namespace Characters.Enemy.State_Machine
{
    /// <summary>
    /// Enemy attack state.
    /// </summary>
    public class EnemyAttackState : EnemyBaseState
    {
        /// <summary>
        /// Variable to store the reference to the player transform.
        /// </summary>
        private Transform _player;
        /// <summary>
        /// Constructor for enemy attack state.
        /// </summary>
        /// <param name="key">Key for the state</param>
        /// <param name="gameObject">GameObject of on which the state machine is attached</param>
        /// <param name="stateMachine">Owner State machine of the state</param>
        public EnemyAttackState(EnemyStates key, GameObject gameObject, EnemyStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            // Get a reference to the player.
            _player = GameManager.Instance.Player.transform;
        }

        public override void ExitState()
        {
            // Set the player reference ot null.
            _player = null;
        }

        public override void UpdateState()
        {
            // If there is no player then return.
            if (!_player) return;

            // Get the nearest node to the target.
            var nearestNodeToTarget = GroundSystem.Instance.GetNearestNode(_player.transform.position);
            // If it's the same as the target node then return.
            if (Agent.Movement.TargetNode != null && Agent.Movement.TargetNode.Equals(nearestNodeToTarget)) return;

            Agent.Movement.SetTarget(_player.transform.position);
        }

        public override EnemyStates GetNextState()
        {
            // If enemy is not in range then start wandering.
            if (!Agent.HasEnemyInRange())
                return EnemyStates.Wander;

            // Else keep attacking.
            return StateKey;
        }
    }
}
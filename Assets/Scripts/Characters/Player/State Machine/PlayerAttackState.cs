using Environment;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    /// <summary>
    /// Player attack state.
    /// </summary>
    public class PlayerAttackState : PlayerBaseState
    {
        /// <summary>
        /// Variable to store the nearest enemy.
        /// </summary>
        private Enemy.Enemy _nearestEnemy;

        /// <summary>
        /// Constructor of the player attack state.
        /// </summary>
        /// <param name="key">key of the state</param>
        /// <param name="gameObject">gameObject the state machine is attached to</param>
        /// <param name="stateMachine">owner state machine</param>
        public PlayerAttackState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            // Try to get the nearest enemy.
            _nearestEnemy = GetNearestEnemy();
            // Get the position of the nearest enemy.
            var targetPosition = _nearestEnemy.transform.position;
            // Set it as the target position for the player.
            Agent.Movement.SetTarget(targetPosition);
        }

        public override void UpdateState()
        {
            // If there no nearest enemy assigned then assign one.
            if (!_nearestEnemy)
                _nearestEnemy = GetNearestEnemy();
            
            // Get the nearest node to the target.
            var nearestNodeToTarget = GroundSystem.Instance.GetNearestNode(_nearestEnemy.transform.position);
            // If it's the same as the target node then return.
            if (Agent.Movement.TargetNode != null && Agent.Movement.TargetNode.Equals(nearestNodeToTarget)) return;

            // Else set a new target for the movement component of the agent.
            Agent.Movement.SetTarget(_nearestEnemy.transform.position);
        }

        public override PlayerStates GetNextState()
        {
            // If player's health is less than 1/3rd then flee
            if (Agent.Health.Normalized <= 0.3f)
                return PlayerStates.Flee;

            // If player has no enemies in range then try to achieve the target.
            if (!Agent.HasEnemyInRange())
                return PlayerStates.GoToTarget;

            // Else stay in attack state.
            return StateKey;
        }

        /// <summary>
        /// Function to get the nearest enemy.
        /// </summary>
        /// <returns>nearest enemy</returns>
        private Enemy.Enemy GetNearestEnemy()
        {
            // Set distance to the nearest enemy as max value.
            var distanceToNearestEnemy = float.MaxValue;
            // Set the nearest enemy as null.
            Enemy.Enemy nearestEnemy = null;
            // Foreach enemy in enemy list
            foreach (var enemy in GameManager.Instance.Enemies)
            {
                // if it is already dead then continue.
                if (!enemy) continue;

                // Else calculate the distance to enemy. 
                var distanceToEnemy = Vector2.Distance(enemy.transform.position, Transform.position);
                // If it is not the closest continue.
                if (distanceToEnemy >= distanceToNearestEnemy) continue;

                // Else set the distance to the nearest enemy and nearest enemy variables.
                distanceToNearestEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }

            // Return the nearest enemy variable.
            return nearestEnemy;
        }
    }
}
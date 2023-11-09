using System.Linq;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    /// <summary>
    /// Player's flee state.
    /// </summary>
    public class PlayerFleeState : PlayerBaseState
    {
        /// <summary>
        /// Constructor for the player flee state.
        /// </summary>
        /// <param name="key">key for the state</param>
        /// <param name="gameObject">gameObject to which the state machine is attached</param>
        /// <param name="stateMachine">owner state machine</param>
        public PlayerFleeState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            // Try to get a flee point and set it as target.
            MoveToFleePoint();
        }

        public override void UpdateState()
        {
            // If agent doesn't have a target then, try to get a flee point and set it as target.
            if (!Agent.Movement.HasTarget)
                MoveToFleePoint();
        }

        public override PlayerStates GetNextState()
        {
            // If enemy is out of range then try to achieve the target.
            if (!Agent.HasEnemyInRange())
                return PlayerStates.GoToTarget;

            // If agent health is greater than 1/3rd try to attack the enemy.
            if (Agent.Health.Normalized > 0.3f)
                return PlayerStates.Attack;

            // Else stay in flee state.
            return StateKey;
        }

        /// <summary>
        /// Function to set a flee point as target.
        /// </summary>
        private void MoveToFleePoint()
        {
            // Get a random walkable point on the map.
            var fleePoint = GameManager.Instance.GroundSystem.GetRandomPoint();
            // Check if the point is safe.
            while (GameManager.Instance.Enemies.Where(enemy => enemy)
                   .Any(enemy => Vector2.Distance(enemy.transform.position, fleePoint) <= GameManager.Instance.EnemySearchRadius))
            {
                // If not then find another point.
                fleePoint = GameManager.Instance.GroundSystem.GetRandomPoint();
            }

            // Set the flee point as the target for the player movement component.
            Agent.Movement.SetTarget(fleePoint);
        }
    }
}
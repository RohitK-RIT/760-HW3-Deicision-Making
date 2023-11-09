using System.Linq;
using Environment;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    public class PlayerAttackState : PlayerBaseState
    {
        private Enemy.Enemy _nearestEnemy;

        public PlayerAttackState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            _nearestEnemy = GetNearestEnemy();
            Agent.Movement.SetTarget(_nearestEnemy.transform.position);
        }

        public override void UpdateState()
        {
            if (!_nearestEnemy)
                _nearestEnemy = GetNearestEnemy();

            // Get the nearest node to the target.
            var nearestNodeToTarget = GroundSystem.Instance.GetNearestNode(_nearestEnemy.transform.position);
            // If it's the same as the target node then return.
            if (Agent.Movement.TargetNode != null && Agent.Movement.TargetNode.Equals(nearestNodeToTarget))
                return;

            Agent.Movement.SetTarget(_nearestEnemy.transform.position);
        }

        public override PlayerStates GetNextState()
        {
            if (Agent.Health.Normalized <= 0.3f)
                return PlayerStates.Flee;

            if (!Agent.HasEnemyInRange())
                return PlayerStates.GoToTarget;

            return StateKey;
        }

        private Enemy.Enemy GetNearestEnemy()
        {
            var distanceToNearestEnemy = float.MaxValue;
            Enemy.Enemy nearestEnemy = null;
            foreach (var enemy in GameManager.Instance.Enemies)
            {
                if (!enemy) continue;

                var distanceToEnemy = Vector2.Distance(enemy.transform.position, Transform.position);
                if (distanceToEnemy >= distanceToNearestEnemy) continue;

                distanceToNearestEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }

            return nearestEnemy;
        }
    }
}